using BaseDLL;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetApplictionServiceDLL
{

    /// <summary>
    /// 
    /// </summary>
    public static class GJWT
    {
        /// <summary>
        /// 这里为了演示，写死一个密钥。实际生产环境可以从配置文件读取,这个是用网上工具随便生成的一个密钥
        /// </summary>
        public static string SecurityKey
        {
            get
            {
                return GVariable.configuration["GJWT:SecurityKey"];
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string Domain
        {
            get
            {
                return GVariable.configuration["GJWT:Domain"];
            }
        }


        /// <summary>
        /// JWT Minute
        /// </summary>
        public static int Expires
        {
            get
            {
                return Int32.Parse(GVariable.configuration["GJWT:Expires"]);
            }
        }

        public static int ExpiresRefresh
        {
            get
            {
                return Int32.Parse(GVariable.configuration["GJWT:ExpiresRefresh"]);
            }
        }
    }
}
