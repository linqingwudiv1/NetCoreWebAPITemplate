using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBAccessDLL.Static
{
    /// <summary>
    /// DI注入类 ，保存配置文件中的字符串连接。
    /// </summary>
    public class Option_ConnctionString
    {
        /// <summary>
        /// LTE数据库
        /// </summary>
        public string LTEConn { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string JZXN_DW_165 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string JK_DW_168 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PublicService_167 { get; set; }
    }
}
