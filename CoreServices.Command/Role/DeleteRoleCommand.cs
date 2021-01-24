using System;
using System.Collections.Generic;
using System.Text;

namespace AdminServices.Command.Role
{
    public class DeleteRoleCommand
    {
        /// <summary>
        /// 
        /// </summary>
        public long key { get; set; } 
    }


    public class DeleteRoleCommandResult 
    {
        public int effectCount { get; set; }
    }
}
