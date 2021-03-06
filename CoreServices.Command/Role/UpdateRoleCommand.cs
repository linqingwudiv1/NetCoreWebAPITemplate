﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AdminServices.Command.Role
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateRoleCommand
    {
        /// <summary>
        /// 
        /// </summary>
        public Int64 key { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string displayName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ICollection<DTOIn_PageRouteId> routes { get; set; }
    }
}
