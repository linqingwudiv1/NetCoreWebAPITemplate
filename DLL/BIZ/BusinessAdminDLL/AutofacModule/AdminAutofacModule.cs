using Autofac;
using BusinessAdminDLL.Accounts;
using BusinessAdminDLL.Roles;
using BusinessAdminDLL.RoutePage;
using DBAccessBaseDLL.IDGenerator;
using DBAccessCoreDLL.Accesser;
using NetApplictionServiceDLL;
using System;
using System.Collections.Generic;
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
            builder.RegisterInstance<DBIDGenerator>(new DBIDGenerator()).As<IIDGenerator>().SingleInstance();

            #region Biz

            builder.RegisterType<AccountBizServices>()  .As<IAccountsBizServices>().InstancePerLifetimeScope();
            builder.RegisterType<RolesBizServices>()    .As<IRolesBizServices>().InstancePerLifetimeScope();
            builder.RegisterType<RoutePageBizServices>().As<IRoutePageBizServices>().InstancePerLifetimeScope();

            #endregion

            #region DB 访问器

            builder.RegisterType<AccountAccesser>().As<IAccountAccesser>().InstancePerLifetimeScope();
            builder.RegisterType<RoleAccesser>().As<IRoleAccesser>().InstancePerLifetimeScope();
            builder.RegisterType<RoutePageAccesser>().As<IRoutePageAccesser>().InstancePerLifetimeScope();

            #endregion

            // 注册到BaseController的所有子类
            //builder.RegisterAssemblyTypes(typeof(BaseController).Assembly)
            //    .Where(classType => classType.IsSubclassOf(typeof(BaseController)));

        }
    }
}
