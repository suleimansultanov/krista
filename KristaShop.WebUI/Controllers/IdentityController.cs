using KristaShop.Business.Interfaces;
using KristaShop.Common.Extensions;
using KristaShop.Common.Models;
using KristaShop.DataReadOnly.DTOs;
using KristaShop.DataReadOnly.Interfaces;
using KristaShop.WebUI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KristaShop.WebUI.Controllers
{
    [Authorize]
    public class IdentityController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILinkService _linkService;
        private readonly IDictionaryService _dictService;
        private readonly IAsupApiClient<RegHashViewModel> _apiIdentityClient;

        public IdentityController
            (IUserService userService, ILinkService linkService, IDictionaryService dictService, IAsupApiClient<RegHashViewModel> apiIdentityClient)
        {
            _userService = userService;
            _linkService = linkService;
            _dictService = dictService;
            _apiIdentityClient = apiIdentityClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LoadData()
        {
            UserDTO user = HttpContext.Session.Get<UserDTO>(GlobalConstant.SessionKeys.User);
            return Ok(_userService.GetAllUsers(user));
        }

        [HttpPost]
        public async Task<IActionResult> CreateLink(Guid userId)
        {
            string link = await _linkService.InsertLinkAuth(userId);
            if (string.IsNullOrEmpty(link))
                return BadRequest("Не удалось!");
            return Ok(link);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var result = await _userService.GetUserByLoginAndPass(model.UserName, model.Password);
            if (!result.Item2.IsOK)
            {
                ModelState.AddModelError("", result.Item2.Description);
                HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                return View();
            }
            await SetClaimsAsync(result.Item1, false);
            return RedirectToAction("Index", "Home");
        }

        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> LoginByLink(string rh)
        {
            var userId = await _linkService.GetUserIdByRandCode(rh);
            if (userId == null)
                return RedirectToAction("Error403", "Error");

            var result = await _userService.SignInByLink(userId.Value);
            if (!result.Item2.IsOK)
            {
                ModelState.AddModelError("", result.Item2.Description);
                HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                return RedirectToAction("Error403", "Error");
            }
            await HttpContext.SignOutAsync();
            HttpContext.Session.Clear();
            await SetClaimsAsync(result.Item1, false);
            return RedirectToAction("Index", "Home");
        }

        private async Task SetClaimsAsync(UserDTO user, bool isPersistent)
        {
            HttpContext.Session.Set(GlobalConstant.SessionKeys.User, user);
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Name, user.Login));
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
              new AuthenticationProperties
              {
                  ExpiresUtc = isPersistent ? DateTime.UtcNow.AddDays(1) : (DateTime?)null,
                  IsPersistent = isPersistent
              });
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Login));
        }

        public IActionResult Register()
        {
            ViewBag.Cities = new SelectList(_dictService.GetCities(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegHashViewModel model)
        {
            var modelJson = model.Reg.ToJson();
            model.Hash = new BaseHashModel { HashCode = modelJson.ComputeSha256Hash() };
            var result = await _apiIdentityClient.SendDataAsync(model, GlobalConstant.URLs.PostUserData);
            if (result.IsOK)
                return Ok(OperationResult.AlertSuccess(result.Description));
            else
                return BadRequest(OperationResult.AlertFailure(result.Description));
        }
    }
}