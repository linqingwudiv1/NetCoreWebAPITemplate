using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseDLL.DTO
{
    /// <summary>
    /// 
    /// </summary>
    public class DTO_PageableModel<T>
    {
        public IList<T> data { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public long? total { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public long? pageNum { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public long? pageSize { get; set; }
    }
}
