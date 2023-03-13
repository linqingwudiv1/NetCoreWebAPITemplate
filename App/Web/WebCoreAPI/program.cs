using Autofac.Extensions.DependencyInjection;
using BaseDLL.Helper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using MySqlX.XDevAPI.Common;
using NLog;
using NLog.Web;
using System;
using System.Diagnostics;
using System.IO;

namespace WebCoreService
{
    /// <summary>
    /// 主函数
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();
            host.Run();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) 
        {
            // Initilize nlog
            string config_nlog_path = Path.Combine(IOHelper.GetBinRunDir(), @".Config/nlog.config");
            LogFactory loggerFactory = NLogBuilder.ConfigureNLog(config_nlog_path);

            string HostAddress = "https://127.0.0.1:18081;http://127.0.0.1:28081";
            Console.WriteLine($"Kestrel地址:{HostAddress}");

            IHostBuilder host = Host.CreateDefaultBuilder(args)
                                    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                                    .ConfigureWebHostDefaults(webBuilder =>
                                    {
                                        webBuilder.UseKestrel()
                                                  .UseContentRoot(Directory.GetCurrentDirectory())
                                                  .UseWebRoot("wwwroot")
                                                  .UseIIS()
                                                  .UseIISIntegration()
                                                  .UseStartup<Startup>()
                                                  .UseUrls(HostAddress);
                                    });

            return host;
        }
    }
}
