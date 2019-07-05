using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Areas.TestArea.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Area("TestArea")]
    public class HomeController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

    }
}