using BusinessAdminDLL.DTOModel.API.Users;
using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCore.Interface;

namespace BusinessAdminDLL.Accounts.WF
{
    /// <summary>
    /// 
    /// </summary>
    public class WF_Login : IWorkflow<DTOAPIReq_Login>
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id => "WF_Login";

        /// <summary>
        /// 
        /// </summary>
        public int Version => 1;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public void Build(IWorkflowBuilder<DTOAPIReq_Login> builder)
        {


            // builder.StartWith();
            // throw new NotImplementedException();
        }
    }
}
