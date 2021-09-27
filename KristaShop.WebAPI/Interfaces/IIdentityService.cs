using KristaShop.Common.Models;
using KristaShop.WebAPI.ViewModels;
using System.Threading.Tasks;

namespace KristaShop.WebAPI.Interfaces
{
    public interface IIdentityService
    {
        Task<ApiResult> UserRegistrate(RegHashViewModel model);
    }
}
