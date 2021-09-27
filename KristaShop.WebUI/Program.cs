using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;
using System;
using System.Net;

namespace KristaShop.WebUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(serverOptions =>
                    {
                        serverOptions.Limits.MaxConcurrentConnections = 100;
                        serverOptions.Limits.MaxConcurrentUpgradedConnections = 100;
                        serverOptions.Limits.MaxRequestBodySize = 10 * 1024;
                        serverOptions.Limits.MinRequestBodyDataRate =
                            new MinDataRate(bytesPerSecond: 100, gracePeriod: TimeSpan.FromSeconds(10));
                        serverOptions.Limits.MinResponseDataRate =
                            new MinDataRate(bytesPerSecond: 100, gracePeriod: TimeSpan.FromSeconds(10));
                        serverOptions.Listen(IPAddress.Loopback, 5000);
                    })
                    .UseStartup<Startup>();
                });
    }
}
