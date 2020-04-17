using BusinessCoreDLL.Base;
using DBAccessCoreDLL.EF.Context;

namespace BusinessCoreDLL.Users
{
    /// <summary>
    /// 
    /// </summary>
    class UsersBizServices : BaseBizServices, IUsersBizServices
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
        public UsersBizServices(CoreContextDIP db) : base() 
        {
            this._db = db;
        }

    }
}
