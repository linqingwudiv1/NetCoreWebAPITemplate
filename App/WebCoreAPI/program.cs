using Autofac.Extensions.DependencyInjection;
using BaseDLL.Helper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using NLog;
using NLog.Web;
using System;
using System.IO;

namespace WebCoreService
{
    /// <summary>
    /// 主函数
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// 
        /// </summary>
        public class HostAddressInfo
        {
            /// <summary>
            /// 
            /// </summary>
            public string HostAddress { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        private static HostAddressInfo HostAddress = new HostAddressInfo();//{ get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            try
            {
                var host = CreateHostBuilder(args).Build();
                
                host.Run();
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) 
        {
            // Initilize nlog
            LogFactory loggerFactory = NLogBuilder.ConfigureNLog(@"./.Config/nlog.config");
            string path = Directory.GetCurrentDirectory() + @"/.Config/HostAddress.json";
            Program.HostAddress = JsonHelper.loadJsonFromFile<HostAddressInfo>(path);

            Console.WriteLine("Kestrel地址:" + Program.HostAddress.HostAddress);

            IHostBuilder host = Host.CreateDefaultBuilder(args)
                                    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                                    .ConfigureWebHost(webBuilder =>
                                    {
                                        webBuilder.UseKestrel()
                                                  .UseContentRoot(Directory.GetCurrentDirectory())
                                                  .UseIIS()
                                                  .UseIISIntegration()
                                                  .UseStartup<Startup>()
                                                  .UseUrls(Program.HostAddress.HostAddress);
                                    });

            return host;
        }
    }
}
