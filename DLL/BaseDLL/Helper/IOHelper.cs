using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace BaseDLL.Helper
{
    /// <summary>
    /// IO Quick Helper
    /// </summary>
    public static class IOHelper
    {
        /// <summary>
        /// 获取bin目录
        /// </summary>
        /// <returns></returns>
        public static string GetBinRunDir()
        {
            string binDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return binDirectory;
        }
    }
}
