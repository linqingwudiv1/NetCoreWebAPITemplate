using AdminServices.Command.Role;
using BaseDLL;
using GreenPipes;
using MassTransit;
using MassTransit.ExtensionsDependencyInjectionIntegration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCoreService
{
    /// <summary>
    /// 
    /// </summary>
    public static class MassTransitConfig
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        public static void MTConfiguration(IServiceCollectionBusConfigurator x)
        {
            x.UsingRabbitMq((ctx, cfg) =>
            {
                string mqHostAddress = GVariable.configuration["MTMQ:Host"];

                cfg.Host(mqHostAddress, virtualHost: "/", c =>
                {
                    string user = GVariable.configuration["MTMQ:UserName"];
                    string pwd  = GVariable.configuration["MTMQ:Password"];
                    c.Username(user);
                    c.Password(pwd);
                });

                cfg.UseRetry(ret =>
                {
                    ret.Interval(5, TimeSpan.FromSeconds(12));
                });

            });
        }
    }
}
