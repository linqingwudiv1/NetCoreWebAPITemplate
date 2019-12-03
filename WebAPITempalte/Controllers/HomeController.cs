using BaseDLL.Helper;
using DBAccessDLL.EF.Context;
using Microsoft.AspNetCore.Mvc;
using NetApplictionServiceDLL;

namespace WebAPI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Controller]
    public class HomeController : BaseController
    {
        private readonly ICoreHelper coreHelper;
        private readonly ExamContextDIPool db;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_coreHelper"></param>
        public HomeController(ICoreHelper _coreHelper)
        {
            //coreHelper = _coreHelper;
            //db = _db;
            coreHelper = _coreHelper;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Index([FromServices]ICoreHelper _coreHelper)
        {
            var assembly = typeof(Controller).Assembly;
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Autofac([FromServices]ExamContextDIPool db, [FromServices]ICoreHelper _coreHelper)
        {
            return Content(@$"<h3 style=""color: blue; "">{coreHelper.HelloAutofac()}</h3><h3 style=""color: red; "">{_coreHelper.HelloAutofac()}</h3>","text/html");
        }
    }
}