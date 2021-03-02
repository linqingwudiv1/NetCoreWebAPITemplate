using DBAccessBaseDLL.EF.Context;
using DBAccessCoreDLL.EFORM.Entity;
using DBAccessCoreDLL.Entity.Asset;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;

namespace DBAccessCoreDLL.EFORM.Context
{
    /// <summary>
    /// Test数据库,对数据的操作应该写在这里...
    /// </summary>
    public class CoreContext : BaseDBContext<CoreContext>
    {
        /// <summary>
        /// 实体:DbQuery
        /// </summary>
        virtual public DbSet<Account> Accounts { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        virtual public DbSet<AppInfo> AppInfos { get; protected set; }

        /// <summary> 
        /// 
        /// </summary>
        virtual public DbSet<AccountIdentityAuth> AccountIdentityAuths { get; protected set;}

    /// <summary>
    /// 
    /// </summary>
    virtual public DbSet<AccountRole> AccountRoles { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        virtual public DbSet<RoutePages> RoutePages { get; protected set; }

        /// <summary>w1A
        /// 不同权限下可访问的页面权限列表
        /// </summary>
        virtual public DbSet<RoutePageRole> RoutePageRoles { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        //virtual public DbSet<BizSystemLog> SystemLogs { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        virtual public DbSet<Role> Roles { get; protected set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="_ConnString">多表组合</param>
        public CoreContext(string _ConnString = "")
            : base(_ConnString)
        { 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public CoreContext(DbContextOptions<CoreContext> options)
        : base(options)
        {
        }


        /// <summary>
        /// ExamContext
        /// </summary>
        /// <param name="options"></param>
        /// <param name="_ConnString"></param>
        public CoreContext(DbContextOptions<CoreContext> options, string _ConnString = "")
            : base(options, _ConnString)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured &&
                 !String.IsNullOrEmpty(this.ConnString))
            {
                optionsBuilder.UseNpgsql(this.ConnString, b =>
                {
                });
            }
            
            base.OnConfiguring(optionsBuilder);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Account>      ( new AccountEFConfig());
            modelBuilder.ApplyConfiguration<AccountIdentityAuth>(new AccountIdentityAuthEFConfig());
            modelBuilder.ApplyConfiguration<AccountRole>  ( new AccountRoleEFConfig());
            modelBuilder.ApplyConfiguration<RoutePages>    ( new RoutePageEFConfig());
            modelBuilder.ApplyConfiguration<Role>(new RoleEFConfig());
            modelBuilder.ApplyConfiguration<RoutePageRole>( new RoutePageRoleEFConfig()); 

            modelBuilder.ApplyConfiguration<AppInfo>(new AppInfoEFConfig() );
            //modelBuilder.ApplyConfiguration<BizSystemLog>(new SystemLogEFConfig());

            //Database/必须存在View_AccountFemale视图
            //modelBuilder.ApplyConfiguration<View_AccountFemale>(new View_AccountFemaleEFConfig());
            base.OnModelCreating(modelBuilder);
        }
    }
}