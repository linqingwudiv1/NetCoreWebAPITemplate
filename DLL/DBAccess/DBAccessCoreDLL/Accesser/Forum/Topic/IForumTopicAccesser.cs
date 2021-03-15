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
    public interface IForumTopicAccesser : IAccesser<ForumTopic, Int64>
    {
        /// <summary>
        /// DB Layer
        /// </summary>
        CoreContextDIP db { get; protected set; }
    }
}
