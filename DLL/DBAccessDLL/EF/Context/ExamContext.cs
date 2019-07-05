using DBAccessDLL.EF.Context.Base;
using Microsoft.EntityFrameworkCore;

namespace DBAccessDLL.EF.Context
{
    /// <summary>
    /// Test数据库
    /// </summary>
    public class ExamContext : BaseContext<ExamContext>
    {
        /// <summary>
        /// TestContext
        /// </summary>
        /// <param name="options"></param>
        public ExamContext(DbContextOptions<ExamContext> options, string _ConnString = "") 
            : base(options, _ConnString)
        {

        }
    }
}
