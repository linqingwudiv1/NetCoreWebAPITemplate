using BaseDLL.Helper;
using BaseDLL.Helper.SMS;
using BaseDLL.Helper.Smtp;
using BusinessCoreDLL.Base;
using BusinessCoreDLL.DTOModel.API.Users;
using DBAccessBaseDLL.IDGenerator;
using DBAccessCoreDLL.Accesser;
using DBAccessCoreDLL.EF.Context;
using DBAccessCoreDLL.EF.Entity;
using System;

namespace BusinessCoreDLL.Accounts
{
    /// <summary>
    /// 
    /// </summary>
    class AccountBizServices : BaseBizServices, IAccountsBizServices
    {

        /// <summary>
        /// DAO层
        /// </summary>
        protected IAccountAccesser accesser { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IDGenerator"></param>
        /// <param name="AccountAccesser"></param>
        public AccountBizServices(IIDGenerator IDGenerator, IAccountAccesser AccountAccesser)
            : base()
        {
            this.accesser = AccountAccesser;
        }


        /// <summary>
        /// 
        /// </summary>
        public enum ERegisterAccountState
        {
            /// <summary>
            /// 
            /// </summary>
            Error         ,
            /// <summary>
            /// 
            /// </summary>
            Success       ,
            /// <summary>
            /// 
            /// </summary>
            ExistAccount  ,
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
        public RegisterAccountInfo Register(DTOAPI_Register model) 
        {

            RegisterAccountInfo RegisterInfo = RegisterAccountVerify(model);

            if (RegisterInfo.State != ERegisterAccountState.Success) 
            {
                return RegisterInfo;
            }

            Tuple<Account, EFindAccountWay> FindAccountResult = accesser.Get( passport: model.Passport  , 
                                                                              username: model.Username  , 
                                                                                 email: model.EMail     , 
                                                                                 phone: model.Phone         );

            switch (FindAccountResult.Item2)
            {
                case EFindAccountWay.Id:
                    {
                        //ID is exist
                        RegisterInfo.State = ERegisterAccountState.ExistAccount;
                        RegisterInfo.Message = "ID is registered";
                        break;
                    }
                case EFindAccountWay.UserName:
                    {
                        //username is exist.
                        RegisterInfo.State = ERegisterAccountState.ExistAccount;
                        RegisterInfo.Message = "username is registered";
                        break;
                    }
                case EFindAccountWay.Passport:
                    {
                        //passport is exist
                        RegisterInfo.State = ERegisterAccountState.ExistAccount;
                        RegisterInfo.Message = "passport is registered";
                        break;
                    }
                case EFindAccountWay.EMail:
                    {
                        //email is exist
                        RegisterInfo.State = ERegisterAccountState.ExistAccount;
                        RegisterInfo.Message = "email is registered";
                        break;
                    }
                case EFindAccountWay.Phone:
                    {
                        //phone is exist
                        RegisterInfo.State = ERegisterAccountState.ExistAccount;
                        RegisterInfo.Message = "Phone is registered";
                        break;
                    }
                case EFindAccountWay.NotFound:
                    {
                        //Register Logic
                        break;
                    }
            }


            return RegisterInfo;
        }

        #region private


        /// <summary>
        /// 用户验证
        /// </summary>
        /// <param name="model"></param>
        private RegisterAccountInfo RegisterAccountVerify(DTOAPI_Register model)
        {
            RegisterAccountInfo ret_model = new RegisterAccountInfo { account = null, State = ERegisterAccountState.Success, Message};

            if (!string.IsNullOrWhiteSpace(model.EMail) && !EmailHepler.IsValid(model.EMail))
            {
                ret_model.State = ERegisterAccountState.FormatNotMatch;
                ret_model.Message = "无效的邮箱";
            }

            if (!string.IsNullOrWhiteSpace(model.Password) && !PasswordHelper.IsValid(model.Password))
            {
                ret_model.State = ERegisterAccountState.FormatNotMatch;
                ret_model.Message = "无效的密码格式";
            }

            if (!string.IsNullOrWhiteSpace(model.Phone) && !PhoneHelper.IsValid(model.Phone))
            {
                ret_model.State = ERegisterAccountState.FormatNotMatch;
                ret_model.Message = "无效的手机号码";
            }

            if (!string.IsNullOrWhiteSpace(model.Username) && !PhoneHelper.IsValid(model.Username))
            {
                ret_model.State = ERegisterAccountState.FormatNotMatch;
                ret_model.Message = "无效的用户昵称";
            }

            if (!string.IsNullOrWhiteSpace(model.Passport) && !PhoneHelper.IsValid(model.Passport))
            {
                ret_model.State = ERegisterAccountState.FormatNotMatch;
                ret_model.Message = "无效的用户名";
            }

            return ret_model;
        }


        #endregion

    }
}
