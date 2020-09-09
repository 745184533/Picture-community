using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PicCommunitity.Tools
{
    /// <summary>
     /// CORS 中间件 【解决跨域问题】
     /// </summary>
     public class CorsMiddleware{
         private readonly RequestDelegate _next;
         public CorsMiddleware(RequestDelegate next)
         {
             _next = next;
         }
 
         public async Task Invoke(HttpContext context)
         {
             if (!context.Response.Headers.ContainsKey("Access-Control-Allow-Origin"))
             {
                 context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
             }
             await _next(context);
         }
     }
}
