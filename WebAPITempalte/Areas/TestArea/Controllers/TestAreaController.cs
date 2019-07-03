using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Areas.TestArea.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class TestAreaController : Controller
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