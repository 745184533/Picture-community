using PicCommunitity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PicCommunitity.Services
{
    public class WalletServices
    {
        private AppDbContext context;

        public WalletServices(AppDbContext context)
        {
            this.context = context;
        }
    }
}
