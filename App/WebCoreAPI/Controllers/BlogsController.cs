using BusinessCoreDLL.Blogs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NetApplictionServiceDLL;
using System;
using System.Threading.Tasks;

namespace WebCoreService.Controllers
{
    /// <summary>
    /// blogs 接口
    /// </summary>
    [Route("api/[controller]")]
    [EnableCors("WebAPIPolicy")]
    [ApiController]
    public class BlogsController : BaseController
    {
        /// <summary>
        /// 用户接口
        /// </summary>
        private readonly IBlogsBizServices services;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="_Services"></param>
        /// <param name="_routeServices"></param>
        public BlogsController(IBlogsBizServices _Services)
        {
            services = _Services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            try
            {
                var data = await this.services.GetBlogInfo(id);
                return JsonToCamelCase(data);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message, 50000, 50000, ex.Message);
            }
        }

    }
}
