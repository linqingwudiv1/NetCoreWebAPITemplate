using AdminServices.Event.Role;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using BaseDLL;
using DBAccessCoreDLL.EFORM.Context;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using Host = Microsoft.Extensions.Hosting.Host;

namespace CoreMicroServices
{
    /// <summary>
    /// 
    /// </summary>
    public class Program
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            Console.WriteLine("Startup ....");
            CreateHostBuilder(args).Build().Run();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) 
        {
            return Host.CreateDefaultBuilder(args)
                       .UseServiceProviderFactory(new AutofacServiceProviderFactory( buolder=> 
                       {
                           buolder.RegisterModule(new AutofacModule());
                       }))
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
                          services.AddAutofac(c => 
                          {
                              c.Populate(services);
                          });

                          services.AddDbContextPool<CoreContextDIP>((opt) =>
                          {
                          }, 100);

                          #region MassTransit

                          services.AddMassTransit(x =>
                          {
                              x.UsingRabbitMq((ctx, cfg) =>
                              {
                                  string mqHostAddress = GVariable.configuration["MTMQ:Host"];

                                  cfg.Host(mqHostAddress, "/", c=> 
                                  {
                                      var user = GVariable.configuration["MTMQ:UserName"];
                                      var pwd = GVariable.configuration["MTMQ:Password"];
                                      c.Username(user);
                                      c.Password(pwd);
                                  });
                              });

                              x.AddConsumer<RoleDomainEvent>(cfg => 
                              {
                              });
                          });
                            
                          services.AddMassTransitHostedService();
                          #endregion
                      });
                    
                       //.ConfigureAppConfiguration;
        }
    }
}

