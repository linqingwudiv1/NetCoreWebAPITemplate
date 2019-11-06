using System;
using System.Collections.Generic;
using System.Text;

namespace NetApplictionServiceDLL
{


    public static class GJWT
    {
        /// <summary>
        /// 这里为了演示，写死一个密钥。实际生产环境可以从配置文件读取,这个是用网上工具随便生成的一个密钥
        /// </summary>
        public static readonly string SecurityKey = "eWQQrkAn6pBZKnJt";

        /// <summary>
        /// 
        /// </summary>
        public static readonly string Domain = "https://localhost:44386/";
    }
}
