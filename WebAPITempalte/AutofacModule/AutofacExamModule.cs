using Autofac;
using BaseDLL;
using BaseDLL.Helper;
using NetApplictionServiceDLL;
using WebApp.Base;

namespace WebAPI.AutofacModule
{
    /// <summary>
    /// Autofac 模块
    /// </summary>
    public class AutofacExamModule : Module
    {
        /// <summary>
        /// Autofac栗子
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            // 示例
            builder.Register(c => new CoreHelper()).As<ICoreHelper>().InstancePerRequest(); //.InstancePerLifetimeScope();

            // 注册到BaseController的所有子类
            builder.RegisterAssemblyTypes(typeof(Class1).Assembly)
                   .Where(classType => classType.IsSubclassOf(typeof(Class1)));
        }
    }
}
