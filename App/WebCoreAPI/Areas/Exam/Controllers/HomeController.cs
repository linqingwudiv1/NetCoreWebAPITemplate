﻿using BusinessCoreDLL.Users;
using DBAccessCoreDLL.EF.Context;
using Microsoft.AspNetCore.Mvc;
using NetApplictionServiceDLL;

namespace WebCoreService.Areas.TestArea.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Area("Exam")]
    [Controller]
    public class HomeController : BaseController
    {

        /// <summary>
        /// 
        /// </summary>
        public HomeController( IUsersBizServices serives) 
        {
        }

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