using AutoMapper;
using KristaShop.Common.Enums;
using KristaShop.Common.Extensions;
using KristaShop.Common.Helpers;
using KristaShop.Common.Models;
using KristaShop.DataReadOnly.DTOs;
using KristaShop.DataReadOnly.Interfaces;
using KristaShop.DataReadOnly.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KristaShop.DataReadOnly.Services
{
    public class UserService : IUserService
    {
        private readonly IReadOnlyRepo<User> _userRepo;
        private readonly IReadOnlyRepo<UserGroup> _ugRepo;
        private readonly IReadOnlyRepo<UserGroupMembership> _ugmRepo;
        private readonly IMapper _mapper;

        public UserService
            (IReadOnlyRepo<User> userRepo, IReadOnlyRepo<UserGroup> ugRepo, IReadOnlyRepo<UserGroupMembership> ugmRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _ugRepo = ugRepo;
            _ugmRepo = ugmRepo;
            _mapper = mapper;
        }

        public List<UserClientDTO> GetAllUsers(UserDTO user)
        {
            var users = _ugmRepo.QueryReadOnly()
                .Include(x => x.User).ThenInclude(x => x.Counterparty).ThenInclude(x => x.City)
                .Include(x => x.UserGroup)
                .Where(x => x.UserGroup.type == (int)UserType.Customer)
                .Select(x => x.User);
            bool? isAdmin = user.UserGroups?
                .Any(x => x.UserType != (int)UserType.Manager && x.UserType == (int)UserType.Administrator);
            if (isAdmin == true || (isAdmin == null && user.IsRoot))
            {
                return _mapper.Map<List<UserClientDTO>>(users.ToList());
            }
            bool isManager = user.UserGroups
                .Any(x => x.UserType == (int)UserType.Manager);
            if (isManager)
            {
                users = users
                    .Where(x => x.Counterparty.manager_id == user.UserId);

                return _mapper.Map<List<UserClientDTO>>(users.ToList());
            }
            return null;
        }

        public async Task<(UserDTO, ApiResult)> GetUserByLoginAndPass(string login, string pass)
        {
            var user = await _userRepo.FindByFilterAsync(x => x.login == login && x.password == HashHelper.TransformPassword(pass));
            return GetUserResult(user);
        }

        public async Task<(UserDTO, ApiResult)> SignInByLink(Guid userId)
        {
            var user = await _userRepo.FindByIdAsync(userId);
            return GetUserResult(user);
        }

        private (UserDTO, ApiResult) GetUserResult(User user)
        {
            if (user == null)
                return (null, new ApiResult(false, "Неверный логин или пароль!"));

            var userDTO = _mapper.Map<UserDTO>(user);
            if (!userDTO.IsRoot)
            {
                userDTO.UserGroups = GetUserGroups(user.id);
                userDTO.AccessLevel = userDTO.UserGroups.Any() ? userDTO.UserGroups.Min(x => x.AccessLevel) : -1;
            }

            return userDTO.Status switch
            {
                (int)UserStatus.Active when userDTO != null => (userDTO, new ApiResult(true)),
                (int)UserStatus.Active when userDTO.AccessLevel == -1 => (null, new ApiResult(false, "Данный пользователь не имеет доступа. Обратитесь к администратору.")),
                (int)UserStatus.Await => (null, new ApiResult(false, "Данный пользователь на рассмотрении.")),
                (int)UserStatus.Banned when user.ban_expire_date == null => (null, new ApiResult(false, $"Данный пользователь заблокирован навсегда.")),
                (int)UserStatus.Banned when user.ban_expire_date != null => (null, new ApiResult(false, $"Данный пользователь заблокирован до {user.ban_expire_date}.")),
                (int)UserStatus.Banned when user.ban_reason != null => (null, new ApiResult(false, $"Данный пользователь заблокирован по причине {user.ban_reason}.")),
                (int)UserStatus.Deleted => (null, new ApiResult(false, "Неверный логин или пароль!")),
                _ => (null, new ApiResult(false, "Неверный логин или пароль!"))
            };
        }

        private List<UserGroupDTO> GetUserGroups(Guid userId)
        {
            var userGroup = _ugmRepo.QueryFindBy(x => x.user_id == userId)
                .Select(x => new UserGroupDTO
                {
                    GroupId = x.UserGroup.id,
                    UserType = x.UserGroup.type,
                    AccessLevel = x.UserGroup.AccessControl.acl
                });

            return userGroup.ToList();
        }
    }
}
