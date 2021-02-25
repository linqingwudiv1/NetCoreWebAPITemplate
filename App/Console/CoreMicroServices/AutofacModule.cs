using AdminServices.Event;
using Autofac;
using AutoMapper;
using BaseDLL;
using BaseDLL.Helper.Captcha;
using DBAccessBaseDLL.IDGenerator;
using DBAccessCoreDLL.Accesser;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CoreMicroServices
{
    /// <summary>
    /// 
    /// </summary>
    public class AutofacModule : Module
    {

        /// <summary>
        /// Autofac 注入服务
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            try
            {
                builder.RegisterInstance<RedisIDGenerator>(new RedisIDGenerator(new List<string>
            {
                GVariable.configuration["RedisIDGenerator:Passport"]
            },  GVariable.configuration["RedisIDGenerator:Password"])).As<IIDGenerator>().SingleInstance();

                builder.RegisterInstance<RedisCaptchaHelper>(new RedisCaptchaHelper(new List<string>
            {
                GVariable.configuration["RedisCaptchaContainer:Passport"]
            },
                    GVariable.configuration["RedisCaptchaContainer:Password"])).As<ICaptchaHelper>().SingleInstance();


                #region DB 访问器

                builder.RegisterType<AccountAccesser>().As<IAccountAccesser>().InstancePerLifetimeScope();
                builder.RegisterType<RoleAccesser>().As<IRoleAccesser>().InstancePerLifetimeScope();
                builder.RegisterType<RoutePageAccesser>().As<IRoutePageAccesser>().InstancePerLifetimeScope();

                #endregion


                #region AutoMapper

                //注册AutoMapper配置文件, Register   

                builder.Register(ctx =>
                {
                    return new MapperConfiguration(cfg =>
                    {
                        cfg.AddProfile(typeof(AdminEventProfile));
                    });
                }).AsSelf().SingleInstance(); 
                    
                builder.Register(ctx =>
                {
                    //This resolves a new context that can be used later.
                    var context = ctx.Resolve<IComponentContext>();
                    var config = context.Resolve<MapperConfiguration>();
                    return config.CreateMapper(context.Resolve);
                }).As<IMapper>().SingleInstance();

                #endregion
            }
            catch (Exception ex) 
            {
                Debug  .WriteLine($"Error ===================  Autofac Module   { ex.Message } ");
                Console.WriteLine($"Error ===================  Autofac Module   { ex.Message } ");
            }


        }
    }
}
