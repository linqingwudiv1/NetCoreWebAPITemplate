using DBAccessCoreDLL.Forum;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessCoreDLL.Forum
{
    /// <summary>
    /// 
    /// </summary>
    public class ForumReplyBizServices : IForumReplyBizServices
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IForumReplyAccesser Accesser;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_Accesser"></param>
        public ForumReplyBizServices(IForumReplyAccesser _Accesser)
        {
            this.Accesser = _Accesser;
        }
    }
}
