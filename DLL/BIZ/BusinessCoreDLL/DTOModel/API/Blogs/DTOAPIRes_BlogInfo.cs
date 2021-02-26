using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessCoreDLL.DTOModel.API.Blogs
{
    /// <summary>
    /// 
    /// </summary>
    public class DTOAPIRes_BlogInfo
    {

        /// <summary>
        /// 
        /// </summary>
        public long userId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string username { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string joinTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string avatar { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string introduction { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string blogTitle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string blogDesc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string blogCover { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string phone { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string email { get; set; }
    }
}
