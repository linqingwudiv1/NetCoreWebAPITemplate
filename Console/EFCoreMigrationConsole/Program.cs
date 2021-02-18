using BaseDLL;
using DBAccessBaseDLL.Static;
using DBAccessCoreDLL.EFORM.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;

namespace EFCoreMigrationConsole
{
    /// <summary>
    /// 
    /// </summary>
    class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("Startup ....");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                       .UseContentRoot(Directory.GetCurrentDirectory())
                       .ConfigureHostConfiguration(builder =>
                       {
                           builder.AddInMemoryCollection()
                                  .AddJsonFile(@".Config\appsettings.json", optional: false, reloadOnChange: true)
                                  .AddJsonFile(@".Config\ConnectionString.json", optional: false, reloadOnChange: true)
                                  .AddJsonFile(@".Config\APILTEUrl.json", optional: false, reloadOnChange: true)
                                  .AddEnvironmentVariables();

                           var Configuration = builder.Build();

                           GVariable.configuration = Configuration;
                       })
                      .ConfigureServices(services =>
                      {

                          services.AddDbContext<CoreContext>((opt) =>
                          {
                              var ConnString = GConnStrings.PostgreSQLCoreDBConn;
                              opt.UseNpgsql(ConnString, b =>
                              {
                                 b.MigrationsAssembly("EFCoreMigrationConsole");
                              });
                          });

                      });

        }
    }
}
