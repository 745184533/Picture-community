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
        private IDBRepository dbRepository;

        public HomeController(IDBRepository dbRepository)
        {
            this.dbRepository = dbRepository;
        }
        [Route("Index")]
        [HttpGet]
        public blog Index()
        {
            var textBlog = dbRepository.GetFirstBlog();
            return textBlog;
        }
    }
}
