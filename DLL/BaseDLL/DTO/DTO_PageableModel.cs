using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BaseDLL.DTO
{
    /// <summary>
    /// 分页查询-响应
    /// </summary>
    public class DTO_PageableModel<T>
    {
        public IList<T> data { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DefaultValue(null)]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public long? total { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DefaultValue(1L)]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public long? pageNum { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DefaultValue(20L)]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public long? pageSize { get; set; }
    }

    /// <summary>
    /// 分页查询-请求,标准请求
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DTO_PageableQueryModel<T> : DTO_PageableQueryInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public T data { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class DTO_PageableQueryInfo
    {

        /// <summary>
        /// 
        /// </summary>
        [DefaultValue(1L)]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int pageNum { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DefaultValue(20L)]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int pageSize { get; set; }
    }
}
