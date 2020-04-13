using DBAccessDLL.EF.Context;
using DBAccessDLL.EF.Entity;
using DTOModelDLL.Common;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NetApplictionServiceDLL;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Configuration;
using DTOModelDLL.API.Routes;


namespace WebCoreService.Controllers
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
        [HttpGet]
        public IActionResult GetRoutePages() 
        {
            string sqliteDBConn = ConfigurationManager.ConnectionStrings["sqliteTestDB"].ConnectionString;
            using (ExamContext db = new ExamContext(sqliteDBConn))
            {
                IList<RoutePage> routePages = (from x in db.RoutePages select x).ToList();

                DTO_ReturnModel<IList<RoutePage>> ret_model = new DTO_ReturnModel<IList<RoutePage>>(routePages);
                return Ok(ret_model);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        public IActionResult GetRoutePage(Int64 Id)
        {
            string sqliteDBConn = ConfigurationManager.ConnectionStrings["sqliteTestDB"].ConnectionString;
            
            using ( ExamContext db = new ExamContext(sqliteDBConn) )
            {
                RoutePage routePage = db.RoutePages.Find(Id);

                DTO_ReturnModel<RoutePage> ret_model = new DTO_ReturnModel<RoutePage>(routePage);
                return Ok(ret_model);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddRoutePage(DTOAPIReq_AddRoutePages routepage ) 
        {
            using (ExamContext db = new ExamContext())
            {
                RoutePage newRoutePage = new RoutePage();
                
                newRoutePage.Meta = new RoutePageMeta();

                DTO_ReturnModel<RoutePage> ret_model = new DTO_ReturnModel<RoutePage>(null) ;

                return Ok(ret_model);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public IActionResult UpdateRoutePage(/* DTOAPIReq_UpdateRoles routepage */) 
        {
            DTO_ReturnModel<RoutePage> ret_model = new DTO_ReturnModel<RoutePage>(null);

            return Ok(ret_model);
        }
    }
}
