using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PicCommunitity.Models;
using PicCommunitity.Services;
using PicCommunitity.ViewsModel;
using PicCommunitity.ViewsModels;

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
        public async Task<IActionResult> register([FromBody]RegisterUser RUser)
        {
            if (!services.checkExist(RUser.userName, RUser.userPassword))
            {//没有该账号密码存在
                //创建user
                var user = new users { };
                user.u_id = (context.users.Count() + 1).ToString();
                user.u_name = RUser.userName;
                user.u_password = RUser.userPassword;
                user.u_status = "AC";
                user.u_type = "US";
                user.create_time = DateTime.Now;
                context.users.Add(user);
                await context.SaveChangesAsync();

                //创建相应userInfo
                var newUserInfo = new userInfo
                { 
                    u_id = user.u_id,
                    u_name = user.u_name,
                    birthday = Convert.ToDateTime("1800-01-01 00:00:00"),
                    mail = RUser.Email,
                    phone_number = "0",
                    message=""
                };
                context.userInfo.Add(newUserInfo);
                await context.SaveChangesAsync();

                //创建相应钱包
                var newUserWallet = new wallet
                {
                    u_id = user.u_id,
                    buy_num = 0,
                    coin = 0,
                    fund = 0,
                    publish_num = 0
                };
                context.wallet.Add(newUserWallet);
                await context.SaveChangesAsync();

                //完成了注册赋予Token
                var token = services.GetToken(
                    new LoginUser { userName = user.u_name, userPassword = user.u_password });
                return Ok(new
                {
                    Success = true,
                    Token=token,
                    msg="New User Registered"
                });
            }
            else
            {//已有该账号密码存在
                return Ok(new
                {
                    Success = false,
                    msg = "Same UserName&Password"
                });
            }
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


        /// <summary>
        /// 获得对应id的用户详细信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        //[Authorize]
        [Route("getUserInfo")]
        [HttpGet]
        public IActionResult getUserInfo(string userId)
        {
            //首先判断是否存在该Id
            var user = services.GetUserById(userId);
            if (user != null)
            {//用户存在
                var userInfo = services.GetUserInfoById(userId);
                if (userInfo == null)
                {//如果此时没有userInfo
                    return Ok(new
                    {
                        Success = false,
                        msg = "No such User"
                    });
                }
                return Ok(new
                {
                    Sucess = true,
                    //Picture=
                    UserName=user.u_name,
                    Name = userInfo.u_name,
                    Birthday = userInfo.birthday,
                    Email = userInfo.mail,
                    Message = userInfo.message,
                });
            }
            else
            {//用户不存在
                return Ok(new
                {
                    Success = false,
                    msg = "No such User"
                });
            }
        }

        /// <summary>
        /// 获得用户个人图片主页信息，点赞数等
        /// </summary>
        //[Authorize]
        [Route("getProfileInfo")]
        [HttpGet]
        public IActionResult getProfileInfo(string userId)
        {
            var info = services.GetProfileInfoById(userId);
            return Ok(new
            {
                Success=true,
                StarNum=info.starNum,
                LikeNum=info.likeNum,
                CommentNum=info.commentNum,
                FollowNum=info.followNum
            });
        }

        /// <summary>
        /// 关注用户或已关注取消关注用户
        /// </summary>
        /// <param name="fansId"></param>
        /// <param name="followId"></param>
        /// <returns></returns>
        [Route("followUser")]
        [HttpPost]
        public IActionResult followUser(string fansId, string followId)
        {
            var follow = context.follow.FirstOrDefault(f => f.fans_id == fansId && f.follow_id == followId);
            if (follow != null)
            {//已经关注，则取消关注
                context.follow.Remove(follow);
                context.SaveChanges();
            }
            else
            {
                follow = new follow
                {
                    fans_id = fansId,
                    follow_id = followId
                };
                context.follow.Add(follow);
                context.SaveChanges();
            }
            return Ok(new
            {
                Success = true,
                msg = "Operation Done"
            });
        }


    }
}
