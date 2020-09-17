﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using PicCommunitity.Models;
using PicCommunitity.Services;
using PicCommunitity.ViewsModels;

namespace PicCommunitity.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlogController : Controller
    {
        private AppDbContext context;
        private BlogServices services;
        public BlogController(AppDbContext context)
        {
            this.context = context;
            this.services = new BlogServices(context);
        }

        [Route("getTen")]
        [HttpGet]
        public IActionResult getTen(int times)
        {
            //得到最新的十条博客
            var list = context.blog.ToList();
            var returnList = new List<blog> { };
            var blogNum = context.blog.Count();
            for (var i = blogNum - 1 - 10 * times; i >= blogNum -11- 10* times && i >= 0; --i)
            {
                returnList.Add(list[i]);
            }

            return Ok(new
            {
                Success = true,
                Times = times,
                List = returnList,
                msg = "Operation Done"
            });
        }

        [Route("writeBlog")]
        [HttpPost]
        public IActionResult writeBlog([FromBody] BlogInfo Blog)
        {
            var nowblog = new blog
            {
                b_date = DateTime.Now,
                b_id = (context.blog.Count() + 1).ToString(),
                b_text = Blog.content,
                b_type = "tt"
            };
            context.blog.Add(nowblog);
            context.SaveChanges();
            //添加联系集
            var newOwnBlog = new ownBlog
            {
                u_id = Blog.userId,
                b_id = (context.blog.Count()).ToString()
            };
            context.ownBlog.Add(newOwnBlog);
            context.SaveChanges();
            return Ok(new
            {
                Success = true,
                msg = "Operation Done"
            });
        }
    }
}