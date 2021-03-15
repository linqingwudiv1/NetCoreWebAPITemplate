using DBAccessBaseDLL.Accesser;
using System;
using System.Collections.Generic;
using System.Text;
using DBAccessCoreDLL.EFORM.Entity;
using DBAccessCoreDLL.EFORM.Context;
using BaseDLL.DTO;
using DBAccessCoreDLL.DTO.API.Users;
using System.Threading.Tasks;
using DBAccessCoreDLL.EFORM.Entity.Forum;

namespace DBAccessCoreDLL.Forum
{


    /// <summary>
    /// 
    /// </summary>
    public interface IForumModuleAccesser : IAccesser<ForumModule, Int64>
    {
        /// <summary>
        /// DB Layer
        /// </summary>
        CoreContextDIP db { get; protected set; }
    }
}
