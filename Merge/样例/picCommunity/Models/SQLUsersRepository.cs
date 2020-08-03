using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace picCommunity.Models
{
    public class SQLUsersRepository:IUsersRepository
    {
        private AppDbContext context;
        public SQLUsersRepository(AppDbContext context)
        {
            this.context = context;
        }

        public Users Add(Users user)
        {
            user.u_id = (context.Users.Count()+1).ToString();
            user.u_status = "AC";
            context.Users.Add(user);
            context.SaveChanges();
            return user;
        }

        public Users Delete(int id)
        {
            return null;
        }

        public IEnumerable<Users> GetAllUsers()
        {
            return context.Users;
        }

        public Users GetUsers(int id)
        {
            return context.Users.Find(id);
        }

        public Users Update(Users userChange)
        {
            return null;
        }
    }
}
