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

        //获取对应id的User，不存在则返回Null
        public users GetUserById(string userId)
        {
            return context.users.FirstOrDefault(u => u.u_id == userId);
        }

        //获取某个用户对应的点赞信息
        public likespicture getLikes(string userId,string picId)
        {
            return context.likesPicture.FirstOrDefault(l => l.u_id == userId && l.p_id == picId);
        }

        //获取图片
        public picture getPicture(string picId)
        {
            return context.picture.FirstOrDefault(p=>p.p_id==picId);
        }

        //获取图片的总收藏数
        public int getPicStarNum(string picId)
        {
            return context.favoritePicture.Count(f => f.p_id == picId);
        }

        //获取图片总的评论数
        public int getPicCommentNum(string picId)
        {
            return context.picComment.Count(p => p.p_id == picId);
        }
    }
}
