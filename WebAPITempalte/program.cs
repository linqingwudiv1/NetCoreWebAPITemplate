using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;

namespace Nokia_LTE_WebAPI
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
                string path = Directory.GetCurrentDirectory() + "/HostAddress.json";
                using (var file = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    using (var sr = new StreamReader(file))
                    {
                        string json = sr.ReadToEnd();
                        Program.HostAddress = JsonConvert.DeserializeObject<HostAddressModel>(json);
                    }
                }
                Console.WriteLine("Kestrel地址：" + Program.HostAddress.HostAddress);
                Console.WriteLine("修改请修改配置文件 HostAddress.json并重启服务.");
                var host = new WebHostBuilder()
                    .UseKestrel()
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .UseIISIntegration()
                    .UseStartup<Startup>()
                    .UseApplicationInsights()
                    .UseUrls(Program.HostAddress.HostAddress)
                    .Build();

                if (!Directory.Exists(Directory.GetCurrentDirectory() + "/ExportExcel"))
                {
                    Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/ExportExcel");
                }

                host.Run();
            }
            #pragma warning disable 0168
            catch (Exception ex)
            {
            }

        }
    }
}
