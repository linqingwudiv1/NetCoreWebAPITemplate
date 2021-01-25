using System;
using System.Collections.Generic;
using System.Text;

namespace AdminServices.Command.Role
{
    /// <summary>
    /// 
    /// </summary>
    public class AddRoleCommand
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
        public string description { get; set; }


        public ICollection<DTOIn_PageRouteId> routes { get; set; }
    }
}
