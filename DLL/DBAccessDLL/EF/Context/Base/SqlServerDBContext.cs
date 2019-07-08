
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
        protected string ConnString { get; set; }

        public SqlServerDBContext(string _ConnString = "")
        : base()
        {
            ConnString = _ConnString;
        }

        public SqlServerDBContext(DbContextOptions<DBCtx> options, string _ConnString = "")
        : base(options)
        {
            ConnString = _ConnString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
