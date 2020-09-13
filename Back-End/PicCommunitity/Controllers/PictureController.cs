using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PicCommunitity.Models;
using PicCommunitity.Services;
using PicCommunitity.ViewsModels;

namespace PicCommunitity.Controllers
{
    [ApiController]
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


        /// <summary>
        /// 创建评论或修改已有评论
        /// </summary>
        /// <param name="Comment"></param>
        /// <returns></returns>
        [Route("comment")]
        [HttpPost]
        public IActionResult comment([FromBody]comment Comment)
        {
            var nowComment = context.picComment.
                FirstOrDefault(c => c.u_id == Comment.userId && c.p_comment == Comment.picId);
            if (nowComment != null)
            {//当前评论已经存在，可更改评论内容
                nowComment.p_comment = Comment.content;
                context.picComment.Attach(nowComment);
                context.SaveChanges();
            }
            else
            {
                nowComment = new picComment
                {
                    u_id = Comment.userId,
                    p_id = Comment.picId,
                    p_comment = Comment.content,
                    likes = 0
                };
                context.picComment.Add(nowComment);
                context.SaveChanges();
            }
            return Ok(new
            {
                Success = true,
                msg = "Operation Done"
            });
        }


        /// <summary>
        /// 返回图片页面所需信息  图片来源及信息/关注/点赞/收藏
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="picId"></param>
        /// <returns></returns>
        [Route("getPicViewInfo")]
        [HttpGet]
        public IActionResult getPicViewInfo(string userId,string picId)
        {
            //获取关注状态
            var publisherfollow = false;
            var publishUserId = context.publishPicture.FirstOrDefault(p => p.p_id == picId).u_id;
            if(context.follow.FirstOrDefault
                (f => f.fans_id == userId && f.follow_id == publishUserId) != null) { publisherfollow = true; }
            //获取点赞状态
            var piclike = false;
            if (context.likesPicture.FirstOrDefault
                (l => l.u_id == userId && l.p_id == picId) != null){ piclike = true; }

            //获取收藏状态
            var picstar = false;
            if (context.favoritePicture.FirstOrDefault
                (f => f.u_id == userId && f.p_id == picId) != null) { picstar = true; }

            //获取图片信息
            var pic = services.getPicture(picId);

            //获取图片Tag
            var pictag = context.ownTag.ToLookup(t => t.p_id)[picId].ToArray();

            //组织返回信息
            return Ok(new
            {
                Success=true,
                picUrl = "url",
                publisherFollow = publisherfollow,
                publisherId=publishUserId,
                picLike = piclike,
                picStar = picstar,
                picHeight = pic.p_height,
                picWidth = pic.p_width,
                picInfo = pic.p_info,
                picTags = pictag
            }) ;
        }


        /// <summary>
        /// 获取当前图片所有评论
        /// </summary>
        /// <param name="picId"></param>
        /// <returns></returns>
        [Route("getComment")]
        [HttpGet]
        public IActionResult getComment(string picId)
        {
            var comments = context.picComment.ToLookup(c => c.p_id)[picId].ToList();
            //返回列表
            return Ok(new
            {
                Comments=comments
            });
        }
    }

}
