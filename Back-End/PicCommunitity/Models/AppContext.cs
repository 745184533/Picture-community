using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PicCommunitity.Models
{
    public class AppContext:DbContext
    {
        public AppContext(DbContextOptions<AppContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           // modelBuilder.Entity<buyhistory>().HasKey(t => new { t.u_id, t.E_id, t.buy_time });

            //modelBuilder.Entity<shopcart>().HasKey(t => new { t.u_id, t.E_id });

            base.OnModelCreating(modelBuilder);
        }

    }
}
