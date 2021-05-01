using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;

namespace SubtextMatching
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
#if (!DEBUG)
            Host.CreateDefaultBuilder(args)
                     .ConfigureAppConfiguration((hostContext, config) =>
                     {
                         config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                     })
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.UseKestrel();
                        webBuilder.UseUrls("http://0.0.0.0:" + Environment.GetEnvironmentVariable("PORT") ?? "8080");
                        webBuilder.UseContentRoot(Directory.GetCurrentDirectory());
                        webBuilder.UseStartup<Startup>();
                        webBuilder.UseIISIntegration();
                    });
#else
            Host.CreateDefaultBuilder(args)
                    .ConfigureAppConfiguration((hostContext, config) =>
                    {
                        config.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);
                    })
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.UseStartup<Startup>();
                    });
#endif
    }
}
