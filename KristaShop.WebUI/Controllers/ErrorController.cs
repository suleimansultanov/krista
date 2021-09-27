using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KristaShop.WebUI.Controllers
{
    [Authorize]
    public class ErrorController : Controller
    {
        public IActionResult Error403()
        {
            return View();
        }

        public IActionResult Error404()
        {
            return View();
        }
    }
}