
// #define Use_SqlServerDB
// #define Use_MySqlDB
#define Use_SqliteDB
// #define Use_MemoryDB

using Microsoft.EntityFrameworkCore;

namespace DBAccessDLL.EF.Context.Base
{
    /// <summary>
    /// 选择性预编译判断，减少if,提升性能
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

        public BaseContext(DbContextOptions<DBCtx> options, string _ConnString = "Data Source=sqliteTestDB.db")
        : base(options)
        {
            ConnString = _ConnString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
#if Use_SqlServerDB
            
#elif Use_MySqlDB
            optionsBuilder.UseMySQL(ConnString);
#elif Use_SqliteDB
            optionsBuilder.UseSqlite(ConnString);
#elif Use_MemoryDB
            optionsBuilder.UseMemoryCache();
#endif
            base.OnConfiguring(optionsBuilder);
        }
    }
}
