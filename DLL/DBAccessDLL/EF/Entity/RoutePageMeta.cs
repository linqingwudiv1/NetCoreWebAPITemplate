using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBAccessDLL.EF.Entity
{
    /// <summary>
    /// RoutePage 子表/详情表
    /// </summary>
    [Owned]
    public  class RoutePageMeta
    {
        /// <summary>
        /// 
        /// </summary>
        public string ActiveMenu { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Affix { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool NoCache { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool AlwaysShow { get; set; }

        /// <summary>
        /// 是否隐藏
        /// </summary>
        public bool Hidden { get; set; }

    }
}
