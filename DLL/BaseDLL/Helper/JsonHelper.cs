using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BaseDLL.Helper
{
    public static class JsonHelper
    {
        /// <summary>
        /// 从磁盘加载JSON数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T loadJsonFromFile<T>(string path) where T : new()
        {
            try
            {
                string json = File.ReadAllText(path);
                T obj = JsonConvert.DeserializeObject<T>(json);
                return obj;
            }
            catch (Exception ex) 
            {
                Console.Error.WriteLine(ex.Message);
                return default(T);
            }
        }

    }
}
