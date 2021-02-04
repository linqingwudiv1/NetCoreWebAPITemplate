using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetApplictionServiceDLL;

namespace WebAdminService.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [EnableCors("WebAPIPolicy")]
    [ApiController]
    public class PageviewsController : BaseController
    {

        /// <summary>
        /// 
        /// </summary>
        public PageviewsController() 
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get() 
        {
            var arr = new dynamic[]
            {
                      new { key = "PC",      pageviews = 1024 },
                      new { key = "Mobile",  pageviews = 1024 },
                      new { key = "iOS",     pageviews = 1024 },
                      new { key = "Android", pageviews = 1024 }
            };

            return JsonEx(arr);                                    
        }
    }
}