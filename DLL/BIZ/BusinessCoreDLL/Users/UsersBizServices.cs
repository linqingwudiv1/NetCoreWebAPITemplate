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
        public CoreContext db 
        {
            get { return _db; } 
            protected set { this._db = value; } 
        }

        CoreContext _db;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_db"></param>
        public UsersBizServices() : base() 
        {
        }
    }
}
