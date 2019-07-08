﻿
// #define Q_SqlServerDB
// #define Q_MySqlDB
// #define Q_SqliteDB
// #define Q_MemoryDB

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace DBAccessDLL.EF.Context.Base
{
    /// <summary>
    /// 常用
    /// </summary>
    /// <typeparam name="DBCtx"></typeparam>
    public class CommonDBContext<DBCtx> : BaseDBContext<DBCtx> where DBCtx : BaseDBContext<DBCtx>
    {

        public CommonDBContext(string _ConnString = "")
        : base(_ConnString)
        {
        }

        public CommonDBContext(DbContextOptions<DBCtx> options, string _ConnString = "")
        : base(options, _ConnString)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
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
#endif
            base.OnConfiguring(optionsBuilder);
        }
    }
}
