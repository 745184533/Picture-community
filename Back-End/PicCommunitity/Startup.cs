using System;
using System.Collections.Generic;
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
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.FileProviders;
using System.IO;
using System.Configuration;

namespace PicCommunitity
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        private IConfiguration _config;

        public Startup(IConfiguration config, IWebHostEnvironment env)
        {
            _config = config;

            var builder = new ConfigurationBuilder()
                    .SetBasePath(env.ContentRootPath)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                    .AddEnvironmentVariables();
            //Configuration configuration = builder.Build();
            Configuration = builder.Build();

        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<AppDbContext>(options =>
                options.UseMySQL(_config.GetConnectionString("DBConnection"))
            );

            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
            }).AddXmlSerializerFormatters();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
                options =>
                {
                    options.Cookie.Name = "MyApplicationTokenCookie";//���ô洢�û���¼��Ϣ���û�Token��Ϣ����Cookie����
                    options.Cookie.HttpOnly = true;//���ô洢�û���¼��Ϣ���û�Token��Ϣ����Cookie��
                                                   //�޷�ͨ���ͻ���������ű�(��JavaScript��)���ʵ�
                    options.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.Always;
                    options.LoginPath = "/Account/Login";
                    options.Cookie.HttpOnly = true;
                });

            services.AddScoped<IDBRepository, SQLDBRepository>();

            services.AddCors(OptionsBuilderConfigurationExtensions =>
            {
                OptionsBuilderConfigurationExtensions.AddPolicy("AllowSpecificOrigin",
                    builder=>builder.WithOrigins("https://localhost:5001").AllowAnyOrigin()
                    .AllowAnyHeader().AllowAnyMethod());
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseStaticFiles();
            app.UseCors("AllowSpecificOrigin");

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider= new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/Files")),
                RequestPath = new PathString("/src"),
                //���ò�����content-type                
                ServeUnknownFileTypes = true   
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=home}/{actions=index}/{id?}");
            });
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync("Hello World!");
            //    });
            //});
        }
    }
}
