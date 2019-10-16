
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
    public class NPGSQLDBContext<DBCtx> : BaseDBContext<DBCtx> where DBCtx : BaseDBContext<DBCtx>
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_ConnString"></param>
        public NPGSQLDBContext(string _ConnString = "")
        : base(_ConnString)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <param name="_ConnString"></param>
        public NPGSQLDBContext(DbContextOptions<DBCtx> options, string _ConnString = "")
        : base(options,_ConnString)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(ConnString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
