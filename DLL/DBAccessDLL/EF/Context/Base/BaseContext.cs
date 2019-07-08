
// #define Q_SqlServerDB
// #define Q_MySqlDB
// #define Q_SqliteDB
// #define Q_MemoryDB

using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace DBAccessDLL.EF.Context.Base
{
    /// <summary>
    /// 基础
    /// </summary>
    /// <typeparam name="DBCtx"></typeparam>
    public class BaseContext<DBCtx> : DbContext where DBCtx : DbContext
    {
        protected string ConnString { get; set; }

        public BaseContext(string _ConnString = "")
        : base()
        {
            ConnString = _ConnString;
        }

        public BaseContext(DbContextOptions<DBCtx> options, string _ConnString = "")
        : base(options)
        {
            ConnString = _ConnString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
#if Q_SqlServerDB

#elif Q_MySqlDB
            optionsBuilder.UseMySQL(ConnString);
#elif Q_SqliteDB
            optionsBuilder.UseSqlite(ConnString);
#elif Q_MemoryDB
            optionsBuilder.UseMemoryCache();
#endif
            base.OnConfiguring(optionsBuilder);
        }
    }
}
