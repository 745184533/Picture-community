using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using PicCommunitity.Models;
using PicCommunitity.Tools;

namespace PicCommunitity
{
    public class Startup
    {
        private IConfiguration _config;
        public IConfigurationRoot Configuration { get; }
        public Startup(IConfiguration config,IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                           .SetBasePath(env.ContentRootPath)
                           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                           .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                           .AddEnvironmentVariables();
            Configuration = builder.Build();
            _config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("any",
                builder =>  builder.WithOrigins("http://localhost:44362",
                "http://localhost:63342").AllowAnyHeader().
                AllowAnyOrigin().AllowAnyMethod());

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

            //配置Jwt验证
            services.Configure<JwtSetting>(_config.GetSection("JwtSetting"));
            var jwtSetting = new JwtSetting();
            _config.Bind("JwtSetting", jwtSetting);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o => {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    //是否验证发行人
                    ValidateIssuer = true,
                    ValidIssuer = jwtSetting.Issuer,//发行人
                                                    //是否验证受众人
                    ValidateAudience = true,
                    ValidAudience = jwtSetting.Audience,//受众人
                                                        //是否验证密钥
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.SecurityKey)),

                    ValidateLifetime = true, //验证生命周期
                    RequireExpirationTime = false //过期时间
                };
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


            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"C://")),
                RequestPath = new PathString("/src"),

            });

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
