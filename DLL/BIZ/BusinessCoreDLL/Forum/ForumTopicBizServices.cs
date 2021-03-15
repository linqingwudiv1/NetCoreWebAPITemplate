using DBAccessCoreDLL.Forum;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessCoreDLL.Forum
{
    /// <summary>
    /// 
    /// </summary>
    public class ForumTopicBizServices : IForumTopicBizServices
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IForumTopicAccesser Accesser;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_Accesser"></param>
        public ForumTopicBizServices(IForumTopicAccesser _Accesser)
        {
            this.Accesser = _Accesser;
        }
    }
}
