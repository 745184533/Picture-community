using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Net.Http.Headers;
using PicCommunitity.Li.Helper;

namespace PicCommunitity.Controllers
{
    public class PersonalController : Controller
    {
        [Route("/Personal/Picture")]
        public IActionResult Picture()
        {
            return View();
        }

        [Route("/Personal/upload")]

        public IActionResult upload()
        {
            return View();
        }

    }
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
     public class FilesController : Controller
     {
         private IWebHostEnvironment hostingEnv;
 
         public FilesController(IWebHostEnvironment env)
         {
             this.hostingEnv = env;
         }

        [HttpPost]
        public IActionResult Post()
        {
            var files = Request.Form.Files;
            long size = files.Sum(f => f.Length);

            //size > 100MB refuse upload !
            if (size > 104857600)
            {
                return Json(Return_Helper_DG.Error_Msg_Ecode_Elevel_HttpCode("files total size > 100MB , server refused !"));
            }

            List<string> filePathResultList = new List<string>();

            foreach (var file in files)
            {
                //?trim去掉多余的空格
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim();

                string filePath = hostingEnv.WebRootPath + $@"\Files\Files\";

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                //char[] p = { '.' };

                string tempName = fileName.ToString();
                string[] temp=tempName.Split(".");

                //fileName = Guid.NewGuid() + "." + fileName.Split('.')[1];
                fileName = Guid.NewGuid() + "." + temp[1];

                string fileFullName = filePath + fileName;

                using (FileStream fs = System.IO.File.Create(fileFullName))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
                filePathResultList.Add($"/src/Files/{fileName}");
            }

            string message = $"{files.Count} file(s) /{size} bytes uploaded successfully!";

            return Json(Return_Helper_DG.Success_Msg_Data_DCount_HttpCode(message, filePathResultList, filePathResultList.Count));
        }



     }

    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class PicturesController : Controller
    {
        private IWebHostEnvironment hostingEnv;

        string[] pictureFormatArray = { "png", "jpg", "jpeg", "bmp", "gif", "ico", "PNG", "JPG", "JPEG", "BMP", "GIF", "ICO" };

        public PicturesController(IWebHostEnvironment env)
        {
            this.hostingEnv = env;
        }

        [HttpPost]
        public IActionResult Post()
        {
            var files = Request.Form.Files;
            long size = files.Sum(f => f.Length);

            //size > 100MB refuse upload !
            if (size > 104857600)
            {
                return Json(Return_Helper_DG.Error_Msg_Ecode_Elevel_HttpCode("pictures total size > 100MB , server refused !"));
            }

            List<string> filePathResultList = new List<string>();

            foreach (var file in files)
            {
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim();
   
                string filePath = hostingEnv.WebRootPath + $@"\Files\Pictures\";
   
                if (!Directory.Exists(filePath))
                {
                        Directory.CreateDirectory(filePath);
                }

                string tempName = fileName.ToString();
                string[] temp = tempName.Split('.');


                //string suffix = fileName.Split('.')[1];

                string suffix = temp[1];

                if (!pictureFormatArray.Contains(suffix))
                {
                        return Json(Return_Helper_DG.Error_Msg_Ecode_Elevel_HttpCode("the picture format not support " +
                            "! you must upload files that suffix like 'png','jpg','jpeg','bmp','gif','ico'."));
                }
   
                fileName = Guid.NewGuid() + "." + suffix;
   
                string fileFullName = filePath + fileName;
   
                using (FileStream fs = System.IO.File.Create(fileFullName))
                {
                        file.CopyTo(fs);
                        fs.Flush();
                 }
                filePathResultList.Add($"/src/Pictures/{fileName}");
            }

             string message = $"{files.Count} file(s) /{size} bytes uploaded successfully!";

             return Json(Return_Helper_DG.Success_Msg_Data_DCount_HttpCode(message, filePathResultList, filePathResultList.Count));
         }

        //public IActionResult OnPostDown() 
        //{ 
        //    var addrUrl = "/bak/love.apk";
        //    return File(addrUrl, "application/vnd.android.package-archive",
        //        Path.GetFileName(addrUrl)); 
        //}                

        //public IActionResult OnPostDown01()
        //{            
        //    var addrUrl = @"D:\F\学习\vs2017\netcore\Study.AspNetCore\WebApp02-1\wwwroot\bak\love.apk"; 
        //    var stream = System.IO.File.OpenRead(addrUrl); 
        //    return File(stream, "application/vnd.android.package-archive", 
        //        Path.GetFileName(addrUrl));        
        //}      
        //public async Task<IActionResult> OnPostDown02()
        //{            
        //    var path = "https://files.cnblogs.com/files/wangrudong003/%E7%89%B9%E4%BB%B701.gif";  
        //    HttpClient client = new HttpClient();           
        //    client.BaseAddress = new Uri(path);          
        //    var stream = await client.GetStreamAsync(path);         
        //    return File(stream, "application/vnd.android.package-archive",
        //        Path.GetFileName(path));        
        //}


        //[HttpGet]
        //public IActionResult ExportText()
        //{
        //    string sFileName = $@"qmhuangtext{DateTime.Now.ToString("yyyyMMddHHmmss")}.txt";

        //    FileStream fs = new FileStream(sFileName, FileMode.OpenOrCreate);
        //    StreamWriter sw = new StreamWriter(fs);

        //    sw.WriteLine("Hello world");
        //    sw.Flush();
        //    sw.Close();
        //    fs.Close();
        //    return File(new FileStream(sFileName, FileMode.Open), 
        //        "application/octet-stream", 
        //        $"导出测试{DateTime.Now.ToString("yyyyMMddHHmmss")}.txt");
        //}


        /// 文件流的方式输出        /// </summary>
        /// <returns></returns>
        public IActionResult DownLoad(string file)
        {
            var addrUrl = file;
            var stream = System.IO.File.OpenRead(addrUrl);
            //string fileExt = GetFileExt(file);
            //正确获取文件扩展名？
            string fileExt = file;
            //获取文件的ContentType
            var provider = new FileExtensionContentTypeProvider();
            var memi = provider.Mappings[fileExt];
            return File(stream, memi, Path.GetFileName(addrUrl));
        }

    }




}
