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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_coreHelper"></param>
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