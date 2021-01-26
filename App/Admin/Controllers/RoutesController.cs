using BusinessAdminDLL.DTOModel.API.Routes;
using BusinessAdminDLL.RoutePage;
using DBAccessCoreDLL.EFORM.Context;
using DBAccessCoreDLL.EFORM.Entity;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NetApplictionServiceDLL;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace WebAdminService.Controllers
{

    /// <summary>
    /// Vue项目展示接口
    /// </summary>
    [Route("api/[controller]")]
    [EnableCors("WebAPIPolicy")]
    [ApiController]
    public class RoutesController : BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        readonly IRoutePageBizServices services;

        /// <summary>
        /// 
        /// </summary>
        public RoutesController(IRoutePageBizServices _services) 
        {
            services = _services;
        }

        /// <summary>
        /// 
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetRoutePages()
        {
            var data = services.GetRoutePages();
            return JsonToCamelCase(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetRoutePage(Int64 Id)
        {
            var data = this.services.GetRoutePage(Id);
            return JsonToCamelCase(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="routepage"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddRoutePage([FromBody]DTOAPI_RoutePages routepage)
        {
            int effectNum = 0;
            effectNum = await this.services.AddRoutePage(routepage);

            return Ok(effectNum);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateRoutePage( [FromBody]DTOAPI_RoutePages routepage )
        {
            int effectNum = 0;
            // 
            effectNum = await this.services.UpdateRoutePage( routepage );
            return Ok(effectNum);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteRoutePage(long id)
        {
            int effectNum = 0;
            effectNum = await this.services.DeleteRoutePage(id);
            return Ok(effectNum);
        }
    }
}
