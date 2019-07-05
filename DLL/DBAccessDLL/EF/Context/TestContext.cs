using Microsoft.EntityFrameworkCore;

namespace DBAccessDLL.EF.Context
{
    /// <summary>
    /// Test数据库
    /// </summary>
    public class TestContext : DbContext
    {
        /// <summary>
        /// TestContext
        /// </summary>
        /// <param name="options"></param>
        public TestContext(DbContextOptions<TestContext> options) 
            : base(options)
        {
        }
    }
}
