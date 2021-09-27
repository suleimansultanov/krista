using KristaShop.Common.Models;
using KristaShop.WebAPI.Interfaces;
using KristaShop.WebAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace KristaShop.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _service;

        public IdentityController
            (IIdentityService service)
        {
            _service = service;
        }

        [HttpGet]
        public string Get()
            => "Все работает";


        [HttpPost]
        public async Task<ApiResult> Post(RegHashViewModel model)
        {
            var modelJson = model.Reg.ToJson();
            string hashcode = modelJson.ComputeSha256Hash();
            if (model.Hash.HashCode != hashcode)
                return new ApiResult(false, "Неудача!");
            var result = await _service.UserRegistrate(model);
            return result;
        }
    }
}