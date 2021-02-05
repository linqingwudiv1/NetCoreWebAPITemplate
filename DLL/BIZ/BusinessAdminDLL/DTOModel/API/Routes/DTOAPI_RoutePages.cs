using DBAccessCoreDLL.EFORM.Entity;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;

namespace BusinessAdminDLL.DTOModel.API.Routes
{
    /// <summary>
    /// 
    /// </summary>
    public class DTOAPI_RoutePages
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IList<DTOAPI_RoutePages> children { get; set; }

        /// <summary>
        /// RoutePages ID
        /// </summary>
        public Int64 id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DefaultValue("RoutePages")]
        public string name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DefaultValue(null)]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Int64? parentId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DefaultValue("")]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string hierarchyPath { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DefaultValue("")]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string path { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DefaultValue("")]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string redirect { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DefaultValue("")]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string component { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DTOAPI_RoutePagesMeta meta { get; set; }
    }
}
