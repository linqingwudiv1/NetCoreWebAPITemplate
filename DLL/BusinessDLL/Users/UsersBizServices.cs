using BusinessDLL.Base;
using DBAccessDLL.EF.Context;

namespace BusinessDLL.Users
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
