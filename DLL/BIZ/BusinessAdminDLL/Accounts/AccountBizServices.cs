using BaseDLL.Helper;
using BaseDLL.Helper.SMS;
using BaseDLL.Helper.Smtp;
using BusinessAdminDLL.Base;
using BusinessAdminDLL.DTOModel.API.Users;
using DBAccessBaseDLL.IDGenerator;
using DBAccessCoreDLL.Accesser;
using DBAccessCoreDLL.EFORM.Context;
using DBAccessCoreDLL.EFORM.Entity;
using System;

namespace BusinessAdminDLL.Accounts
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
        protected IIDGenerator IDGenerator { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_IDGenerator"></param>
        /// <param name="AccountAccesser"></param>
        public AccountBizServices(IIDGenerator _IDGenerator, IAccountAccesser AccountAccesser)
            : base()
        {
            this.accesser = AccountAccesser;
            this.IDGenerator = _IDGenerator;
        }

        /// <summary>
        /// Register
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public RegisterAccountInfo Register(DTOAPIReq_Register model) 
        {
            RegisterAccountInfo RegisterInfo = RegisterAccountVerify(model);

            //注册未成功.....
            if (RegisterInfo.State != ERegisterAccountState.Success) 
            {
                return RegisterInfo;
            }

            //next
            bool isExistAccount = this.IsRegisterAccountExisted( model,ref RegisterInfo);

            if (isExistAccount) 
            {
                return RegisterInfo;
            }

            long NewID = IDGenerator.GetNewID<Account>();
            string NewUsername = model.Username ?? @$"User_{NewID.ToString()}" ;

            Account account = new Account 
            {
                Id       = NewID            ,
                Passport = model.Passport   ,
                Username = NewUsername      ,
                Email    = model.EMail      ,
                Phone    = model.Phone      ,
                Sex      = -1               ,
                // md5 secret key
                Password = MD5Helper.GetMd5Hash( model.Password )
            };
            
            accesser.Add(account);
            return RegisterInfo;
        }

        
        /// <summary>
        /// 
        /// </summary>
        public void Login(DTOAPIReq_Login LoginInfo)
        {
            // this.accesser.Get(LoginInfo.passport);
            // bool PhoneHelper.IsValid(LoginInfo.passport);
            // accesser.Get();
        }

        #region private

        /// <summary>
        /// 用户验证
        /// </summary>
        /// <param name="model"></param>
        private RegisterAccountInfo RegisterAccountVerify(DTOAPIReq_Register model)
        {
            RegisterAccountInfo ret_model = new RegisterAccountInfo { account = null, State = ERegisterAccountState.Success, Message = "" };

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="RegisterInfo"></param>
        private bool IsRegisterAccountExisted(DTOAPIReq_Register model,ref RegisterAccountInfo RegisterInfo) 
        {
            // next
            Tuple<Account, EFindAccountWay> FindAccountResult = accesser.Get( passport: model.Passport,
                                                                              username: model.Username,
                                                                                 email: model.EMail,
                                                                                 phone: model.Phone     );

            // Verify Account whether exist...
            switch (FindAccountResult.Item2)
            {
                case EFindAccountWay.Id:
                    {
                        //ID is exist
                        RegisterInfo.State = ERegisterAccountState.ExistAccount;
                        RegisterInfo.Message = "ID已存在";
                        break;
                    }
                case EFindAccountWay.UserName:
                    {
                        //username is exist.
                        RegisterInfo.State = ERegisterAccountState.ExistAccount;
                        RegisterInfo.Message = "用户名已被注册";
                        break;
                    }
                case EFindAccountWay.Passport:
                    {
                        //passport is exist
                        RegisterInfo.State = ERegisterAccountState.ExistAccount;
                        RegisterInfo.Message = "通行证已被注册";
                        break;
                    }
                case EFindAccountWay.EMail:
                    {
                        //email is exist
                        RegisterInfo.State = ERegisterAccountState.ExistAccount;
                        RegisterInfo.Message = "邮箱已被注册";
                        break;
                    }
                case EFindAccountWay.Phone:
                    {
                        //phone is exist
                        RegisterInfo.State = ERegisterAccountState.ExistAccount;
                        RegisterInfo.Message = "手机号已被注册";
                        break;
                    }
                case EFindAccountWay.NotFound:
                default:
                    {
                        RegisterInfo.State = ERegisterAccountState.Success;
                        RegisterInfo.Message = "Success";
                        break;
                    }
            }

            return (FindAccountResult.Item2 == null);
        }
        #endregion

    }
}
