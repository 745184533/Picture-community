using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace picCommunity.Models
{
    public interface IUsersRepository
    {
        Users GetUsers(int id);
        IEnumerable<Users> GetAllUsers();
        Users Add(Users user);
        Users Update(Users userChange);
        Users Delete(int id);
    }
}
