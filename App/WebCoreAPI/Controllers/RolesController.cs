using BusinessCoreDLL.DTOModel.API.Roles;
using DBAccessCoreDLL.EF.Context;
using DBAccessCoreDLL.EF.Entity;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NetApplictionServiceDLL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebCoreService.Controllers
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
            using (ExamContext db = new ExamContext()) 
            {
                IList<Role> role = (from x in db.Roles select x).DefaultIfEmpty().ToList();

                return Ok(role);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetRole(Int64 id)
        {
            using (ExamContext db = new ExamContext())
            {
                Role role = db.Roles.Find(id);

                return Ok(role);
            }
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

            int effectRowNum = 0; 
            using (ExamContext db = new ExamContext())
            {
                try
                {
                    db.Roles.Add(role);
                    effectRowNum = db.SaveChanges();
                }
                catch (Exception)
                {
                    NotFound("数据库错误");
                }
            }

            return Ok(effectRowNum);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult UpdateRole(Int64 id, [FromBody]DTOAPIReq_Role data)
        {
            if (data == null) 
            {
                return NotFound("DTOAPIReq_Role is null");
            }

            int effectRowNum = 0;
            
            using (ExamContext db = new ExamContext()) 
            {
                try
                {
                    Role role = db.Roles.Find(id);

                    if (role == null)
                    {
                        return NotFound("role is invailed");
                    }

                    role.Name = data.Name;
                    role.Descrption = data.Descrption;

                    effectRowNum = db.SaveChanges();
                }
                catch (Exception ex) 
                {
                    return NotFound(ex.Message);
                }
            }

            return Ok(effectRowNum);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteRole( Int64 id ) 
        {
            int effectRowNum = 0;
            using (ExamContext db = new ExamContext()) 
            {
                try
                {
                    Role role = db.Roles.Find(id);
                    effectRowNum = db.SaveChanges();
                }
                catch (Exception ex) 
                {
                    return NotFound(ex.Message);
                }
            }

            return Ok(effectRowNum);
        }

    }
}
 
