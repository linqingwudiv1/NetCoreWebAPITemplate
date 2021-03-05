using BusinessCoreDLL.Asset;
using DBAccessCoreDLL.DTOModel.API.Asset;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NetApplictionServiceDLL;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebCoreService.Controllers.Asset
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/asset/[controller]")]
    [EnableCors("WebAPIPolicy")]
    [ApiController]
    public class AppInfoController : BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        public readonly IAppInfoBizServices services;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_services"></param>
        public AppInfoController( IAppInfoBizServices _services) 
        {
            services = _services;
        }

        [HttpGet("Latest")]
        public async Task<IActionResult>  GetLatest(string appName = "aw") 
        {
            try
            {
                var data = await this.services.GetLatest(appName);
                return JsonToCamelCase(data);
                // this.services.
            }
            catch (Exception ex) 
            {
                return JsonToCamelCase(ex.Message, 50000,50000, ex.Message);
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetApps()
        {
            IList<dynamic> list = new List<dynamic>();
            list.Add(new 
            {
                appName = "Amazing Work",
                identity = "aw",
                cover   = "img/icons/logo.png"
            });
            return JsonToCamelCase(list);
        }
    }
}
