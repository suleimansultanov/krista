using KristaShop.Common.Models;
using System.Threading.Tasks;

namespace KristaShop.Business.Interfaces
{
    public interface IAsupApiClient<T>
    {
        Task<ApiResult> SendDataAsync(T data, string urlPath);
    }
}
