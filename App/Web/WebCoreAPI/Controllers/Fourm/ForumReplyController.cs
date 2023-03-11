using BusinessCoreDLL.Forum;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NetApplictionServiceDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCoreService.Controllers.Fourm
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [EnableCors("WebAPIPolicy")]
    [ApiController]
    public class ForumReplyController : BaseController
    {
        /// <summary>
        /// 用户接口
        /// </summary>
        private readonly IForumReplyBizServices services;


        /// <summary>
        /// 
        /// </summary>
        public ForumReplyController(IForumReplyBizServices _Services)
        {
            this.services = _Services;
        }
    }
}
