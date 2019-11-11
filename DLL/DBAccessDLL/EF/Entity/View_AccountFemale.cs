using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DBAccessDLL.EF.Entity
{
    /// <summary>
    /// EF 栗子 女性用户,View Case.
    /// </summary>
    public class View_AccountFemale : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string username { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string password { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string avatar { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string introduction { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int sex { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string phone { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class View_AccountFemaleEFConfig : IEntityTypeConfiguration<View_AccountFemale>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<View_AccountFemale> builder)
        {
            #region //水平拆分处理处
            builder.ToView<View_AccountFemale>("View_AccountFemale").SetupBaseEntity();
            #endregion
        }
    }

}
