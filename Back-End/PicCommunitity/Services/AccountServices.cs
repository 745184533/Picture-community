using Microsoft.Extensions.Options;
using PicCommunitity.Models;
using PicCommunitity.ViewsModel;
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
    }
}
