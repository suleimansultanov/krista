using AutoMapper;
using KristaShop.Business.DTOs;
using KristaShop.Business.Interfaces;
using KristaShop.Common.Models;
using KristaShop.WebUI.Filters;
using KristaShop.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KristaShop.WebUI.Controllers
{
    [Authorize]
    [ServiceFilter(typeof(PermissionFilter))]
    public class SettingController : Controller
    {
        private readonly ISettingService _settingService;
        private readonly IMapper _mapper;

        public SettingController(ISettingService settingService, IMapper mapper)
        {
            _settingService = settingService;
            _mapper = mapper;
        }

        public IActionResult Index() => View();

        public async Task<IActionResult> LoadData() => Ok(await _settingService.GetSettings());

        [HttpPost]
        public async Task<IActionResult> Create(SettingCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = string.Join(" | ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                return BadRequest(OperationResult.AlertFailure($"Неверно заполнены данные! {errors}"));
            }
            var dto = _mapper.Map<SettingDTO>(model);
            var result = await _settingService.InsertSetting(dto);
            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result);
        }

        public async Task<IActionResult> Details(Guid id) => Ok(await _settingService.GetSettingDetails(id));

        [HttpPost]
        public async Task<IActionResult> Edit(SettingEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = string.Join(" | ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                return BadRequest(OperationResult.AlertFailure($"Неверно заполнены данные! {errors}"));
            }
            var dto = _mapper.Map<SettingDTO>(model);
            var result = await _settingService.UpdateSetting(dto);
            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id) => Ok(await _settingService.DeleteSetting(id));
    }
}