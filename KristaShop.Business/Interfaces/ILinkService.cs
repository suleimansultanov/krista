using KristaShop.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KristaShop.Business.Interfaces
{
    public interface ILinkService
    {
        Task<string> InsertLinkAuth(Guid userId, bool oneTime = false);
        Task<Guid?> GetUserIdByRandCode(string code);
    }
}
