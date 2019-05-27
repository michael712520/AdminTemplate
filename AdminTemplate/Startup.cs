using AdminTemplate.DataBase.Models;
using AdminTemplate.service.ConfigServices;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AdminTemplate
{
    public class Startup
    {
        public IHostingEnvironment Environment { get; }
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Environment = environment;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLoggingFileUI();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(
                opts =>
                {
                    //忽略循环引用
                    opts.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

                    //设置时间格式
                    opts.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                });

            //跨域
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin", policy =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
            services.AddDbContext<questionContext>(o =>
            {
                string connectionString = Configuration.GetConnectionString("DefaultConnection");
                o.UseMySQL(connectionString);
                // o.UseMySQL()

            });
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", info: new OpenApiInfo
                {
                    Version = "Version 1.0",
                    Title = "xxxxxx的API文档",
                    Description = "xxx作者"
                });

            });
            services.AddAutoMapper();
            services.Scan(x =>
            {

                var entryAssembly = Assembly.GetEntryAssembly();
                if (entryAssembly != null)
                {
                    var referencedAssemblies = entryAssembly.GetReferencedAssemblies().Select(Assembly.Load);
                    var assemblies = new List<Assembly> { entryAssembly }.Concat(referencedAssemblies);
                    assemblies = assemblies.Where(item => item.GetName().Name.StartsWith("AdminTemplate") || item.GetName().Name.StartsWith("GlobalConfiguration"));
                    x.FromAssemblies(assemblies)
                        .AddClasses()
                        //TODO:加上会因为日志接口的泛型问题报错
                        //.AsImplementedInterfaces()
                        .AsSelf()
                        .WithScopedLifetime();
                }
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("AllowSpecificOrigin");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "OrgAPI");
                c.DocExpansion(DocExpansion.None);
            });
            app.UseStaticFiles();
            //mvc 路由配置
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });


            DefaultFilesOptions options = new DefaultFilesOptions();
            // options.DefaultFileNames.Add("Swagger-ui.html");
            app.UseDefaultFiles(options);
            //AutoMapper
            AutoMapper.Mapper.Initialize(x =>
            {
                x.AddProfile<ServiceProfiles>();
            });
        }
    }
}
