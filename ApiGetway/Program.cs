using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace ApiGeteway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).ConfigureAppConfiguration((hostingContext, builder) => { builder.AddJsonFile("configuration.json", false, true); })
                .Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
