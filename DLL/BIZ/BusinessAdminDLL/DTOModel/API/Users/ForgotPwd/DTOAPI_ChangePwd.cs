using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessAdminDLL.DTOModel.API.Users
{
    /// <summary>
    /// 
    /// </summary>
    public class DTOAPI_ChangePwd
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string oldPwd { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string newPwd { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string verifyCode { get; set; }
    }
}
