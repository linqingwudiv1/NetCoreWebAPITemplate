using DTOModelDLL.Common.Store;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetApplictionServiceDLL
{
    /// <summary>
    /// Session 扩展
    /// </summary>
    public static class SessionExtensions
    {
        public static void SetObject(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static DTO_StoreAccount GetStoreAccount(this ISession session)
        {
            return session.GetObject<DTO_StoreAccount>(GVariable.StoreAccount);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="data"></param>
        public static void SetStoreAccount(this ISession session, DTO_StoreAccount data)
        {
            session.SetObject(GVariable.StoreAccount, data);
        }
    }
}
