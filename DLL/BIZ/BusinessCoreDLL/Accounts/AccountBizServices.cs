using BaseDLL.Helper.Smtp;
using BusinessCoreDLL.Base;
using BusinessCoreDLL.DTOModel.API.Users;
using DBAccessBaseDLL.IDGenerator;
using DBAccessCoreDLL.Accesser;
using DBAccessCoreDLL.EF.Context;
using DBAccessCoreDLL.EF.Entity;

namespace BusinessCoreDLL.Accounts
{
    /// <summary>
    /// 
    /// </summary>
    class AccountBizServices : BaseBizServices, IAccountsBizServices
    {
        /// <summary>
        /// 
        /// </summary>
        protected CoreContextDIP db { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected  CoreContextDIP _db;

        protected IAccountAccesser accesser { get;  set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="IDGenerator"></param>
        /// <param name="AccountAccesser"></param>
        public AccountBizServices(CoreContextDIP db, IIDGenerator IDGenerator, IAccountAccesser AccountAccesser) 
            : base() 
        {
            this._db = db;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Register(DTOAPI_Register model) 
        {

            Account existAccount = accesser.Get(passport:model.Passport ,username: model.Username,email: model.EMail, phone: model.Phone);
            
            if ( existAccount != null ) 
            {
                //
                return "用户已存在";
            }


            return "";
            //db.Accounts.Add();
        }

        #region private

        /// <summary>
        /// 用户验证
        /// </summary>
        /// <param name="model"></param>
        private bool RegisterAccountVerify(DTOAPI_Register model)
        {
            bool bVaildEmail =  EmailHepler.IsValid(model.EMail);
            //bool bVaildPhone = 

            return false;
        }


        #endregion

    }
}
