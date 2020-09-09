using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using picCommunity.Models;
using picCommunity.ViewModels;

namespace picCommunity.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private IUsersRepository _usersRepository;

        public AccountController(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }
        [Route("~/Account/Register")]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new Users
                {
                    u_name = model.u_name,
                    u_password = model.u_password,
                    u_type = model.u_type
                };
                _usersRepository.Add(user);
                return RedirectToAction("Index", "Home");
            }
            return View(model);
            
        }
    }
}
