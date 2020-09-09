using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PicCommunitity.Models;
using PicCommunitity.Services;
using PicCommunitity.ViewsModel;

namespace PicCommunitity.Controllers
{
    [ApiController]
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
        /// <param name="user"></param>
        /// <returns></returns>
        [Route("login")]
        [HttpPost]
        public JsonResult login([FromBody]LoginUser user)
        {
            return Json(new { 
                status=services.checkExist(user.userName,user.userPassword),
                msg="你好，用户"+user.userName+"欢迎回到图片社区"
            });
        }
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPassword"></param>
        /// <returns></returns>
        [Route("register")]
        [HttpPost]
        public users register(string userName,string userPassword)
        {
            var user = new users { };
            user.u_id = (context.users.Count() + 1).ToString();
            user.u_name = userName;
            user.u_password = userPassword;
            user.u_status = "AC";
            user.u_type = "US";
            user.create_time = DateTime.Now;
            context.users.Add(user);
            context.SaveChanges();
            return user;
        }
        /// <summary>
        /// 获取当前所有用户信息
        /// </summary>
        /// <returns></returns>
        [Route("getAllUsers")]
        [HttpGet]
        public IEnumerable<users> getAllUsers()
        {
            return context.users.ToArray();
        }
    }
}
