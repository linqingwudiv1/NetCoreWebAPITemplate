using BaseDLL.Helper.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using Dapper;
using Microsoft.Data.SqlClient;

namespace DBAccessBaseDLL.IDGenerator
{
    /// <summary>
    /// 
    /// </summary>
    public class DBIDGenerator : AbsIDGenerator, IIDGenerator
    {
        /// <summary>
        /// 
        /// </summary>
        protected string SqlCmd = @"Exec PD_GenerateID @TagName;";

        /// <summary>
        /// 
        /// </summary>
        protected string conn = @"Data Source=129.204.160.155;UID=sa;PWD=1qaz@WSX;
                                         Initial Catalog=TableIDCounter;
                                         Connect Timeout=30;Min Pool Size=10;Max Pool Size=100;";

        /// <summary>
        /// 
        /// </summary>
        public DBIDGenerator()
        {
            //
            //this.SqlCmd = sqlcmd;
            //this.conn = conn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Entity"></typeparam>
        /// <returns></returns>
        public long GetNewID<Entity>() where Entity : new()
        {
            string key = this.GetKey<Entity>();
            using (SqlConnection conn = new SqlConnection(this.conn))
            {
                return conn.QuerySingle<long>(this.SqlCmd, new { TagName = key });
            }
        }
    }
}
