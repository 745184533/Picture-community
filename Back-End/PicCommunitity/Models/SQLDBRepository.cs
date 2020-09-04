using PicCommunitity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PicCommunitity.Models
{
    public class SQLDBRepository : IDBRepository
    {
        private AppDbContext context;

        public SQLDBRepository(AppDbContext context)
        {
            this.context = context;
        }
        public blog GetFirstBlog()
        {
            return context.blog.First();
        }
    }
}
