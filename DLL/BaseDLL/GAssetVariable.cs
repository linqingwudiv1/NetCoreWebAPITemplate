using Microsoft.Extensions.Configuration;
using System;

namespace BaseDLL
{
    /// <summary>
    /// 
    /// </summary>
    public static class GAssetVariable
    {
        /// <summary>
        /// AppID
        /// </summary>
        static public string AppId
        {
            get
            {
                return GVariable.configuration["COS:AppId"];
            }
        }

        /// <summary>
        /// 默认密钥ID
        /// </summary>
        static public string SecretId
        {
            get
            {
                return GVariable.configuration["COS:SecretId"];
            }
        }

        /// <summary>
        /// 默认密钥Key
        /// </summary>
        static public string SecretKey
        {
            get
            {
                return GVariable.configuration["COS:SecretKey"];
            }
        }

        /// <summary>
        /// 默认区域
        /// </summary>
        static public string Region
        {
            get
            {
                return GVariable.configuration["COS:Region"];
            }
        }

        /// <summary>
        /// 默认Bucket
        /// </summary>
        static public string Bucket
        {
            get
            {
                return GVariable.configuration["COS:Bucket"];
            }
        }
    }
}
