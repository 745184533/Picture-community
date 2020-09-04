using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PicCommunitity.Models;

namespace PicCommunitity.Controllers
{
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private IDBRepository dbRepository;

        public HomeController(IDBRepository dbRepository)
        {
            this.dbRepository = dbRepository;
        }
        [Route("~/")]
        [Route("~/Home")]
        [Route("/Home/Index")]
        public IActionResult Index()
        {
            var textBlog = dbRepository.GetFirstBlog();
            return View(textBlog);
        }
    }
}
