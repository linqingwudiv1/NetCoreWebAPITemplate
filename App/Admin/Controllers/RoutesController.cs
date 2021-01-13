using BusinessAdminDLL.DTOModel.API.Routes;
using DBAccessCoreDLL.EF.Context;
using DBAccessCoreDLL.EF.Entity;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NetApplictionServiceDLL;
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
        /// <summary>
        /// 
        /// </summary>
        [HttpGet]
        public IActionResult GetRoutePages()
        {
            string sqliteDBConn = ConfigurationManager.ConnectionStrings["sqliteTestDB"].ConnectionString;

            using ( CoreContext db = new CoreContext(sqliteDBConn) )
            {
                IList<RoutePage> routePages = (from x in db.RoutePages select x).ToList();

                return Ok(routePages);
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

            using ( CoreContext db = new CoreContext(sqliteDBConn) )
            {
                RoutePage routePage = db.RoutePages.Find(Id);
                return Ok(routePage);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddRoutePage(DTOAPI_RoutePages routepage ) 
        {
            int effectNum = 0;
            using (CoreContext db = new CoreContext())
            {
                RoutePage newRoutePage = new RoutePage();

                newRoutePage.Meta = new RoutePageMeta();
            }
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
