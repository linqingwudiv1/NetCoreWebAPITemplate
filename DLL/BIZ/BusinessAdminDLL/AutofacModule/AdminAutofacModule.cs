﻿using Autofac;
using AutoMapper;
using BusinessAdminDLL.Accounts;
using BusinessAdminDLL.DTOModel.AutoMapper;
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
            builder.RegisterInstance<RedisIDGenerator>(new RedisIDGenerator(new List<string> { "127.0.0.1:10110" }, "abc123,") ).As<IIDGenerator>().SingleInstance();

            #region Biz

            builder.RegisterType<AccountBizServices>().As<IAccountsBizServices>().InstancePerLifetimeScope();
            builder.RegisterType<RolesBizServices>().As<IRolesBizServices>().InstancePerLifetimeScope();
            builder.RegisterType<RoutePageBizServices>().As<IRoutePageBizServices>().InstancePerLifetimeScope();

            #endregion

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
                   cfg.AddProfile(typeof(BizAdminProfile));
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
    }
}
