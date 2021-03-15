using Autofac;
using AutoMapper;
using BaseDLL;
using BaseDLL.Helper.Asset;
using BaseDLL.Helper.Captcha;
using BusinessCoreDLL.Accounts;
using BusinessCoreDLL.Asset;
using BusinessCoreDLL.AutoMapper;
using BusinessCoreDLL.Blogs;
using BusinessCoreDLL.Forum;
using DBAccessBaseDLL.IDGenerator;
using DBAccessCoreDLL.Accesser;
using DBAccessCoreDLL.Accesser.Asset;
using DBAccessCoreDLL.Forum;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BusinessCoreDLL.AutofacModule
{
    /// <summary>
    /// 
    /// </summary>
    public class CoreModule : Module
    {
        /// <summary>
        /// Autofac 注入服务
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            #region middle services
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

                #endregion

                #region Biz

                builder.RegisterType<AccountBizServices>().As<IAccountsBizServices>().InstancePerLifetimeScope();
                builder.RegisterType<AccountLoginBizServices>().As<IAccountLoginBizServices>().InstancePerLifetimeScope();
                builder.RegisterType<AccountFotgotPwdBizServices>().As<IAccountFotgotPwdBizServices>().InstancePerLifetimeScope();
                builder.RegisterType<AccountRegisterBizServices>().As<IAccountRegisterBizServices>().InstancePerLifetimeScope();

                builder.RegisterType<AppInfoBizServices>().As<IAppInfoBizServices>().InstancePerLifetimeScope();
                builder.RegisterType<BlogsBizServices>().As<IBlogsBizServices>().InstancePerLifetimeScope();

                builder.RegisterType<ForumBizServices>().As<IForumBizServices>().InstancePerLifetimeScope();

                #endregion

                #region DB 访问器

                builder.RegisterType<AccountAccesser>().As<IAccountAccesser>().InstancePerLifetimeScope();
                builder.RegisterType<AppInfoAccesser>().As<IAppInfoAccesser>().InstancePerLifetimeScope();

                builder.RegisterType<ForumModuleAccesser>() .As<IForumModuleAccesser>() .InstancePerLifetimeScope();
                builder.RegisterType<ForumTopicAccesser>()  .As<IForumTopicAccesser>()  .InstancePerLifetimeScope();
                builder.RegisterType<ForumPostAccesser>()   .As<IForumPostAccesser>()   .InstancePerLifetimeScope();
                builder.RegisterType<ForumReplyAccesser>()  .As<IForumReplyAccesser>()  .InstancePerLifetimeScope();

                #endregion

                #region AutoMapper

                //注册AutoMapper配置文件, Register   
                builder.Register(ctx =>
                {
                    MapperConfiguration MapperConfig = new MapperConfiguration(cfg =>
                    {
                        cfg.AddMaps(new[] { typeof(BizCoreProfile).Assembly });
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
                Debug.WriteLine($"Error ================== CoreAutofacModule {ex.Message}");
                Console.WriteLine($"Error ==================  CoreAutofacModule {ex.Message}");
            }

            // 注册到BaseController的所有子类
            //builder.RegisterAssemblyTypes(typeof(BaseController).Assembly)
            //    .Where(classType => classType.IsSubclassOf(typeof(BaseController)));

        }
    }
}
