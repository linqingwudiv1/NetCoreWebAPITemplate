﻿namespace DBAccessCoreDLL.Static
{
    /// <summary>
    /// DI注入类 ，保存配置文件中的字符串连接(分表分库自行处理逻辑)
    /// </summary>
    public class Option_ConnctionString
    {
        /// <summary>
        /// Case: LTE数据库
        /// </summary>
        public string LTEConn { get; set; }

    }
}