using BusinessCoreDLL.DTOModel.API.Blogs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCoreDLL.Blogs
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBlogsBizServices
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<DTOAPIRes_BlogInfo> GetBlogInfo(long id);
    }
}
