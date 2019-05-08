using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace IdentityServer
{
    public static class Config
    {

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }
        /// <summary>
        /// API信息
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetApis()
        {
            var configuration = Configuration();
            List<ApiResource> list = new List<ApiResource>();
            var allowedScopes = configuration.GetSection("IdentityService").GetSection("ApiSecrets")
                .GetSection("AllowedScopes");
            foreach (var item in allowedScopes.GetChildren())
            {
                var apiResource = new ApiResource(item.Value, item.Value + "Resource");
                list.Add(apiResource);
            }
            return list;
        }
        /// <summary>
        /// 客服端信息
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients()
        {
            var configuration = Configuration();
            var cAllowedScopes = configuration.GetSection("IdentityService").GetSection("ApiSecrets")
                .GetSection("AllowedScopes");
            var allowedScopes = cAllowedScopes.GetChildren().Select(s => s.Value).ToList();
            var clientId = configuration.GetSection("IdentityService").GetSection("ApiSecrets")
                .GetSection("ClientId");
            var clientName = configuration.GetSection("IdentityService").GetSection("ApiSecrets")
                .GetSection("ClientName");
            var clientSecret = configuration.GetSection("IdentityService").GetSection("ApiSecrets")
                .GetSection("ClientSecret");
            var grantType = configuration.GetSection("IdentityService").GetSection("ApiSecrets")
                .GetSection("GrantType");
            return new[]
            {
                new Client
                {
                    ClientId = clientId.Value,//客服端名称
                    ClientName = clientName.Value,//描述
                    AllowedGrantTypes = GrantTypes.ClientCredentials,//指定允许的授权类型（AuthorizationCode，Implicit，Hybrid，ResourceOwner，ClientCredentials的合法组合）。
                    AllowAccessTokensViaBrowser = true,//是否通过浏览器为此客户端传输访问令牌
                    AllowedScopes = allowedScopes,//指定客户端请求的api作用域。 如果为空，则客户端无法访问
                    ClientSecrets={ new Secret { Value= clientSecret.Value.Sha256(), Expiration=DateTime.Now.AddMonths(5)} }
                }
            };
        }

        public static IConfiguration Configuration()
        {
            var Production = "Production";
            if (File.Exists(Directory.GetCurrentDirectory() + $"/appsettings.{Production}.json"))
            { //添加 json 文件路径
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile($"appsettings.{Production}.json");


                //创建配置根对象
                var configurationRoot = builder.Build();
                return configurationRoot;
            }
            else
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile($"appsettings.json");
                //创建配置根对象

                var configurationRoot = builder.Build();
                return configurationRoot;
            }
        }
    }
}
