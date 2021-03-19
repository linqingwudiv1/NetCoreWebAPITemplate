using System;
using System.Collections.Generic;
using System.Text;

namespace AdminServices.Command.PageRouteRole
{
    /// <summary>
    /// 
    /// </summary>
    public class AddPageRoutesCommand
    {
        /// <summary>
        /// 
        /// </summary>
        public ICollection <DTOIn_PageRoute> routes { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class DTOIn_PageRoute 
    {
        /// <summary>
        /// 
        /// </summary>
        public Int64 Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Nullable<Int64> ParentId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string HierarchyPath { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Platform { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Component { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RouteName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Redirect { get; set; }

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
