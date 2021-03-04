using BaseDLL.DTO;
using BusinessAdminDLL.Asset;
using BusinessAdminDLL.DTOModel.API.Asset;
using DBAccessCoreDLL.Accesser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NetApplictionServiceDLL;
using System;
using System.Threading.Tasks;

namespace WebAdminService.Controllers.Asset
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> GetAppInfos([FromBody] DTO_PageableQueryModel<DTOAPIReq_GetAppInfos> info) 
        {
            try
            {
                var data = await this.services.getAppInfos(info);
                return JsonToCamelCase(data);
            }
            catch (Exception ex) 
            {
                return JsonToCamelCase(ex.Message, 50000, 50000, ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAppInfo(long id)
        {
            try
            {
                await this.services.RemoveAppInfo(id);

                return JsonToCamelCase(new { success = true });
            }
            catch (Exception ex)
            {
                return JsonToCamelCase(ex.Message, 50000, 50000, ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appinfo"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddAppInfo([FromBody] DTOAPI_AppInfo appinfo)
        {
            try
            {
                long newId = await this.services.AddAppInfo(appinfo);

                return JsonToCamelCase(newId);
            }
            catch (Exception ex)
            {
                return JsonToCamelCase(ex.Message, 50000, 50000, ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appinfo"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateAppInfo([FromBody] DTOAPI_AppInfo appinfo) 
        {
            try
            {
                await this.services.UpdateAppInfo(appinfo);

                return JsonToCamelCase(new { success = true });
            }
            catch (Exception ex)
            {
                return JsonToCamelCase(ex.Message, 50000, 50000, ex.Message);
            }
        }
    }
}
