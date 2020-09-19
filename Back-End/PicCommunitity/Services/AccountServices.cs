using Microsoft.Extensions.Options;
using PicCommunitity.Models;
using PicCommunitity.ViewsModel;
using PicCommunitity.ViewsModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PicCommunitity.Services
{
    public class AccountServices
    {
        private AppDbContext context;
        private JwtSetting _jwtSetting;

        public AccountServices(AppDbContext context,IOptions<JwtSetting> options)
        {
            this.context = context;
            _jwtSetting = options.Value;
        }
        
        //查找当前是否存在用户
        public bool checkExist(string userName,string password)
        {
            var user = context.users.FirstOrDefault(u => u.u_name == userName && u.u_password == password);
            if (user != null)
            {
                return true;
            }
            return false;
        }
        //根据用户信息获取Token
        public string GetToken(LoginUser user)
        {
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("name", user.userName) // 用户名
            };
            //创建令牌
            var token = new JwtSecurityToken(
                issuer: _jwtSetting.Issuer,
                audience: _jwtSetting.Audience,
                signingCredentials: _jwtSetting.Credentials,
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddSeconds(_jwtSetting.ExpireSeconds)
            );
            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return jwtToken;
        }
        //获取对应id的User，不存在则返回Null
        public users GetUserById(string userId)
        {
            return context.users.FirstOrDefault(u => u.u_id == userId);
        }
        
        //根据id取得对应User详细信息
        public userInfo GetUserInfoById(string userId)
        {
            
            return context.userInfo.FirstOrDefault(u => u.u_id == userId);
        }

        //根据id对应的钱包
        public wallet GetWalletById(string userId)
        {
            return context.wallet.FirstOrDefault(w => w.u_id == userId);
        }

        //获取个人图片主页的数据信息
        public ProfileInfo GetProfileInfoById(string userId)
        {
            
            var StarNum = 0;var LikeNum = 0;var CommentNum = 0;var FollowNum = 0;

            #region 获取图片相关各类数据
            var picGroup = context.publishPicture.ToLookup(p => p.u_id)[userId].ToList();
            foreach (var pic in picGroup)
            {
                StarNum += context.favoritePicture.Count(f => f.p_id == pic.p_id);
                LikeNum += context.picture.Find(pic.p_id).likes;
            }
            #endregion

            #region 获取个人相关各类数据
            CommentNum += context.picComment.Count(p => p.u_id == userId);
            FollowNum += context.follow.Count(f => f.follow_id == userId);
            #endregion

            return new ProfileInfo {
                starNum=StarNum,
                likeNum=LikeNum,
                followNum=FollowNum,
                commentNum=CommentNum
            };
        }
        public int getPicNum()
        {
            return context.tableCount.Find(1).picture;
        }
        public int getUserNum()
        {
            return context.tableCount.Find(1).users;
        }
        public int getBlogNum()
        {
            return context.tableCount.Find(1).blog;
        }
    }
}
