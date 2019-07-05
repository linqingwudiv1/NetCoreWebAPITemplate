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
                string path = Directory.GetCurrentDirectory() + @"\.Config\HostAddress.json";
                using (FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader sr = new StreamReader(file))
                    {
                        string json = sr.ReadToEnd();
                        Program.HostAddress = JsonConvert.DeserializeObject<HostAddressModel>(json);
                    }
                }
                Console.WriteLine("Kestrel地址：" + Program.HostAddress.HostAddress);
                Console.WriteLine("Note：如需修改,请修改配置文件 HostAddress.json并重启服务.");

                var host = new WebHostBuilder()
                    .UseKestrel()
                    .ConfigureServices(services => services.AddAutofac())
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .UseIISIntegration()
                    .UseStartup<Startup>()
                    .UseApplicationInsights()
                    .UseUrls(Program.HostAddress.HostAddress)
                    .Build();

                host.Run();
            }
            #pragma warning disable 0168
            catch (Exception ex)
            {
            }

        }
    }
}
