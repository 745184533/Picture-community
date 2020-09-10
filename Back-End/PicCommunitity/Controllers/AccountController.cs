using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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
        public AccountController(AppDbContext context,IOptions<JwtSetting> options)
        {
            this.context = context;
            this.services = new AccountServices(context,options);
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [Route("login")]
        [HttpPost]
        public IActionResult login([FromBody]LoginUser user)
        {
            if (services.checkExist(user.userName, user.userPassword))
            {//登录成功

                //颁发Token
                var token = services.GetToken(user);
                return Ok(new
                {
                    Success = true,
                    Token = token,
                    Type= "Bearer"
                });
            }
            return Ok(new
            {
                Success = false
            }) ;
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
        /// 测试Swagger
        /// </summary>
        /// <returns></returns>
        [Route("test")]
        [HttpGet]
        public bool test()
        {
            return true;
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
