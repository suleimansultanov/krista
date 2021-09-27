using KristaShop.Business.Interfaces;
using KristaShop.Common.Helpers;
using KristaShop.Common.Models;
using KristaShop.DataAccess.Entities;
using KristaShop.DataAccess.Interfaces;
using System;
using System.Threading.Tasks;

namespace KristaShop.Business.Services
{
    public class LinkService : ILinkService
    {
        private readonly IShopRepository<AuthorizationLink> _linkRepo;

        public LinkService(IShopRepository<AuthorizationLink> linkRepo)
        {
            _linkRepo = linkRepo;
        }

        public async Task<string> InsertLinkAuth(Guid userId, bool oneTime = false)
        {
            var link = new AuthorizationLink
            {
                id = Guid.NewGuid(),
                record_time_stamp = DateTime.Now,
                user_id = userId,
                valid_to = oneTime ? (DateTime?)null : DateTime.Now.AddDays(3),
                random_code = HashHelper.CalculateMD5Hash(Guid.NewGuid().ToString())
            };

            await _linkRepo.AddAsync(link);
            if (link == null)
                return string.Empty;

            return $"https://localhost:44378/?randh={link.random_code}";
        }

        public async Task<Guid?> GetUserIdByRandCode(string code)
        {
            var link = await _linkRepo.FindByFilterAsync(x => x.random_code == code);
            bool IsNotValid = (link?.valid_to == null && link?.login_date != null) || (link?.valid_to > DateTime.Now);
            if (!IsNotValid)
                return null;

            link.login_date = DateTime.Now;
            await _linkRepo.UpdateAsync(link);
            return link.user_id;
        }
    }
}
