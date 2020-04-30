using BusinessCoreDLL.Base;
using DBAccessBaseDLL.IDGenerator;
using DBAccessCoreDLL.EF.Context;

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
        public CoreContextDIP db 
        {
            get { return _db; } 
            protected set { this._db = value; } 
        }

        CoreContextDIP _db;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="IDGenerator"></param>
        public AccountBizServices(CoreContextDIP db, IIDGenerator IDGenerator) : base() 
        {
            this._db = db;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Register() 
        {
            //db.Accounts.Add()
            //db.Accounts
        }

    }
}
