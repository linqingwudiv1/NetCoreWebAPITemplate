﻿using DBAccessDLL.EF.Context.Base;
using DBAccessDLL.EF.Entity;
using Microsoft.EntityFrameworkCore;
using DBAccessDLL.EF;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DBAccessDLL.EF.Context
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
        /// 视图：Account
        /// </summary>
        /// <param name="_ConnString"></param>
        virtual public DbQuery<View_AccountFemale> view_AccountFemales { get; }

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

            ///Database 必须存在 View_AccountFemale 视图
            modelBuilder.Query<View_AccountFemale>().ToView("View_AccountFemale").SetupBaseEntity();

            base.OnModelCreating(modelBuilder);
        }
    }
}