using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PicCommunitity.Models;

namespace PicCommunitity.Controllers
{
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private IDBRepository dbRepository;

        public HomeController(IDBRepository dbRepository)
        {
            this.dbRepository = dbRepository;
        }
        [Route("~/")]
        [Route("~/Home")]
        [Route("/Home/Index")]
        public IActionResult Index()
        {
            var textBlog = dbRepository.GetFirstBlog();
            return View(textBlog);
        }

        public async Task<IActionResult> GetAccessory(string token, string ftpKey, string fn)
        {
            byte[] buffer = null;
            using (var httpClient = new HttpClient())
            {
                //url编码
                string fileName = System.Web.HttpUtility.UrlEncode(fn);
                //图片访问地址
                var requestUri = $"http://10.22.153.39:5000/api/resource/test/{ftpKey}/{fileName}";
                var httpResult = await httpClient.GetAsync(requestUri);
                buffer = await httpResult.Content.ReadAsByteArrayAsync();
            }
            var file = File(buffer, "image/jpeg");
            return file;
        }


    }
}
