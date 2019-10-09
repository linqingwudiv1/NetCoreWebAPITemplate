using DBAccessDLL.EF.Entity;
using DTOModelDLL.Common;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NetApplictionServiceDLL;
using System;
using System.Collections.Generic;

namespace WebAPI.Controllers
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
        /// <returns></returns>
        [HttpGet]
        public IActionResult AddRoles()
        {
            DTO_ReturnModel<IList<Role>> ret_model = new DTO_ReturnModel<IList<Role>>();
            ret_model = new DTO_ReturnModel<IList<Role>>();

            return Ok(ret_model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult AddRoles(Int64 id)
        {
            return Ok(null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddRole([FromBody]Role data)
        {
            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult UpdateRole(Int64 id, [FromBody]Role data)
        {
            return Ok(null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteRole(Int64 id) 
        {
            return Ok(new DTO_ReturnModel<dynamic>(null));
        }
    }
}
