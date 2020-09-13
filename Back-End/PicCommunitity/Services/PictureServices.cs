using PicCommunitity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PicCommunitity.Services
{
    public class PictureServices
        
    {
        private AppDbContext context;
        public PictureServices(AppDbContext context)
        {
            this.context = context;
        }
        
        public likespicture getLikes(string userId,string picId)
        {
            return context.likesPicture.FirstOrDefault(l => l.u_id == userId && l.p_id == picId);
        }
        public picture getPicture(string picId)
        {
            return context.picture.FirstOrDefault(p=>p.p_id==picId);
        }
    }
}
