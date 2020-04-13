using Microsoft.AspNetCore.Mvc;
using NetApplictionServiceDLL;

namespace AdminService.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Controller]
    public class HomeController : BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        public HomeController()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            var assembly = typeof(Controller).Assembly;
            return View();
        }

    }
}