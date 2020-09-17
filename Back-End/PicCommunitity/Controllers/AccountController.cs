using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using PicCommunitity.Models;
using PicCommunitity.Services;
using PicCommunitity.ViewsModel;
using PicCommunitity.ViewsModels;

namespace PicCommunitity.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private AppDbContext context;
        private AccountServices services;
        private PictureServices PictureServices;

        private IWebHostEnvironment hostingEnv;

        string[] pictureFormatArray = { "png", "jpg", "jpeg", "bmp", "gif",
            "ico", "PNG", "JPG", "JPEG", "BMP", "GIF", "ICO" };

        public AccountController(AppDbContext context,IOptions<JwtSetting> options, 
            IWebHostEnvironment env)
        {
            this.context = context;
            this.services = new AccountServices(context,options);
            this.PictureServices = new PictureServices(context);
            this.hostingEnv = env;
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [Route("login")]
        [HttpPost]
        public IActionResult login([FromBody]LoginUser user)
        {
            if (services.checkExist(user.userName, user.userPassword))
            {//登录成功
                var User = context.users
                    .FirstOrDefault(u => u.u_name == user.userName && u.u_password == user.userPassword);
                //颁发Token
                var token = services.GetToken(user);
                return Ok(new
                {
                    Success = true,
                    userId = User.u_id,
                    userName=User.u_name,
                    Token = token,
                    Type = "Bearer"
                }) ;
            }
            return Ok(new
            {
                Success = false
            }) ;
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPassword"></param>
        /// <returns></returns>
        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> register([FromBody]RegisterUser RUser)
        {
            if (!services.checkExist(RUser.userName, RUser.userPassword))
            {//没有该账号密码存在
                //创建user
                var user = new users { };
                user.u_id = (context.users.Count() + 1).ToString();
                user.u_name = RUser.userName;
                user.u_password = RUser.userPassword;
                user.u_status = "AC";
                user.u_type = "US";
                user.create_time = DateTime.Now;
                context.users.Add(user);
                await context.SaveChangesAsync();

                //创建相应userInfo
                var newUserInfo = new userInfo
                { 
                    u_id = user.u_id,
                    u_name = user.u_name,
                    birthday = Convert.ToDateTime("1800-01-01 00:00:00"),
                    mail = RUser.Email,
                    phone_number = "0",
                    message=""
                };
                context.userInfo.Add(newUserInfo);
                await context.SaveChangesAsync();

                //创建相应钱包
                var newUserWallet = new wallet
                {
                    u_id = user.u_id,
                    buy_num = 0,
                    coin = 0,
                    publish_num = 0
                };
                context.wallet.Add(newUserWallet);
                await context.SaveChangesAsync();

                //完成了注册赋予Token
                var token = services.GetToken(
                    new LoginUser { userName = user.u_name, userPassword = user.u_password });
                return Ok(new
                {
                    Success = true,
                    userId=user.u_id,
                    userName=user.u_name,
                    Token=token,
                    msg="New User Registered"
                });
            }
            else
            {//已有该账号密码存在
                return Ok(new
                {
                    Success = false,
                    msg = "Same UserName&Password"
                });
            }
        }

        /// <summary>
        /// 获取当前所有用户信息
        /// </summary>
        /// <returns></returns>
        [Route("getAllUsers")]
        [HttpGet]
        public IEnumerable<users> getAllUsers()
        {
            return context.users.ToArray();
        }


        /// <summary>
        /// 获得对应id的用户详细信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        //[Authorize]
        [Route("getUserInfo")]
        [HttpGet]
        public IActionResult getUserInfo(string userId)
        {
            //首先判断是否存在该Id
            var user = services.GetUserById(userId);
            if (user != null)
            {//用户存在
                var userInfo = services.GetUserInfoById(userId);
                if (userInfo == null)
                {//如果此时没有userInfo
                    return Ok(new
                    {
                        Success = false,
                        msg = "No such User"
                    });
                }
                return Ok(new
                {
                    Success = true,
                    //Picture=
                    UserName=user.u_name,
                    Name = userInfo.u_name,
                    Birthday = userInfo.birthday,
                    Email = userInfo.mail,
                    Message = userInfo.message,
                });
            }
            else
            {//用户不存在
                return Ok(new
                {
                    Success = false,
                    msg = "No such User"
                });
            }
        }


        /// <summary>
        /// 保存填写后用户详细信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [Route("saveUserInfo")]
        [HttpPost]
        public IActionResult saveUserInfo([FromBody] UserInfo info)
        {
            var user = services.GetUserById(info.UserId);
            var userInfo = services.GetUserInfoById(info.UserId);
            user.u_name = info.UserName;
            context.users.Attach(user);
            userInfo.u_name = info.Name;
            userInfo.mail = info.Email;
            userInfo.message = info.Message;
            context.userInfo.Attach(userInfo);
            context.SaveChanges();
            return Ok(new
            {
                Success = true,
                msg = "Operation Done"
            });
        }
        

        /// <summary>
        /// 获得用户个人图片主页信息，点赞数等
        /// </summary>
        //[Authorize]
        [Route("getProfileInfo")]
        [HttpGet]
        public IActionResult getProfileInfo(string userId)
        {
            var info = services.GetProfileInfoById(userId);
            return Ok(new
            {
                Success=true,
                StarNum=info.starNum,
                LikeNum=info.likeNum,
                CommentNum=info.commentNum,
                FollowNum=info.followNum
            });
        }

        ///<summary>
        ///获取用户个人图片主页的图片信息
        /// 
        /// </summary>
        [Route("getProfilePicture")]
        [HttpGet]
        public IActionResult getProfilePicture(string userId)
        {
            //我的上传
            var info = context.publishPicture.ToLookup(p => p.u_id)[userId].ToList();
            //我的收藏
            var infos = context.favoritePicture.ToLookup(p => p.u_id)[userId].ToList();
            int count1 = info.Count();
            int count2 = infos.Count();
            //?初始化局部list?
            //int[] a = new []int;
            var tempPicture = new List<ProfilePicture> { };

            var tempPictures = new List<ProfilePicture> { };

            ///ProfilePicture[] tempPictures = new ProfilePicture[count2];
            ///ProfilePicture[] tempPictures = new ProfilePicture[count2];

            //int[] a = new int[10] ;
            int i = 0;

            foreach(var Picture in info)
            {
                //上传图片信息
                var temp1 = new ProfilePicture
                {
                    like = context.likesPicture.Count(p => p.p_id == Picture.p_id),
                    favorite = context.favoritePicture.Count(p => p.p_id == Picture.p_id),
                    comment = context.picComment.Count(p => p.p_id == Picture.p_id),
                    thatpicture = context.picture.FirstOrDefault(p => p.p_id == Picture.p_id)
                };
                tempPicture.Add(temp1);
                ++i;
            }
            i = 0;
            foreach(var Picture in infos)
            {
                //收藏图片信息
                var temp1 = new ProfilePicture
                {
                    like = context.likesPicture.Count(p => p.p_id == Picture.p_id),
                    favorite = context.favoritePicture.Count(p => p.p_id == Picture.p_id),
                    comment = context.picComment.Count(p => p.p_id == Picture.p_id),
                    thatpicture = context.picture.FirstOrDefault(p => p.p_id == Picture.p_id)
                };
                tempPictures.Add(temp1);
                ++i;
            }
            return Ok(new
            {
                Success=true,
                Upload=tempPicture,
                favorite=tempPictures
            }
            );
        }


        /// <summary>
        /// 关注用户或已关注取消关注用户
        /// </summary>
        /// <param name="fansId"></param>
        /// <param name="followId"></param>
        /// <returns></returns>
        [Route("followUser")]
        [HttpPost]
        public IActionResult followUser(string fansId, string followId)
        {
            var follow = context.follow.FirstOrDefault(f => f.fans_id == fansId && f.follow_id == followId);
            if (follow != null)
            {//已经关注，则取消关注
                context.follow.Remove(follow);
                context.SaveChanges();
            }
            else
            {
                follow = new follow
                {
                    fans_id = fansId,
                    follow_id = followId
                };
                context.follow.Add(follow);
                context.SaveChanges();
            }
            return Ok(new
            {
                Success = true,
                msg = "Operation Done"
            });
        }

        ///<summary>
        ///下载图片
        /// </summary>
        //[Authorize]
        [Route("Upload")]
        [HttpPost]
        public async Task<IActionResult> Upload([FromForm] IFormCollection forms)
        {
            //只选取文件
            FormFileCollection Lifile = (FormFileCollection)forms.Files;

            //需要绑定图片名和图片id
            StringValues[] temp = { "", "", "" };
            string[] thagTag = { "tag", "tag1", "tag2" };
            //int[5] a;
            for (int i = 0; i < 3; ++i)
            {
                forms.TryGetValue(thagTag[i], out temp[i]);
            }

            StringValues information = "";
            forms.TryGetValue("p_Info", out information);

            StringValues userID = "";
            forms.TryGetValue("userId", out userID);

            StringValues price = "";
            forms.TryGetValue("price", out price);
            string tempsd = price;
            int Prices = int.Parse(tempsd);
            


            var files = Request.Form.Files;


            long size = files.Sum(f => f.Length);

            //size > 100MB refuse upload !
            if (size > 104857600)
            {
                return Ok(new
                {
                    Success = false,
                    Message = "pictures total size > 100MB , server refused !"
                });
            }

            List<string> filePathResultList = new List<string>();

            //只能上传一张图片顺便贴标签
            var file = files[0];

            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

            //@
            string filePath = "C:" + @"\Pics\";

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            string suffix = fileName.Split('.')[1];


            //检查文件后缀名确保是图片而不是其他文件。
            if (!pictureFormatArray.Contains(suffix))
            {
                return Ok(new
                {
                    Success = false,
                    Message = "the picture format not support ! you must upload files " +
                    "that suffix like 'png','jpg','jpeg','bmp','gif','ico'.",
                    Name = fileName
                });
            }
            //文件名命名？
            //存取图片的时候以id为准
            //context.picture.Count();


            fileName = Guid.NewGuid() + "." + suffix;

            string fileFullName = filePath + fileName;

            int height = 0;
            int width = 0;


            await using (FileStream fs = System.IO.File.Create(fileFullName))
            {
                file.CopyTo(fs);
                System.Drawing.Image image = System.Drawing.Image.FromStream(fs);
                height = image.Height;
                width = image.Width;
                fs.Flush();
            }

            filePathResultList.Add($"/src/Pics/{fileName}");

            string message = $"{files.Count} file(s) /{size} bytes uploaded successfully!";

            //Json("tag1", temp[0].ToString());
            string OwnTags = "tag1:"+temp[0].ToString() + ",tag2:" + temp[1].ToString() + ",tag3:" + temp[2].ToString();

            //刷新为服务器的图片。
            fileFullName = "http://172.81.239.44:8002/" + fileName;

            picture tempPicture = new picture
            {
                p_id = (context.picture.Count() + 1).ToString(),
                p_url = fileFullName,
                p_info = information,//还是要能用http访问，不是https
                p_height=height,
                p_width=width,
                p_status="OK",//图片状态不确定。
                price=Prices,
                likes=0,
                dislikes=0,
                comm_num=0
            };

            //承接前一步异步保存。
            context.picture.Add(tempPicture);
            await context.SaveChangesAsync();


            //List<tag> contextTag = context.tag.ToList();
            for(int i=0;i<3;++i)
            {
                if(temp[i]!="")
                {
                    tag isLegal=context.tag.FirstOrDefault(p => p.tag_name == temp[i]);

                    if(isLegal==null)
                    {
                        //表示这是用户新增的tag
                        isLegal = new tag
                        {
                            tag_name = temp[i]
                        };
                        //因为外码依赖要先增加tag
                        context.tag.Add(isLegal);
                        await context.SaveChangesAsync();
                    }

                    ownTag tempTag = new ownTag
                    {
                        p_id = tempPicture.p_id,
                        tag_name = temp[i]
                    };
                    context.ownTag.Add(tempTag);
                }
            }
            await context.SaveChangesAsync();



            publishPicture tempPublish = new publishPicture
            {
                u_id=userID,
                p_id=tempPicture.p_id,
                publish_time=DateTime.Now
            };
            context.publishPicture.Add(tempPublish);
            await context.SaveChangesAsync();



            return Ok(new
            {
                Success = true,
                Message = message,
                PictureHeight=tempPicture.p_height,
                PictureWidth=tempPicture.p_width,
                Price=Prices,
                PictureURL=tempPicture.p_url,
                //fileList = filePathResultList,
                OwnTag=OwnTags
            });
        }

        ///<summary>
        ///搜索相似图片
        /// 
        /// </summary>
        [Route("SimilarPicture")]
        [HttpPost]
        public async Task<IActionResult> SimilarPicture([FromForm] IFormCollection forms)
        {
            var files = Request.Form.Files;

            long size = files.Sum(f => f.Length);

            //size > 100MB refuse upload !
            if (size > 104857600)
            {
                return Ok(new
                {
                    Success = false,
                    Message = "pictures total size > 100MB , server refused !"
                });
            }
            var file = files[0];
            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');



            string filePath = "C:" + @"\Pics\";

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            string suffix = fileName.Split('.')[1];


            //检查文件后缀名确保是图片而不是其他文件。
            if (!pictureFormatArray.Contains(suffix))
            {
                return Ok(new
                {
                    Success = false,
                    Message = "the picture format not support ! you must upload files " +
                    "that suffix like 'png','jpg','jpeg','bmp','gif','ico'.",
                    Name = fileName
                });
            }


            fileName = Guid.NewGuid() + "." + suffix;

            string fileFullName = filePath + fileName;

            int height = 0;
            int width = 0;


            await using (FileStream fs = System.IO.File.Create(fileFullName))
            {
                file.CopyTo(fs);
                System.Drawing.Image image = System.Drawing.Image.FromStream(fs);
                height = image.Height;
                width = image.Width;
                fs.Flush();
            }

            string message = $"{files.Count} file(s) /{size} bytes uploaded successfully!";

            //刷新为服务器的图片。
            fileFullName = "C:/Pics/" + fileName;
            

            //string[] ReturnTag = PictureServices.getTag("D:/Entertainment/Entertainment/MechineLearning/Data/test/5.jpg");
            string[] ReturnTag = PictureServices.getTag(fileFullName);

            var returnList = new List<picInfo> { };

            //循环3个tag
            for(int i=0;i<3;++i)
            {
                var picList = context.ownTag.ToLookup(t => t.tag_name)[ReturnTag[i]].ToList();
                //循环每个tag里面的图片
                foreach (var pic in picList)
                {
                    var nowPic = PictureServices.getPicture(pic.p_id);
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
            }

            return Ok(new
            {
                Success = true,
                picList = returnList,
                msg = "Operation Done"
            });


        }

        ///<summary>
        ///第一回合上传
        /// 
        /// </summary>
        [Route("Upload1")]
        [HttpPost]
        public async Task<IActionResult> Upload1([FromForm] IFormCollection forms)
        {
            StringValues userID = "";
            forms.TryGetValue("userId", out userID);

            var PictureId = context.picture.Count() + 1;
 
            var files = Request.Form.Files;
            long size = files.Sum(f => f.Length);

            //size > 100MB refuse upload !
            if (size > 104857600)
            {
                return Ok(new
                {
                    Success = false,
                    Message = "pictures total size > 100MB , server refused !"
                });
            }

            //只能上传一张图片顺便贴标签
            var file = files[0];

            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

            //@
            string filePath = "C:" + @"\Pics\";

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            string suffix = fileName.Split('.')[1];


            //检查文件后缀名确保是图片而不是其他文件。
            if (!pictureFormatArray.Contains(suffix))
            {
                return Ok(new
                {
                    Success = false,
                    Message = "the picture format not support ! you must upload files " +
                    "that suffix like 'png','jpg','jpeg','bmp','gif','ico'.",
                    Name = fileName
                });
            }

            fileName = Guid.NewGuid() + "." + suffix;

            string fileFullName = filePath + fileName;
            int height = 0;
            int width = 0;


            await using (FileStream fs = System.IO.File.Create(fileFullName))
            {
                file.CopyTo(fs);
                System.Drawing.Image image = System.Drawing.Image.FromStream(fs);
                height = image.Height;
                width = image.Width;
                fs.Flush();
            }
            //刷新为服务器的图片。
            fileFullName = "http://172.81.239.44:8002/" + fileName;
            var Tanfile = "C:/Pics/" + fileName;

            picture tempPicture = new picture
            {
                p_id = (context.picture.Count() + 1).ToString(),
                p_url = fileFullName,
                p_height = height,
                p_width = width,
                p_status = "OK",//图片状态不确定。
                price=0,
                p_info="NULL",
                likes = 0,
                dislikes = 0,
                comm_num = 0
            };

            //承接前一步异步保存。
            context.picture.Add(tempPicture);
            await context.SaveChangesAsync();

            publishPicture tempPublish = new publishPicture
            {
                u_id = userID,
                p_id = tempPicture.p_id,
                publish_time = DateTime.Now
            };
            context.publishPicture.Add(tempPublish);
            await context.SaveChangesAsync();



            string[] AITag = PictureServices.getTag(Tanfile);
            string message = $"{files.Count} file(s) /{size} bytes uploaded successfully!";

            return Ok(new
            {
                Success = true,
                Tags=AITag,
                pictureHeight=height,
                pictureWidth=width,
                Message=message,
                pictureURL=fileFullName,
                pictureId= PictureId
            });
        }


        ///<summary>
        ///第二回合上传
        /// 
        /// </summary>
        [Route("Upload2")]
        [HttpPost]
        public async Task<IActionResult> Upload2([FromForm] IFormCollection forms)
        {
            StringValues pid = "";
            StringValues userID = "";
            StringValues information = "";
            StringValues sPrice = "";

            //需要绑定图片名和图片id
            StringValues[] temp = { "", "", "" };
            string[] thagTag = { "tag", "tag1", "tag2" };
            //int[5] a;
            for (int i = 0; i < 3; ++i)
            {
                forms.TryGetValue(thagTag[i], out temp[i]);
            }

            forms.TryGetValue("userId",out userID);
            forms.TryGetValue("pictureId", out pid);
            forms.TryGetValue("p_info", out information);
            forms.TryGetValue("price", out sPrice);
            int prices = int.Parse(sPrice);

            var LiPicture = context.picture.FirstOrDefault(s=>s.p_id==pid);
            LiPicture.price = prices;
            LiPicture.p_info = information;
            context.picture.Attach(LiPicture);
            for(int i=0;i<3;++i)
            {
                var tagPicture = new ownTag
                {
                    p_id = pid,
                    tag_name = temp[i]
                };
                context.ownTag.Add(tagPicture);
            }
            
                



            return Ok(new
            {
                Success = true,
                ownTag=temp,
                price=prices,
                p_info=information
            }) ;
        }

    }
}
