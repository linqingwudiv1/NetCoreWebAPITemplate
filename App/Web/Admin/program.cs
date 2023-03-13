﻿using Autofac.Extensions.DependencyInjection;
using BaseDLL.Helper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using NLog;
using NLog.Web;
using System;
using System.Diagnostics;
using System.IO;

namespace WebAdminService
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
            var host = CreateHostBuilder(args).Build();
            host.Run();
            Console.WriteLine("host is running");
            //try
            //{
            //
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //    Debug.WriteLine(ex.StackTrace);
            //}
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

            string HostAddress = "https://127.0.0.1:18082;http://127.0.0.1:28082";

            Console.WriteLine($"==============Kestrel Server Address :{HostAddress} ==============");

            IHostBuilder host = Host.CreateDefaultBuilder(args)
                                    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                                    .ConfigureWebHostDefaults(webBuilder =>
                                    {
                                        webBuilder.UseKestrel(c => 
                                        {
                                        })
                                                  .UseContentRoot(Directory.GetCurrentDirectory())
                                                  .UseIIS()
                                                  .UseIISIntegration()
                                                  .UseStartup<Startup>()
                                                  .UseUrls(HostAddress);
                                    });
            return host;
        }
    }
}
