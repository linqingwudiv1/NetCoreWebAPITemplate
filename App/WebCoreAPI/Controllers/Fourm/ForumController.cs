using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessCoreDLL.Forum;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetApplictionServiceDLL;

namespace WebCoreService.Controllers.Fourm
{

    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [EnableCors("WebAPIPolicy")]
    [ApiController]
    public class ForumController : BaseController
    {

        /// <summary>
        /// 用户接口
        /// </summary>
        private readonly IForumBizServices services;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="_Services"></param>
        public ForumController(IForumBizServices _Services)
        {
            services = _Services;
        }


        /**
         * 
         **/
        [HttpGet("[action]")]
        public async Task<IActionResult>  GetForumPortalData() 
        {
            try
            {
                var data = await this.services.GetForumPortalData().ConfigureAwait(false);
                return JsonToCamelCase(data);
            }
            catch (Exception ex) 
            {
                return NotFound(ex.Message, 50000, 50000, ex.Message);
            }
        }

    }
}