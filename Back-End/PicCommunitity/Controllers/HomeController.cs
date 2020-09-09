using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PicCommunitity.Models;

namespace PicCommunitity.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {

        public HomeController()
        {
        }
        [Route("Index")]
        [HttpGet]
        public blog Index()
        {
            return null;
        }
    }
}
