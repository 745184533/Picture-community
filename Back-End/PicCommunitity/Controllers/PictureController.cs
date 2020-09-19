using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        ////[Authorize]
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
        ////[Authorize]
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
        ////[Authorize]
        [Route("comment")]
        [HttpPost]
        public IActionResult comment([FromBody]comment Comment)
        {
            var nowComment = context.picComment.
                FirstOrDefault(c => c.u_id == Comment.userId && c.p_id == Comment.picId);
            if (nowComment != null)
            {//当前评论已经存在，可更改评论内容
                context.Entry(nowComment).CurrentValues.SetValues(new picComment
                {
                    u_id = nowComment.u_id,
                    p_id = nowComment.p_id,
                    likes = nowComment.likes,
                    p_comment = Comment.content
                });
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
            //是否下载
            var HasDownload = false;
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

            //点赞数、收藏数
            var numberlike = context.likesPicture.Count(predicate => predicate.p_id == picId);
            var numberfavorite = context.favoritePicture.Count(predicate => predicate.p_id == picId);

            //获取图片信息、图片下载量、图片作者、上传时间。
            var pic = services.getPicture(picId);
            var Download = context.download.Count(i => i.p_id == picId);
            var LiUp=context.publishPicture.FirstOrDefault(i => i.p_id == picId);


            var uploadTime = LiUp.publish_time;
            var LiName = context.users.FirstOrDefault(i => i.u_id == LiUp.u_id);


            //获取图片Tag
            var pictag = context.ownTag.ToLookup(t => t.p_id)[picId].ToArray();

            //获取评论信息
            var piccomment = "";
            var tmpComment = context.picComment.FirstOrDefault(c => c.u_id == userId && c.p_id == picId);
            if (tmpComment != null) { piccomment = tmpComment.p_comment; }

            //是否下载
            if(context.download.FirstOrDefault(c=>c.p_id==picId&&c.u_id==userId)!=null)
            {
                HasDownload = true;
            }



            //组织返回信息
            return Ok(new
            {
                Success=true,
                Message="numberLike,numberFavorite,Downloads分别是点赞量，收藏量，下载量",
                picUrl = pic.p_url,

                publisherFollow = publisherfollow,
                publisherId=publishUserId,

                picLike = piclike,
                picStar = picstar,

                numberLike=numberlike,
                numberFavorite=numberfavorite,

                picHeight = pic.p_height,
                picWidth = pic.p_width,
                picInfo = pic.p_info,

                picTags = pictag,
                hasDownload=HasDownload,
                Downloads=Download,


                uploadtime=uploadTime,
                uploadName=LiName.u_name,
                pirce=pic.price,
                nowComment=piccomment
            }) ;
        }


        /// <summary>
        /// 获取当前图片所有评论
        /// </summary>
        /// <param name="picId"></param>
        /// <returns></returns>
        
        [Route("getAllComment")]
        [HttpGet]
        public IActionResult getAllComment(string picId)
        {
            var commentNum = context.picComment.Count(c => c.p_id == picId);
            var comments = context.picComment.ToLookup(c => c.p_id)[picId].ToList();
            var returnComment = new List<OtherComment> { };
            foreach(var temp in comments)
            {
                users tempuser=context.users.FirstOrDefault(predicate => predicate.u_id == temp.u_id);
                var add = new OtherComment
                {
                    userId = tempuser.u_id,
                    userName = tempuser.u_name,
                    content = temp
                };
                returnComment.Add(add);
            }


            //返回列表
            return Ok(new
            {
                CommentNum=commentNum,
                Comments=returnComment
            });
        }

        /// <summary>
        /// 根据tag返回相关图片信息列表
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        [Route("searchByTag")]
        [HttpGet]
        public IActionResult searchByTag(string tag)
        {
            var returnList = new List<picInfo> { };
            var picList = context.ownTag.ToLookup(t => t.tag_name)[tag].ToList();
            foreach (var pic in picList)
            {
                var nowPic = services.getPicture(pic.p_id);
                var pubulisher = services.GetUserById(context.publishPicture
                    .FirstOrDefault(p => p.p_id == nowPic.p_id).u_id);
                var newPicInfo = new picInfo
                {

                    picId = nowPic.p_id,
                    picUrl = nowPic.p_url,
                    publisherId = pubulisher.u_id,
                    publisherName = pubulisher.u_name,
                    info = nowPic.p_info,
                    starNum = context.favoritePicture.Count(f => f.p_id == nowPic.p_id),
                    likeNum = context.likesPicture.Count(l => l.p_id == nowPic.p_id),
                    commNum = context.picComment.Count(c => c.p_id == nowPic.p_id)
                };
                returnList.Add(newPicInfo);
            }
            return Ok(new
            {
                Success = true,
                picList = returnList,
                msg = "Operation Done"
            });
        }



        ///<summary>
        ///收费并且下载图片。
        /// </summary>
        ////[Authorize]
        [Route("Download")]
        [HttpGet]
        public IActionResult Download(string userId,string picId)
        {
            var nowPic = services.getPicture(picId);
            var publisher = services.GetUserById(context.publishPicture.FirstOrDefault(p => p.p_id == picId).u_id);
            if (context.download.FirstOrDefault(d => d.u_id == userId && d.p_id == picId) == null
                && context.publishPicture.FirstOrDefault(p => p.u_id == userId && p.p_id == picId) == null)
            {//表示第一次下载且图片不是本人发布
                //进行购买
                var wallet = context.wallet.Find(userId);
                if (wallet.coin >= nowPic.price)
                {//能够支付
                    wallet.coin = wallet.coin-nowPic.price;
                    wallet.buy_num=wallet.buy_num+1;
                    context.wallet.Attach(wallet);
                    context.SaveChanges();
                    var newDownload = new download
                    {
                        u_id = userId,
                        p_id = picId,
                        downloadTime = DateTime.Now,
                        price = nowPic.price
                    };
                    context.download.Add(newDownload);
                    context.SaveChanges();
                    //在payment中增加消费记录
                    var newPayment = new payment
                    {
                        pay_id = (context.payment.Count() + 1).ToString(),
                        u_id = userId,
                        pay_time = DateTime.Now,
                        coin = nowPic.price,
                        type = "CS"
                    };
                    context.payment.Add(newPayment);
                    context.SaveChanges();
                    //为图片发布者增加硬币
                    var publisherWallet = context.wallet.Find(publisher.u_id);
                    publisherWallet.coin += nowPic.price;
                    context.wallet.Attach(publisherWallet);
                    context.SaveChanges();
                }
                else
                {
                    return Ok(new
                    {
                        Success = false,
                        msg = "Lack of Coin"
                    }); 
                    
                }
            }
            //已经购买进行下载
            return Ok(new
            {
                Success = true,
                downloadUrl = nowPic.p_url,
                msg = "请右键图片进行手动下载"
            });
        }


        /// <summary>
        /// 根据请求次数获取最新的三张图片
        /// </summary>
        /// <param name="requestTimes"></param>
        /// <returns></returns>
        [Route("get3Pic")]
        [HttpGet]
        public IActionResult get3Pic(int requestTimes)
        {
            var returnList = new List<picInfo> { };
            
            var picList = context.picture.ToList();
            int nowPici = services.getPicNum() - 3 * requestTimes - 1;
            for (int i = 0; i < 3 && nowPici >= 0; ++i)
            {//返回三张pic
                var pic = picList[nowPici];
                var pub = context.publishPicture
                    .FirstOrDefault(p => p.p_id == picList[nowPici].p_id);
                var pubId = pub.u_id;
                var publisher = services.GetUserById(context.publishPicture
                    .FirstOrDefault(p => p.p_id == picList[nowPici].p_id).u_id);
                var picInfo = new picInfo
                {
                    picId = pic.p_id,
                    picUrl = pic.p_url,
                    publisherId = publisher.u_id,
                    publisherName = publisher.u_name,
                    info = pic.p_info,
                    starNum = context.favoritePicture.Count(f => f.p_id == pic.p_id),
                    likeNum = context.likesPicture.Count(l => l.p_id == pic.p_id),
                    commNum = context.picComment.Count(c => c.p_id == pic.p_id)
                };
                returnList.Add(picInfo);
                nowPici--;
            }
            return Ok(new
            {
                Success = true,
                ReturnList = returnList,
                msg = "Operation Done"
            });
        }


    }


}
