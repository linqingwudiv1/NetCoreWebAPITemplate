using Microsoft.EntityFrameworkCore;

namespace WebAPI.Model.EF.Context
{
    /// <summary>
    /// Test数据库
    /// </summary>
    public class TestContext : DbContext
    {
        /// <summary>
        /// LTE
        /// </summary>
        /// <param name="options"></param>
        public TestContext(DbContextOptions<TestContext> options) 
            : base(options)
        {
        }
    }
}
