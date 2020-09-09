using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using PicCommunitity.Models;
using PicCommunitity.Services;
using PicCommunitity.ViewsModel;
using PicCommunitity.ViewsModels;

namespace PicCommunitity.Controllers
{
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private AppDbContext context;
        private AccountServices services;
        public AccountController(AppDbContext context)
        {
            this.context = context;
            this.services = new AccountServices(context);
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <returns></returns>
        [Route("login")]
        [HttpGet]
        public IActionResult login()
        {
            return View();
        }
        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> login(LoginUser Luser,string returnUrl)
        {
            if (services.checkExist(Luser.userName, Luser.userPassword))
            {
                var claims = new[] { new Claim("UserName", Luser.userName) };
                var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                ClaimsPrincipal user = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync("Cookies",
                       user, new AuthenticationProperties()
                       {
                           IsPersistent = true,
                           ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
                           AllowRefresh = true
                       });
                ViewBag.logined = true;
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(Luser);
            }
        }
        [Route("logout")]
        [HttpGet]
        public async Task<IActionResult> logout()
        {
            await HttpContext.SignOutAsync("Cookies");
            ViewBag.logined = false;
            return RedirectToAction("Index", "Home");
        }
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <returns></returns>
        [Route("register")]
        [HttpGet]
        public IActionResult register()
        {
            return View();
        }

        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> register(registerUser Ruser)
        {
            if (!services.checkExist(Ruser.userName, Ruser.userPassword))
            {
                var newUser = new users { };
                newUser.u_id = (context.users.Count() + 1).ToString();
                newUser.u_name = Ruser.userName;
                newUser.u_password = Ruser.userPassword;
                newUser.u_status = "AC";
                newUser.u_type = "US";
                newUser.create_time = DateTime.Now;
                context.users.Add(newUser);
                context.SaveChanges();
                var claims = new[] { new Claim("UserName", Ruser.userName) };
                var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                ClaimsPrincipal user = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync("Cookies",
                       user, new AuthenticationProperties()
                       {
                           IsPersistent = true,
                           ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
                           AllowRefresh = true
                       });
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "已有相同账号密码存在");
                return View(Ruser);
            }
        }
    }
}
