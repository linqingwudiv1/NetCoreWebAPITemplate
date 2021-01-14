
// #define Q_SqlServerDB
// #define Q_MySqlDB
// #define Q_SqliteDB
// #define Q_MemoryDB

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace DBAccessBaseDLL.EF.Context
{
    /// <summary>
    /// 常用
    /// </summary>
    /// <typeparam name="DBCtx"></typeparam>
    public class CommonDBContext<DBCtx> : BaseDBContext<DBCtx> where DBCtx : BaseDBContext<DBCtx>
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_ConnString"></param>
        public CommonDBContext(string _ConnString = "")
        : base(_ConnString)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <param name="_ConnString"></param>
        public CommonDBContext(DbContextOptions<DBCtx> options, string _ConnString = "")
        : base(options, _ConnString)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            /*
                #if Q_SqlServerDB
                            optionsBuilder.UseSqlServer(ConnString);
                #elif Q_OracleDB
                            optionsBuilder.UseOracle(ConnString);
                #elif Q_PostgreSQLDB
                            optionsBuilder.UseNpgsql (ConnString);
                #elif Q_MySqlDB
                            optionsBuilder.UseMySQL(ConnString);
                #elif Q_SqliteDB
                            optionsBuilder.UseSqlite(ConnString);
                #elif Q_MemoryDB
                            // optionsBuilder.UseMemoryCache();
                #else
                            optionsBuilder.UseSqlite(ConnString);
                #endif
            */
            base.OnConfiguring(optionsBuilder);
        }
    }
}
