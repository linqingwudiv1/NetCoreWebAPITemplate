using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCoreDLL.Forum
{
    public interface IForumBizServices
    {
        Task<dynamic>  GetForumPortalData();
    }
}
