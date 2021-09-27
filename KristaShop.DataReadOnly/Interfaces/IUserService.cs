using KristaShop.Common.Models;
using KristaShop.DataReadOnly.DTOs;
using KristaShop.DataReadOnly.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KristaShop.DataReadOnly.Interfaces
{
    public interface IUserService
    {
        Task<(UserDTO, ApiResult)> GetUserByLoginAndPass(string login, string pass);
        Task<(UserDTO, ApiResult)> SignInByLink(Guid userId);
        List<UserClientDTO> GetAllUsers(UserDTO user);
    }
}
