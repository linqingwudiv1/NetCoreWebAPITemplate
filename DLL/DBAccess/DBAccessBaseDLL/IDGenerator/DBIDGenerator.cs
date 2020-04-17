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
    class DBIDGenerator : IIDGenerator
    {

        protected static string SqlCmd = @"Exec PD_GenerateID @TagName;";

        protected static string conn = @"Data Source=129.204.160.155;UID=sa;PWD=1qaz@WSX;
                                         Initial Catalog=TableIDCounter;
                                         Connect Timeout=30;Min Pool Size=10;Max Pool Size=100";

        /// <summary>
        /// 
        /// </summary>
        public DBIDGenerator()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public long GetNewID<T>()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                return conn.QuerySingle<long>(DBIDGenerator.SqlCmd, { @TagName = ""});
            }
        }
    }
}
