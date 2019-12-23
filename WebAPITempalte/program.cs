using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using NLog;
using NLog.Web;
using System;
using System.IO;

namespace WebAPI
{
    /// <summary>
    /// 主函数
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// 
        /// </summary>
        public class HostAddressModel
        {
            /// <summary>
            /// 
            /// </summary>
            public string HostAddress { get; set; }
        }
        /// <summary>
        /// 
        /// </summary>
        private static HostAddressModel HostAddress = new HostAddressModel();//{ get; set; }

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

            using (FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    string json = sr.ReadToEnd();

                    Program.HostAddress = JsonConvert.DeserializeObject<HostAddressModel>(json);
                }
            }

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
