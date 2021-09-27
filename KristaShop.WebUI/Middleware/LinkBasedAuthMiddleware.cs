using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace KristaShop.WebUI.Middleware
{
    public class LinkBasedAuthMiddleware
    {
        private readonly RequestDelegate _next;

        public LinkBasedAuthMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var randh = context.Request.Query["randh"];
            if (!string.IsNullOrEmpty(randh.ToString()))
            {
                context.Response.Redirect("/Identity/LoginByLink?rh=" + randh);
            }
            else
            {
                await _next.Invoke(context);
            }
        }
    }
}
