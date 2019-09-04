using DBAccessDLL.EF.Context.Base;
using DBAccessDLL.EF.Entity;
using Microsoft.EntityFrameworkCore;

namespace DBAccessDLL.EF.Context
{
    /// <summary>
    /// Test数据库
    /// </summary>
    public class ExamContext : CommonDBContext<ExamContext>
    {
        virtual public DbSet<Account> Accounts { get; protected set; }

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Account>(new AccountEFConfig());
            base.OnModelCreating(modelBuilder);
        }
    }
}