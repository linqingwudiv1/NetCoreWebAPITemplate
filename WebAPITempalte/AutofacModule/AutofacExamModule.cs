using Autofac;
using BaseDLL;
using BaseDLL.Helper;
using Microsoft.AspNetCore.Mvc;
using NetApplictionServiceDLL;

namespace WebAPI.AutofacModule
{
    /// <summary>
    /// 
    /// </summary>
    public class AutofacExamModule : Module
    {
        /// <summary>
        /// Autofac栗子
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            //演示，就一个方法
            builder.Register(c => new CoreHelper()).As<ICoreHelper>().InstancePerLifetimeScope();
            //注册到Controller的所有子类
            builder.RegisterAssemblyTypes(typeof( BaseController ).Assembly)
                   .Where( classType => classType
                   .IsSubclassOf(typeof(BaseController)));
        }
    }
}
