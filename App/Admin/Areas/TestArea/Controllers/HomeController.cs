﻿using Microsoft.AspNetCore.Mvc;

namespace WebAdminService.Areas.TestArea.Controllers
{
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