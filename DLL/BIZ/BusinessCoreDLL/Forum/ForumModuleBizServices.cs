using DBAccessCoreDLL.Forum;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessCoreDLL.Forum
{
    /// <summary>
    /// up4
    /// </summary>
    public class ForumModuleBizServices : IForumModuleBizServices
    {
        private readonly IForumModuleAccesser Accesser;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_Accesser"></param>
        public ForumModuleBizServices( IForumModuleAccesser _Accesser) 
        {
            this.Accesser = _Accesser;
        }
    }
}
