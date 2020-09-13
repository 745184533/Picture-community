using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PicCommunitity.Models;
using PicCommunitity.Services;

namespace PicCommunitity.Controllers
{
    [Route("[controller]")]
    public class PictureController : Controller
    {
        private AppDbContext context;
        private PictureServices services;

        public PictureController(AppDbContext context)
        {
            this.context = context;
            services = new PictureServices(context);
        }

        /// <summary>
        /// 点击进行赞或者已有赞取消赞
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="picId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [Route("likePicture")]
        [HttpPost]
        public IActionResult likePicute(string userId,string picId,string type)
        {
            //首先查找是否有现有的点赞
            var like=services.getLikes(userId, picId);
            var pic = services.getPicture(picId);
            if (like != null)//现在已经有 赞
            {
                //点击类型相同，取消赞
                
                context.likesPicture.Remove(like);
                pic.likes--;//原有的赞减少
                context.picture.Attach(pic);
                context.SaveChanges();
            }
            else
            {//创建新的赞
                like = new likespicture
                {
                    u_id = userId,
                    p_id = picId,
                    like_time = DateTime.Now,
                    like_type = type
                };
                context.likesPicture.Add(like);
                pic.likes++;//原有的赞增加
                context.Attach(pic);
                context.SaveChanges();
            }
            return Ok(new
            {
                Success = true,
                msg = "Operation Done"
            }) ;
        }
        

        /// <summary>
        /// 进行收藏或已有收藏取消收藏
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="picId"></param>
        /// <returns></returns>
        [Route("favoritePicture")]
        [HttpPost]
        public IActionResult favoritePicture(string userId,string picId)
        {
            var favorite = context.favoritePicture.FirstOrDefault(f => f.p_id == picId);
            if (favorite != null)
            {//已有收藏
                context.favoritePicture.Remove(favorite);
                context.SaveChanges();
            }
            else
            {//创建新的收藏
                favorite = new favoritePicture
                {
                    u_id = userId,
                    p_id = picId,
                    fav_time = DateTime.Now
                };
                context.favoritePicture.Add(favorite);
                context.SaveChanges();
            }
            return Ok(new
            {
                Success=true,
                msg="Operation Done"
            });
        }



    }
}
