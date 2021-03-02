using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BaseDLL.Helper.Asset
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAssetHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bucket"></param>
        /// <param name="allowPrefixs"></param>
        /// <param name="allowActions"></param>
        /// <returns></returns>
        dynamic GetTempToken(string bucket, string[] allowPrefixs = null, string[] allowActions = null, int keepTime = 1800);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bucket"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        byte[] Get(string bucket, string key);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Delete(string bucket, string key);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bucket"></param>
        /// <param name="key"></param>
        /// <param name="assetPath"></param>
        /// <returns></returns>
        bool Update(string bucket, string key, string assetPath);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bucket"></param>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(string bucket, string key, byte[] data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Add(string bucket, string key, byte[] data);


        bool Add(string bucket, string key, string assetPath );

        /// <summary>
        ///
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        string ConvertToKey(string url);
    }
}
