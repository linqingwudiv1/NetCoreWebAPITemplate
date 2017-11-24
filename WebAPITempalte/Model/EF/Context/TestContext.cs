using Microsoft.EntityFrameworkCore;

namespace Nokia_LTE_WebAPI.Model.EF.Context
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

        //virtual public DbSet<AccountUser> AccountUser { get; set; }
    }
}
