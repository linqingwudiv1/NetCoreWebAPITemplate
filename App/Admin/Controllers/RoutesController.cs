﻿using BusinessAdminDLL.DTOModel.API.Routes;
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
    /// SPA Admin Route
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
            try
            {
                var data = await services.GetRoutePages();
                return JsonToCamelCase(data);
            }
            catch (Exception ex) 
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetRoutePage(Int64 Id)
        {
            try
            {
                var data = await this.services.GetRoutePage(Id);
                return JsonToCamelCase(data);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        [HttpPost("GetRoutePageByRoles")]
        public async Task<IActionResult> GetRoutePageByRoles([FromBody] IList<Int64> Ids)
        {
            try
            {
                var data = await this.services.GetRoutePageTreeByRoles(Ids);
                return JsonToCamelCase(data);
            }
            catch (Exception ex) 
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// 添加PageRoute
        /// </summary>
        /// <param name="routepage"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddRoutePage([FromBody]DTOAPIRes_RoutePage routepage) 
        {
            try
            {
                dynamic data = await this.services.AddRoutePage(routepage.route);
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
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateRoutePage( [FromBody]DTOAPIRes_RoutePage routepage )
        {
            try
            {
                dynamic data = await this.services.UpdateRoutePage(routepage.route);
                return JsonEx(data);
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
        public async Task<IActionResult> DeleteRoutePage(long id)
        {
            try
            {
                var data  = await this.services.DeleteRoutePage(id);
                return JsonEx(data);
            }
            catch(Exception ex)
            {
                return JsonToCamelCase(ex.Message, 50000, 50000, ex.Message);
            }
        }
    }
}
