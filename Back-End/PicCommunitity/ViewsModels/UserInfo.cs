using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PicCommunitity.ViewsModels
{ 
    //UserName=user.u_name,
    //                Name = userInfo.u_name,
    //                Birthday = userInfo.birthday,
    //                Email = userInfo.mail,
    //                Message = userInfo.message,
    public class UserInfo
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string Email { get; set; }
        public string  Message { get; set; }
    }
}
