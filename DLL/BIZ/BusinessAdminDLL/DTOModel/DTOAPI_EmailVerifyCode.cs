﻿using Newtonsoft.Json;
using System.ComponentModel;

namespace BusinessAdminDLL.DTOModel.API
{
    /// <summary>
    /// 
    /// </summary>
    public class DTOAPI_EmailVerifyCode
    {
        /// <summary>
        /// 
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DefaultValue(null)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string verifyCode { get; set; }
    }
}
