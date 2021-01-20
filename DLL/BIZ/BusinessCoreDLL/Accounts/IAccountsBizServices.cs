using BusinessCoreDLL.DTOModel.API.Users;
using DBAccessCoreDLL.EFORM.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessCoreDLL.Accounts
{


    /// <summary>
    /// 
    /// </summary>
    public enum ERegisterAccountState
    {
        /// <summary>
        /// 
        /// </summary>
        Error,
        /// <summary>
        /// 
        /// </summary>
        Success,
        /// <summary>
        /// 
        /// </summary>
        ExistAccount,
        /// <summary>
        /// 注册资料不匹配规则
        /// </summary> 
        FormatNotMatch
    }

    /// <summary>
    /// 
    /// </summary>
    public class RegisterAccountInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public RegisterAccountInfo()
        {
            this.State = ERegisterAccountState.Success;
        }

        /// <summary>
        /// 
        /// </summary>
        public Account account { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ERegisterAccountState State { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Message { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IAccountsBizServices
    {
        //void Login();
        //
        //void Logout();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        RegisterAccountInfo Register(DTOAPIReq_Register model);
    }
}
