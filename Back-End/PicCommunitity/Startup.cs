using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PicCommunitity.Models;
using PicCommunitity.Tools;

namespace PicCommunitity
{
    public class Startup
    {
        private IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("any",
                builder =>  builder.WithOrigins("http://localhost:44362").AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

            });

            services.AddControllersWithViews().AddNewtonsoftJson();

            services.AddDbContextPool<AppDbContext>(options =>
                options.UseMySQL(_config.GetConnectionString("DBConnection"))
            );
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v0.1.0",
                    Title = "Api",
                    Description = "说明文档",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact { Name = "AAAApi", Email = "892542", Url = new Uri("http://www.baidu.com") }
                });
                var basePath = AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "PicCommunitity.xml");
                c.IncludeXmlComments(xmlPath, true);

            });

            services.AddMvc(options =>
            {
            }).AddXmlSerializerFormatters();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
                options =>
                {
                    options.Cookie.Name = "MyApplicationTokenCookie";//设置存储用户登录信息（用户Token信息）的Cookie名称
                    options.Cookie.HttpOnly = true;//设置存储用户登录信息（用户Token信息）的Cookie，
                                                   //无法通过客户端浏览器脚本(如JavaScript等)访问到
                    options.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.Always;
                    options.LoginPath = "/Account/Login";
                    options.Cookie.HttpOnly = true;
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiHelp V1");
                });
            }


            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors("any");

            //app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute("default", "{controller=home}/{actions=index}/{id?}");
            //});
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
