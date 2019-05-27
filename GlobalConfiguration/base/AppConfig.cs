using GlobalConfiguration.Utility;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace GlobalConfiguration.@base
{
    public class AppConfig
    {
        private static IConfiguration DefaultConnection { get; set; }
        private static NetResult netResult = new NetResult();
        public static NetResult GetToken()
        {
            var identityService = DefaultConnection.GetSection("IdentityService");
            if (identityService != null)
            {
                var url = identityService.GetSection("Url");
                var apiSecrets = identityService.GetSection("ApiSecrets");
                if (url != null && apiSecrets != null)
                {
                    var clientId = apiSecrets.GetSection("ClientId").Value;
                    var clientSecret = apiSecrets.GetSection("ClientSecret").Value;
                    List<KeyValuePair<string, string>> paramList = new List<KeyValuePair<string, string>>();
                    paramList.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
                    paramList.Add(new KeyValuePair<string, string>("client_id", clientId));
                    paramList.Add(new KeyValuePair<string, string>("client_secret", clientSecret));
                    var result = HttpHelper.Post(url.Value + "/connect/token", paramList);
                    var data = JsonConvert.DeserializeObject<dynamic>(result);
                    if (data != null)
                    {
                        netResult.Code = EnumResult.Success;
                        netResult.Data = data;
                        return netResult;
                    }
                    else
                    {
                        netResult.Code = EnumResult.Error;
                        netResult.Message = "配置文件错误！";
                        return netResult;
                    }
                }
                else
                {
                    netResult.Code = EnumResult.Error;
                    netResult.Message = "配置文件错误！";
                    return netResult;
                }
            }
            else
            {
                netResult.Code = EnumResult.Error;
                netResult.Message = "配置文件错误！";
                return netResult;
            }
        }

        static AppConfig()
        {
            var Production = "Production";

            if (File.Exists(Directory.GetCurrentDirectory() + $"/appsettings.{Production}.json"))
            {
                //添加 json 文件路径
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile($"appsettings.{Production}.json");


                //创建配置根对象
                var configurationRoot = builder.Build();

                DefaultConnection = configurationRoot;
            }
            else
            {
                //添加 json 文件路径
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");


                //创建配置根对象
                var configurationRoot = builder.Build();

                DefaultConnection = configurationRoot;
            }
            //取配置根下的 family 部分
        }
        public static string GetConnectionString(string name = "DefaultConnection")
        {
            if (DefaultConnection == null)
            {
                throw new ArgumentNullException("未发现默认数据库连接");
            }

            return DefaultConnection.GetSection("ConnectionStrings").GetSection(name).Value;
        }
    }
}
