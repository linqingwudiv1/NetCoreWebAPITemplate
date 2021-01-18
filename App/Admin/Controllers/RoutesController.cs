using BusinessAdminDLL.DTOModel.API.Routes;
using BusinessAdminDLL.RoutePage;
using DBAccessCoreDLL.EF.Context;
using DBAccessCoreDLL.EF.Entity;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NetApplictionServiceDLL;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;


namespace AdminService.Controllers
{

    /// <summary>
    /// Vue项目展示接口
    /// </summary>
    [Route("api/[controller]")]
    [EnableCors("WebAPIPolicy")]
    [ApiController]
    public class RoutesController : BaseController
    {
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
        public IActionResult GetRoutePages()
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
        public IActionResult GetRoutePage(Int64 Id)
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
        public IActionResult AddRoutePage([FromBody]DTOAPI_RoutePages routepage)
        {
            int effectNum = 0;
            this.AddRoutePage(routepage);
            return Ok(effectNum);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public IActionResult UpdateRoutePage(/* DTOAPIReq_UpdateRoles routepage */)
        {
            int effectNum = 0;

            return Ok(effectNum);
        }
    }
}
