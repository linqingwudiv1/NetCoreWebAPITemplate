﻿using Autofac;
using BusinessCoreDLL.Users;
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
        /// Autofac栗子
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            // 示例
            builder.RegisterType<UsersBizServices>().As<IUsersBizServices>().InstancePerLifetimeScope(); //.InstancePerLifetimeScope();
            // 注册到BaseController的所有子类
            builder.RegisterAssemblyTypes(typeof(BaseController).Assembly)
                .Where(classType => classType.IsSubclassOf(typeof(BaseController)));
        }
    }
}