using KristaShop.Business.Interfaces;
using KristaShop.Common.Extensions;
using KristaShop.Common.Helpers;
using KristaShop.Common.Models;
using KristaShop.DataReadOnly.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KristaShop.WebUI.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class PermissionFilter : Attribute, IAsyncAuthorizationFilter
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;
        private readonly IUrlAclService _urlAclService;

        public PermissionFilter
            (IHttpContextAccessor httpContextAccessor, IUrlAclService urlAclService)
        {
            _httpContextAccessor = httpContextAccessor;
            _urlAclService = urlAclService;
            _session = _httpContextAccessor.HttpContext.Session;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var loginRoute = new RouteValueDictionary(new { controller = "Identity", action = "Login" });
            var forbiddenRoute = new RouteValueDictionary(new { controller = "Error", action = "Error403" });
            var notFoundRoute = new RouteValueDictionary(new { controller = "Error", action = "Error404" });
            var descriptor = context?.ActionDescriptor as ControllerActionDescriptor;
            string controllerName = descriptor.ControllerName;
            string actionName = descriptor.ActionName;

            var user = _session.Get<UserDTO>(GlobalConstant.SessionKeys.User);
            if (user == null)
            {
                context.Result = new RedirectToRouteResult(loginRoute);
                return;
            }
            else if (user.IsRoot)
                return;
            
            var urlAcls = (await _urlAclService.GetUrlAcls())
                .Where(x => UrlHelper.CompareUrls(x.URL, UrlHelper.GetURL(controllerName, actionName)))
                .OrderBy(x => x.Acl)
                .ToList();
            var urlAcl = urlAcls.FirstOrDefault();
            if (urlAcl == null)
            {
                context.Result = new RedirectToRouteResult(notFoundRoute);
                return;
            }
            var allowedGroups = urlAcl.AccessGroupsJson;
            var deniedGroups = urlAcl.DeniedGroupsJson;

            bool isDenied = deniedGroups.Any(dg => user.UserGroups.Any(ug => ug.GroupId == dg));

            if (isDenied)
            {
                context.Result = new RedirectToRouteResult(forbiddenRoute);
                return;
            }

            bool isAllowed = urlAcl.Acl <= user.AccessLevel
                ? true
                : allowedGroups.Any(dg => user.UserGroups.Any(ug => ug.GroupId == dg));

            if (isAllowed)
                return;
            else
            {
                context.Result = new RedirectToRouteResult(forbiddenRoute);
                return;
            }
        }
    }
}
