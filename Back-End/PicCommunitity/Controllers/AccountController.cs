using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PicCommunitity.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        [Route("login")]
        [HttpGet]
        public JsonResult login()
        {
            return Json(new { name="test"});
        }
        [Route("register")]
        [HttpGet]
        public IActionResult register()
        {
            return Json(new { status="创建成功"});
        }
    }
}
