using BaseDLL.DTO.Store;
using BusinessCoreDLL.DTOModel.API.Users;
using DBAccessCoreDLL.EFORM.Context;
using DBAccessCoreDLL.EFORM.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetApplictionServiceDLL;
using System.Collections.Generic;
using System.Linq;

namespace BusinessCoreDLL.Extensison
{
    /// <summary>
    /// 登录状态
    /// </summary>
    public enum EM_LoginState 
    {
        /// <summary>
        /// 
        /// </summary>
        Pass,
        /// <summary>
        /// 
        /// </summary>
        NoExist,
        /// <summary>
        /// 
        /// </summary>
        PasswordError
    }
}
