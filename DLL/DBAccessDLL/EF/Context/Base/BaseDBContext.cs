
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
    public class BaseDBContext<DBCtx> : DbContext where DBCtx : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        protected string ConnString { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_ConnString"></param>
        public BaseDBContext(string _ConnString = "")
        : base()
        {
            ConnString = _ConnString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <param name="_ConnString"></param>
        public BaseDBContext(DbContextOptions<DBCtx> options, string _ConnString = "")
        : base(options)
        {
            ConnString = _ConnString;
        }
    }
}
