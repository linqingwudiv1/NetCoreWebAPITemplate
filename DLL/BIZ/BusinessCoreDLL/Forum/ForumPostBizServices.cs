using DBAccessCoreDLL.Forum;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessCoreDLL.Forum
{
    /// <summary>
    /// 
    /// </summary>
    public class ForumPostBizServices : IForumPostBizServices
    {
        private readonly IForumPostAccesser Accesser;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_Accesser"></param>
        public ForumPostBizServices(IForumPostAccesser _Accesser)
        {
            this.Accesser = _Accesser;
        }
    }
}
