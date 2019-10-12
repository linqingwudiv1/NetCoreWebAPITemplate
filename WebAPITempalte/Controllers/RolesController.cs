using DBAccessDLL.EF.Context;
using DBAccessDLL.EF.Entity;
using DTOModelDLL.Common;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NetApplictionServiceDLL;
using System;
using System.Linq;
using System.Collections.Generic;
using DTOModelDLL.API.Roles;

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
            return Ok(ret_model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddRole([FromBody]DTOAPIReq_Role data)
        {
            if (data == null)
            {
                return NotFound("没有正确添加数据");
            }

            Role role = new Role();


            role.Id = Math.Abs(Guid.NewGuid().GetHashCode());
            role.Name = data.Name;
            role.Descrption = data.Descrption;

            DTO_ReturnModel<int> ret_model = new DTO_ReturnModel<int>();
            using (ExamContext db = new ExamContext())
            {
                try
                {
                    db.Roles.Add(role);
                    ret_model.data = db.SaveChanges();
                }
                catch (Exception ex) 
                {
                    ret_model.desc = ex.Message;
                    ret_model.data = -1;
                }
            }

            return Ok(ret_model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult UpdateRole(Int64 id, [FromBody]DTOAPIReq_Role data)
        {
            DTO_ReturnModel<int> ret_model = new DTO_ReturnModel<int>();
            
            using (ExamContext db = new ExamContext()) 
            {
                try
                {
                    Role role = db.Roles.Find(id);

                    if (role == null)
                    {
                        return NotFound(ret_model);
                    }

                    role.Name = data.Name;
                    role.Descrption = data.Descrption;
                    
                    ret_model.data = db.SaveChanges();
                }
                catch (Exception ex) 
                {
                    ret_model.data = -1;
                    ret_model.desc = ex.Message;
                    return NotFound(ret_model);
                }
            }

            return Ok(ret_model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteRole(Int64 id) 
        {
            DTO_ReturnModel<int> ret_model = new DTO_ReturnModel<int>();
            using (ExamContext db = new ExamContext()) 
            {
                try
                {
                    Role role = db.Roles.Find(id);


                    db.SaveChanges();
                }
                catch (Exception ex) 
                {
                    ret_model.data = -1;
                    ret_model.desc = ex.Message;
                }
            }
        return Ok();
        }
    }
}
 
