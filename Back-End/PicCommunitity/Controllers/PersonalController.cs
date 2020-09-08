using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PicCommunitity.Controllers
{
    public class PersonalController : Controller
    {
        [Route("/Personal/Picture")]
        public IActionResult Picture()
        {
            return View();
        }
    }
}
