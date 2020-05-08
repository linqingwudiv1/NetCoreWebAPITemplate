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
        protected IAccountAccesser accesser { get;  set; }

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
        public string Register(DTOAPI_Register model) 
        {

            RegisterAccountVerify(model);

            Tuple<Account, EFindAccountWay> FindAccountResult = accesser.Get( passport: model.Passport  , 
                                                                              username: model.Username  , 
                                                                                 email: model.EMail     , 
                                                                                 phone: model.Phone         );

            switch (FindAccountResult.Item2)
            {
                case EFindAccountWay.Id:
                    {
                        //用户ID已存在
                        break;
                    }
                case EFindAccountWay.UserName:
                    {
                        //用户昵称已存在
                        break;
                    }
                case EFindAccountWay.Passport:
                    {
                        //用户名已存在
                        break;
                    }
                case EFindAccountWay.EMail:
                    {
                        //EMail已存在
                        break;
                    }
                case EFindAccountWay.Phone:
                    {
                        //手机已存在
                        break;
                    }
                case EFindAccountWay.NotFound: 
                    {
                        //Register Logic
                        break;
                    }
            }

            return "";
        }

        #region private

        /// <summary>
        /// 用户验证
        /// </summary>
        /// <param name="model"></param>
        private bool RegisterAccountVerify(DTOAPI_Register model)
        {
            bool bIsVaildEMail = true;
            bool bIsVaildPassword = true;
            bool bIsVaildPhone = true;
            bool bIsVaildPassport = true;
            bool bIsVaildUserName = true;

            if (!string.IsNullOrWhiteSpace(model.EMail) && !EmailHepler.IsValid(model.EMail))
            {
                // "无效的邮箱";
            }

            if (!string.IsNullOrWhiteSpace(model.Password) && !PasswordHelper.IsValid(model.Password))
            {
                //"无效的密码格式";
            }

            if (!string.IsNullOrWhiteSpace(model.Phone) && !PhoneHelper.IsValid(model.Phone))
            {
                //"无效的手机号码";
            }

            if (!string.IsNullOrWhiteSpace(model.Username) && !PhoneHelper.IsValid(model.Username))
            {
                //"无效的用户昵称";
            }

            if (!string.IsNullOrWhiteSpace(model.Passport) && !PhoneHelper.IsValid(model.Passport))
            {
                //"无效的用户名";
            }


            return false;
        }


        #endregion

    }
}
