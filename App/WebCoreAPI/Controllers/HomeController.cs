using BusinessCoreDLL.Accounts;
using Microsoft.AspNetCore.Mvc;
using NetApplictionServiceDLL;
using System.Reflection;

namespace WebCoreService.Controllers
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
        public HomeController(IAccountsBizServices usersServices)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            Assembly assembly = typeof(Controller).Assembly;

            return View();
        }
    }
}