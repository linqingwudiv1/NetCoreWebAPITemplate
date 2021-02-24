using BaseDLL.Helper.SMS;
using BusinessCoreDLL.DTOModel.API.Users;
using DBAccessCoreDLL.Accesser;
using DBAccessCoreDLL.EFORM.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace BusinessCoreDLL.Accounts.WF
{
    /// <summary>
    /// 
    /// </summary>
    public class FindAccountByIDStepBody : StepBody
    {
        public Account account;
        //public string password;
        //readonly IAccountAccesser accesser;
        DTOAPIReq_Login login { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_accesser"></param>
        public FindAccountByIDStepBody(IAccountAccesser _accesser) 
        {
            // this.accesser = _accesser
            // this.accesser = _accesser;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            //ExecutionResult.Branch();
            return ExecutionResult.Next();
            //throw new NotImplementedException();
        }
    }
}
