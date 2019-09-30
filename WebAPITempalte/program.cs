using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using Autofac.Extensions.DependencyInjection;
using System.Diagnostics;
using Autofac;
using System.IO;
using log4net;
using System.Linq;
using DBAccessDLL.EF.Context;
using System.Configuration;
using Microsoft.Extensions.Hosting;

namespace WebAPI
{
    /// <summary>
    /// 主函数
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 
        /// </summary>
        private class HostAddressModel
        {
            /// <summary>
            /// 
            /// </summary>
            public string HostAddress { get; set; }
        }
        /// <summary>
        /// 
        /// </summary>
        private static HostAddressModel HostAddress { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex) 
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
            string path = Directory.GetCurrentDirectory() + @"\.Config\HostAddress.json";
            using ( FileStream file = new FileStream ( path, FileMode.Open, FileAccess.Read) )
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    string json = sr.ReadToEnd();
                    Program.HostAddress = JsonConvert.DeserializeObject<HostAddressModel>(json);
                }
            }

            Console.WriteLine("Kestrel地址：" + Program.HostAddress.HostAddress);
            Console.WriteLine("Note：如需修改,请修改配置文件 HostAddress.json并重启服务.");

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
