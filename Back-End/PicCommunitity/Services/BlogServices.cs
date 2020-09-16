using PicCommunitity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PicCommunitity.Services
{
    public class BlogServices
    {
        private AppDbContext context;

        public BlogServices(AppDbContext context)
        {
            this.context = context;
        }
    }
}
