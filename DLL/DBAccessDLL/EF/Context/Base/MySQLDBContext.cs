
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
    public class MySQLDBContext<DBCtx> : BaseDBContext<DBCtx> where DBCtx : BaseDBContext<DBCtx>
    {

        public MySQLDBContext(string _ConnString = "")
        : base(_ConnString)
        {
        }

        public MySQLDBContext(DbContextOptions<DBCtx> options, string _ConnString = "")
        : base(options, _ConnString)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(ConnString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
