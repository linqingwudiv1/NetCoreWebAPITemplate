﻿using BusinessCoreDLL.Users;
using Microsoft.AspNetCore.Mvc;
using NetApplictionServiceDLL;

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
        public HomeController(IUsersBizServices usersServices)
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