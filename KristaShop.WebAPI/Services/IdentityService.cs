using AutoMapper;
using KristaShop.Common.Enums;
using KristaShop.Common.Helpers;
using KristaShop.Common.Models;
using KristaShop.WebAPI.Data;
using KristaShop.WebAPI.Interfaces;
using KristaShop.WebAPI.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KristaShop.WebAPI.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly KristaAsupDbContext _context;
        private readonly IMapper _mapper;

        public IdentityService
            (KristaAsupDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApiResult> UserRegistrate(RegHashViewModel model)
        {
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (IsExistHash(model.Hash.HashCode))
                        return new ApiResult(true);
                    AddRequestHash(model.Hash.HashCode);

                    Guid? managerId = await BalancedRoundRobin();
                    if (managerId == null)
                        return new ApiResult(false, "На данный момент менеджеров нет.");

                    var cparty = AddCounterparty(model.Reg, managerId.Value);
                    if (IsExistUser(cparty.id))
                        return new ApiResult(false, "Такой пользователь уже существует.");

                    AddUser(cparty);
                    await _context.SaveChangesAsync();

                    await dbContextTransaction.CommitAsync();

                    return new ApiResult(true);
                }
                catch (Exception ex)
                {
                    await dbContextTransaction.RollbackAsync();
                    return new ApiResult(false, "Ошибка: Неправильно заполнены данные.");
                }
            }
        }

        private bool IsExistHash(string hashCode)
        {
            return _context.WebApiRequests.Any(x => x.request_hash == hashCode);
        }

        private void AddRequestHash(string hashCode)
        {
            WebApiRequest request = new WebApiRequest
            {
                id = Guid.NewGuid(),
                request_hash = hashCode
            };
            _context.WebApiRequests.Add(request);
        }

        private Counterparty AddCounterparty(UserRegistrationVM model, Guid managerId)
        {
            Counterparty cparty = _mapper.Map<Counterparty>(model);
            Counterparty existCParty = _context.Counterparties
                .FirstOrDefault(x => x.person == cparty.person && x.person_phone == cparty.person_phone);
            if (existCParty != null)
                return existCParty;

            cparty.manager_id = managerId;
            _context.Counterparties.Add(cparty);
            return cparty;
        }

        private bool IsExistUser(Guid cpartyId)
        {
            return _context.Users.Any(x => x.counterparty_id == cpartyId);
        }

        private void AddUser(Counterparty cparty)
        {
            string[] clientFullName = cparty.person.Split(" ");
            string fname = string.Concat(clientFullName[0].Select(c => TranslateHelper.Map[c]));
            User user = new User
            {
                id = Guid.NewGuid(),
                login = $"{fname}{cparty.person_phone}",
                password = HashHelper.TransformPassword(CreatePassword(8)),
                counterparty_id = cparty.id
            };
            var group = AndAddUserToGroup(user.id);
            user.acl = GetAclNumber(group.acl_id);
            _context.Users.Add(user);
        }

        private UserGroup AndAddUserToGroup(Guid userId)
        {
            var group = _context.UserGroups.FirstOrDefault(x => x.type == (int)UserType.Customer);
            UserGroupMembership membership = new UserGroupMembership
            {
                user_id = userId,
                group_id = group.id
            };
            _context.UserGroupMemberships.Add(membership);
            return group;
        }

        private int GetAclNumber(Guid aclId)
        {
            var acl = _context.AccessControls.Find(aclId);
            return acl.acl;
        }

        private string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

        private async Task<Guid?> BalancedRoundRobin()
        {
            long clientCounter = await UpdateClientCounterAsync();
            var orderedRates = _context.UserRates.Where(x => x.rate > GlobalConstant.Epsilon);
            if (!orderedRates.Any())
                return null;
            double remainder = 1 / orderedRates.Sum(x => x.rate);

            var anonRates = orderedRates.Select(x => new
            {
                Rate = Math.Round(x.rate * remainder, 4),
                UserId = x.user_id,
                RegDate = x.User.registration_date
            }).OrderBy(x => x.RegDate).ToList();

            double minRate = anonRates.Min(x => x.Rate);
            int maxValueCounter = anonRates.Count;

            int totalClientAmount = Convert.ToInt32(1 / minRate);
            double someValue = (clientCounter % (double)totalClientAmount) / totalClientAmount;
            someValue += minRate;

            double tempRate = 0;
            foreach (var item in anonRates)
            {
                tempRate += item.Rate;
                bool flag = (someValue - tempRate) <= GlobalConstant.Epsilon;
                if (flag)
                {
                    return item.UserId;
                }
            }
            return null;
        }

        private async Task<long> UpdateClientCounterAsync()
        {
            var counter = await _context.ClientCounters.FirstOrDefaultAsync();
            if (counter == null)
            {
                counter = new ClientCounter
                {
                    id = Guid.NewGuid(),
                    update_time_stamp = DateTime.Now,
                    counter = 1
                };
                _context.Add(counter);
                return counter.counter - 1;
            }
            else
            {
                counter.counter += 1;
                counter.update_time_stamp = DateTime.Now;
                _context.Update(counter);
                return counter.counter - 1;
            }
        }
    }
}
