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

}
