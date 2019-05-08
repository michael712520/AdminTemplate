using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System;

namespace ApiGeteway
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin", policy =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
            // IdentityServer
            #region IdentityServerAuthenticationOptions => need to refactor

            var uri = Configuration.GetSection("IdentityService").GetSection("Url").Value;
            var useHttps = Configuration.GetSection("IdentityService").GetSection("UseHttps").Value;
            var clientSecret = Configuration.GetSection("IdentityService").GetSection("ApiSecrets").GetSection("ClientSecret").Value;
            void Api(IdentityServerAuthenticationOptions option)
            {

                option.Authority = uri;
                option.ApiName = "api";
                option.RequireHttpsMetadata = Convert.ToBoolean(useHttps);
                option.SupportedTokens = SupportedTokens.Both;
                option.ApiSecret = clientSecret;
            }
            void WebApi(IdentityServerAuthenticationOptions option)
            {
                option.Authority = uri;
                option.ApiName = "WebApi";
                option.RequireHttpsMetadata = Convert.ToBoolean(useHttps);
                option.SupportedTokens = SupportedTokens.Both;
                option.ApiSecret = clientSecret;
            }
            void AppApi(IdentityServerAuthenticationOptions option)
            {
                option.Authority = uri;
                option.ApiName = "AppApi";
                option.RequireHttpsMetadata = Convert.ToBoolean(useHttps);
                option.SupportedTokens = SupportedTokens.Both;
                option.ApiSecret = clientSecret;
            }
            void WxApi(IdentityServerAuthenticationOptions option)
            {
                option.Authority = uri;
                option.ApiName = "WxApi";
                option.RequireHttpsMetadata = Convert.ToBoolean(useHttps);
                option.SupportedTokens = SupportedTokens.Both;
                option.ApiSecret = clientSecret;
            }
            #endregion
            services.AddAuthentication()
                .AddIdentityServerAuthentication("api", Api)
                .AddIdentityServerAuthentication("WebApi", WebApi)
                .AddIdentityServerAuthentication("AppApi", AppApi)
                .AddIdentityServerAuthentication("WxApi", WxApi)
                ;

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);


            services.AddOcelot();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("AllowSpecificOrigin");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseAuthentication()
                .UseOcelot()
                .Wait();
        }
    }
}
