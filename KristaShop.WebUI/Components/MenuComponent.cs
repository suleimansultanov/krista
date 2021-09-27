using KristaShop.Business.Interfaces;
using KristaShop.Common.Enums;
using KristaShop.Common.Extensions;
using KristaShop.Common.Helpers;
using KristaShop.Common.Models;
using KristaShop.DataReadOnly.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KristaShop.WebUI.Components
{
    public class MenuComponent : ViewComponent
    {
        private readonly IMenuService _menuService;
        private readonly IUrlAclService _urlAclService;

        public MenuComponent(IMenuService menuService, IUrlAclService urlAclService)
        {
            _menuService = menuService;
            _urlAclService = urlAclService;
        }

        public IViewComponentResult Invoke(MenuType menuType)
        {
            var user = HttpContext.Session.Get<UserDTO>(GlobalConstant.SessionKeys.User);
            var menuItems = _menuService.GetMenusByType(menuType);
            if (!user.IsRoot)
            {
                var urls = _urlAclService.GetUrlAclsByAcl(user.AccessLevel).Select(x => x.URL).ToList();
                menuItems = menuItems
                    .Where(x => urls.Any(u => UrlHelper.CompareUrls(u, UrlHelper.GetURL(x.ControllerName, x.ActionName)))).ToList();
            }
            return View(menuItems.OrderBy(x => x.Order).ToList());
        }
    }
}
