using Autofac;
using BusinessCoreDLL.Accounts;
using DBAccessBaseDLL.IDGenerator;
using DBAccessCoreDLL.Accesser;
using NetApplictionServiceDLL;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessCoreDLL.AutofacModule
{
    /// <summary>
    /// 
    /// </summary>
    public class CoreModule : Module
    {
        /// <summary>
        /// Autofac 基本注入
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AccountBizServices>().As<IAccountsBizServices>().InstancePerLifetimeScope(); //.InstancePerLifetimeScope();
            builder.RegisterInstance<DBIDGenerator>(new DBIDGenerator()).As<IIDGenerator>().SingleInstance();

            builder.RegisterType<AccountAccesser>().As<IAccountAccesser>().InstancePerLifetimeScope();
            // 注册到BaseController的所有子类
            //builder.RegisterAssemblyTypes(typeof(BaseController).Assembly)
            //    .Where(classType => classType.IsSubclassOf(typeof(BaseController)));

        }
    }
}
