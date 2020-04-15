using DBAccessCoreDLL.EF.Context.Base;
using DBAccessCoreDLL.EF.Entity;
using Microsoft.EntityFrameworkCore;
using DBAccessCoreDLL.EF;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DBAccessCoreDLL.EF.Context
{
    /// <summary>
    /// Test数据库,对数据的操作应该写在这里...
    /// </summary>
    public class ExamContext : CommonDBContext<ExamContext>
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
        /// <param name="_ConnString">多表组合</param>
        public ExamContext(string _ConnString = "")
            : base( _ConnString)
        {
        }

        /// <summary>
        /// ExamContext
        /// </summary>
        /// <param name="options"></param>
        /// <param name="_ConnString"></param>
        public ExamContext(DbContextOptions<ExamContext> options, string _ConnString = "") 
            : base(options, _ConnString)
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