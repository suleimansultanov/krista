using KristaShop.Business.Interfaces;
using KristaShop.Common.Extensions;
using KristaShop.Common.Models;
using KristaShop.DataReadOnly.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KristaShop.WebUI.Controllers
{
    [Authorize]
    public class FeedbackController : Controller
    {
        private readonly IFeedbackService _fbService;

        public FeedbackController(IFeedbackService fbService)
        {
            _fbService = fbService;
        }

        public IActionResult Index() => View();

        public async Task<IActionResult> LoadData() => Ok(await _fbService.GetFeedbacks());

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (!ModelState.IsValid)
            {
                var errors = string.Join(" | ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                return BadRequest(OperationResult.AlertFailure($"Неверно заполнены данные! {errors}"));
            }
            var user = HttpContext.Session.Get<UserDTO>(GlobalConstant.SessionKeys.User);
            var result = await _fbService.UpdateFeedback(id, user.UserId);
            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result);
        }
    }
}