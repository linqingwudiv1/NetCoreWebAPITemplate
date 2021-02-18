using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BusinessAdminDLL.DTOModel.API.Users
{
    /// <summary>
    /// 
    /// </summary>
    public class DTOAPI_PhoneVerifyCode
    {
        /// <summary>
        /// 
        /// </summary>
        public string phone { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DefaultValue(null)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string code { get; set; }
    }
}
