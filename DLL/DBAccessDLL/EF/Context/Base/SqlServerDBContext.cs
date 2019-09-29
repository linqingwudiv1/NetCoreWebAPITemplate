
// #define Q_SqlServerDB
// #define Q_MySqlDB
// #define Q_SqliteDB
// #define Q_MemoryDB

using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace DBAccessDLL.EF.Context.Base
{
    /// <summary>
    /// 预留类/便于对单系统多数据库类型环境开发支持
    /// </summary>
    /// <typeparam name="DBCtx"></typeparam>
    public class SqlServerDBContext<DBCtx> : DbContext where DBCtx : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        protected string ConnString { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_ConnString"></param>
        public SqlServerDBContext(string _ConnString = "")
        : base()
        {
            ConnString = _ConnString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <param name="_ConnString"></param>
        public SqlServerDBContext(DbContextOptions<DBCtx> options, string _ConnString = "")
        : base(options)
        {
            ConnString = _ConnString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnString);
            base.OnConfiguring(optionsBuilder);

        }
    }
}
