using BaseDLL.Helper.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using Dapper;
using Microsoft.Data.SqlClient;
using BaseDLL;
using Microsoft.Extensions.Configuration;

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
        protected string conn = @"Server=localhost\SQLEXPRESS;Database=TableIDCounterDB;Trusted_Connection=True;";

        /// <summary>
        /// 
        /// </summary>
        public DBIDGenerator()
        {
            this.conn = GVariable.configuration.GetConnectionString("IDGeneratorDB");
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
