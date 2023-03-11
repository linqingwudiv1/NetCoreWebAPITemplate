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
    public class ForumTopicController : BaseController
    {
        /// <summary>
        /// 用户接口
        /// </summary>
        private readonly IForumTopicBizServices services;


        /// <summary>
        /// 
        /// </summary>
        public ForumTopicController(IForumTopicBizServices _Services)
        {
            this.services = _Services;
        }
    }
}
