using BusinessCoreDLL.Base;
using DBAccessCoreDLL.EF.Context;

namespace BusinessCoreDLL.Users
{
    /// <summary>
    /// 
    /// </summary>
    public class UsersBizServices : BaseBizServices, IUsersBizServices
    {
        public readonly ExamContextDIP db;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        public UsersBizServices(ExamContext db) : base() 
        {
        }
    }
}
