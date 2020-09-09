using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PicCommunitity.Models;

namespace PicCommunitity.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class HomeController : Controller
    {

        public HomeController()
        {
        }
        [Route("Index")]
        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                ViewBag.UserName = HttpContext.User.Claims.First().Value;
                ViewBag.logined = true;
            }
            else ViewBag.logined = false;
            return View();
        }
    }
}
