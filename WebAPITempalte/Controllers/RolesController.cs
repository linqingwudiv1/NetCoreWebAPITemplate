using DBAccessDLL.EF.Context;
using DBAccessDLL.EF.Entity;
using DTOModelDLL.Common;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NetApplictionServiceDLL;
using System;
using System.Linq;
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
        public IActionResult GetRoles()
        {
            ExamContext db = new ExamContext();

            IList<Role> role = (from x in db.Roles select x).DefaultIfEmpty().ToList();

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
        public IActionResult GetRole(Int64 id)
        {
            ExamContext db = new ExamContext();

            Role role = db.Roles.Find(id);

            DTO_ReturnModel<Role> ret_model = new DTO_ReturnModel<Role>(role);
            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddRole([FromBody]Role data)
        {
            if (data != null) 
            {
                data.Id = Math.Abs(Guid.NewGuid().GetHashCode());   
            }

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
 
