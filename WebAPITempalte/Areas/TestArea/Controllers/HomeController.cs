using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Areas.TestArea.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    class Point
    {
        /// <summary>
        /// 
        /// </summary>
        public int X { get; }

        /// <summary>
        /// 
        /// </summary>
        public int Y { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Point(int x, int y)
        {
            (X, Y) = (x, y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Deconstruct(out int x, out int y) => (x, y) = (X, Y);
    }

    /// <summary>
    /// 
    /// </summary>
    [Area("TestArea")]
    [Controller]
    public class HomeController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Index(object o)
        {
            return View();
        }
    }
}