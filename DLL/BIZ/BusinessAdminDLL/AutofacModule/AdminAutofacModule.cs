using Autofac;
using AutoMapper;
using BaseDLL;
using BaseDLL.Helper.Asset;
using BaseDLL.Helper.Captcha;
using BusinessAdminDLL.Accounts;
using BusinessAdminDLL.Asset;
using BusinessAdminDLL.AutoMapper;
using BusinessAdminDLL.Roles;
using BusinessAdminDLL.RoutePage;
using DBAccessBaseDLL.IDGenerator;
using DBAccessCoreDLL.Accesser;
using DBAccessCoreDLL.Accesser.Asset;
using NetApplictionServiceDLL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace BusinessAdminDLL.AutofacModule
{
    /// <summary>
    /// 
    /// </summary>
    public class AdminAutofacModule : Module
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
            },
    GVariable.configuration["RedisIDGenerator:Password"])).As<IIDGenerator>().SingleInstance();


                builder.RegisterInstance<RedisCaptchaHelper>(new RedisCaptchaHelper(new List<string>
                {
                    GVariable.configuration["RedisCaptchaContainer:Passport"]
                },
                    GVariable.configuration["RedisCaptchaContainer:Password"])).As<ICaptchaHelper>().SingleInstance();

                builder.RegisterInstance<IAssetHelper>(
                new COSAssetHelper
                (
                    GVariable.configuration["COS:AppId"],
                    GVariable.configuration["COS:SecretId"],
                    GVariable.configuration["COS:SecretKey"],
                    GVariable.configuration["COS:Region"])
                ).As<IAssetHelper>().SingleInstance();


                #region Biz

                builder.RegisterType<AccountBizServices>().As<IAccountsBizServices>().InstancePerLifetimeScope();
                builder.RegisterType<AccountLoginBizServices>().As<IAccountLoginBizServices>().InstancePerLifetimeScope();


                builder.RegisterType<RolesBizServices>().As<IRolesBizServices>().InstancePerLifetimeScope();
                builder.RegisterType<RoutePageBizServices>().As<IRoutePageBizServices>().InstancePerLifetimeScope();
                builder.RegisterType<AppInfoBizServices>().As<IAppInfoBizServices>().InstancePerLifetimeScope();

                #endregion

                #region DB 访问器

                builder.RegisterType<AccountAccesser>().As<IAccountAccesser>().InstancePerLifetimeScope();
                builder.RegisterType<RoleAccesser>().As<IRoleAccesser>().InstancePerLifetimeScope();
                builder.RegisterType<RoutePageAccesser>().As<IRoutePageAccesser>().InstancePerLifetimeScope();
                builder.RegisterType<AppInfoAccesser>().As<IAppInfoAccesser>().InstancePerLifetimeScope();
                #endregion

                #region AutoMapper

                //注册AutoMapper配置文件, Register   
                builder.Register(ctx =>
                {
                    MapperConfiguration MapperConfig = new MapperConfiguration(cfg =>
                    {
                        cfg.AddMaps(new[] { typeof(BizAdminProfile).Assembly });
                    });

                    MapperConfig.CompileMappings();
                    return MapperConfig;
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
                Debug.WriteLine($"Error ================== AdminAutofacModule {ex.Message}");
                Console.WriteLine($"Error ==================  AdminAutofacModule {ex.Message}");
            }

        }
    }
}
