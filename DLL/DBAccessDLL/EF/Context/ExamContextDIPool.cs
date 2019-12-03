using DBAccessDLL.EF.Context.Base;
using DBAccessDLL.EF.Entity;
using Microsoft.EntityFrameworkCore;

namespace DBAccessDLL.EF.Context
{
    /// <summary>
    /// 线程池DI，不推荐. 理由:不能对EF进行水平业务拆分的规则处理
    /// </summary>
    public class ExamContextDIPool : CommonDBContext<ExamContextDIPool>
    {
        /// <summary>
        /// 实体:DbQuery
        /// </summary>
        virtual public DbSet<Account> Accounts { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        virtual public DbSet<AccountRole> AccountRoles { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        virtual public DbSet<RoutePage> RoutePages { get; protected set; }
     
        /// <summary>
        /// 可访问页面权限列表
        /// </summary>
        virtual public DbSet<RoutePageRole> RoutePageRoles { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        virtual public DbSet<BizSystemLog> SystemLogs { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        virtual public DbSet<Role> Roles { get; protected set; }

        /// <summary>
        /// 视图：Account
        /// </summary>
        virtual public DbSet<View_AccountFemale> view_AccountFemales { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options">多表组合</param>
        public ExamContextDIPool(DbContextOptions<ExamContextDIPool> options)
            :base(options)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Account>(new AccountEFConfig());
            modelBuilder.ApplyConfiguration<AccountRole>(new AccountRoleEFConfig());
            modelBuilder.ApplyConfiguration<RoutePage>(new RoutePageEFConfig());
            modelBuilder.ApplyConfiguration<RoutePageRole>(new RoutePageRoleEFConfig());
            modelBuilder.ApplyConfiguration<Role>(new RoleEFConfig());
            modelBuilder.ApplyConfiguration<BizSystemLog>(new SystemLogEFConfig());

            // Database 必须存在 View_AccountFemale 视图
            modelBuilder.ApplyConfiguration<View_AccountFemale>(new View_AccountFemaleEFConfig());

            base.OnModelCreating(modelBuilder);
        }
    }
}