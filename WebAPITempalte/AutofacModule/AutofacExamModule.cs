using Autofac;
using BaseDLL;
using BaseDLL.Helper;
using NetApplictionServiceDLL;

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
            
            builder.Register(c => new CoreHelper()).As<ICoreHelper>().InstancePerLifetimeScope();
            
            // 注册到BaseController的所有子类
            
            builder.RegisterAssemblyTypes(typeof( BaseController ).Assembly)
                   .Where( classType => classType
                   .IsSubclassOf(typeof(BaseController)));
        }
    }
}
