using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using picCommunity.Models;

namespace picCommunity.Controllers
{
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private IUsersRepository _usersRepository;
        public HomeController(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }
        [Route("~/Home")]
        [Route("~/")]
        [Route("/Home/Index")]
        public IActionResult Index()
        {
            var model = _usersRepository.GetAllUsers();
            var now = model.First();
            return View(model);
            //return Json(new
            //{
            //    id = now.u_id,
            //    password = now.u_password,
            //    name = now.u_name,
            //    type = now.u_type,
            //    status = now.u_status,
            //    createTime = now.create_time
            //});
        }
    }
}
