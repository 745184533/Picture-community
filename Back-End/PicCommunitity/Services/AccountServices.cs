using PicCommunitity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PicCommunitity.Services
{
    public class AccountServices
    {
        private AppDbContext context;

        public AccountServices(AppDbContext context)
        {
            this.context = context;
        }
        
        public bool checkExist(string userName,string password)
        {
            var user = context.users.FirstOrDefault(u => u.u_name == userName && u.u_password == password);
            if (user != null)
            {
                return true;
            }
            return false;
        }
        
    }
}
