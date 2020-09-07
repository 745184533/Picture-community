using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PicCommunitity.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        [Route("/Account/login")]
        public IActionResult login()
        {
            return View();
        }
        [Route("/Account/register")]
        public IActionResult register()
        {
            return View();
        }
    }
}
