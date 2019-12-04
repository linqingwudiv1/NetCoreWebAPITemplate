using DTOModelDLL.Common.Store;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace NetApplictionServiceDLL
{
    /// <summary>
    /// Session 扩展
    /// </summary>
    public static class SessionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetObject(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <returns></returns>
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
            return session.GetObject<DTO_StoreAccount>(GWebVariable.StoreAccount);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="data"></param>
        public static void SetStoreAccount(this ISession session, DTO_StoreAccount data)
        {
            session.SetObject(GWebVariable.StoreAccount, data);
        }
    }
}
