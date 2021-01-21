using BusinessAdminDLL.DTOModel.API.Roles;
using BusinessAdminDLL.Roles;
using DBAccessCoreDLL.EFORM.Context;
using DBAccessCoreDLL.EFORM.Entity;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NetApplictionServiceDLL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdminService.Controllers
{
    /// <summary>
    /// Vue项目展示接口
    /// </summary>
    [Route("api/[controller]")]
    [EnableCors("WebAPIPolicy")]
    [ApiController]
    public class RolesController : BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        readonly IRolesBizServices services;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_services"></param>
        public RolesController(IRolesBizServices _services)
        {
            services = _services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetRoles()
        {
            var roles = services.GetRoles();
            return Ok(roles);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetRole(Int64 id)
        {
            var role = services.GetRole(id);
            return Ok(role);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddRole( [FromBody]DTOAPI_Role data )
        {
            if (data == null)
            {
                return NotFound("没有正确添加数据");
            }

            int effectRowNum = services.AddRole(data);
            return Ok(effectRowNum);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult UpdateRole(Int64 id, [FromBody]DTOAPI_Role data)
        {
            if (data == null)
            {
                return NotFound("DTOAPIReq_Role is null");
            }

            int effectRowNum = 0;
            services.UpdateRole(data);

            return Ok(effectRowNum);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteRole(Int64 id)
        {
            int effectRowNum = 0;
            effectRowNum = services.DeleteRole(id);

            return Ok(effectRowNum);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult DeleteRole([FromBody]long[] ids)
        {
            int effectRowNum = 0;
            effectRowNum = this.services.DeleteRoles(ids);
            return Ok(effectRowNum);
        }

    }
}
 
