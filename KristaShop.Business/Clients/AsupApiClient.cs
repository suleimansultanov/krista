using KristaShop.Business.Interfaces;
using KristaShop.Common.Extensions;
using KristaShop.Common.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace KristaShop.Business.Clients
{
    public class AsupApiClient<T> : IAsupApiClient<T> 
        where T : class
    {
        private readonly HttpClient _httpClient;

        public AsupApiClient(HttpClient httpClient)
        {
            httpClient.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Sample");
            _httpClient = httpClient;
        }

        public async Task<ApiResult> SendDataAsync(T data, string urlPath)
        {
            _httpClient.BaseAddress = new Uri("https://localhost:44356/" + urlPath);
            var response = await _httpClient.PostAsJsonAsync(data);
            return await response.Content.ReadAsJsonAsync<ApiResult>();
        }
    }
}
