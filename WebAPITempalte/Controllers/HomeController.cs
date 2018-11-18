﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseDLL.Helper;
using Microsoft.AspNetCore.Mvc;

namespace WebAPITempalte.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ICoreHelper coreHelper;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_coreHelper"></param>
        public HomeController(ICoreHelper _coreHelper)
        {
            coreHelper = _coreHelper;
            //var t = _coreHelper.HelloAotufac();
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Autofac()
        {
            return Content($"<h3 style=\"color: blue; \">{coreHelper.HelloAotufac()}</h3>","text/html");
        }
    }
}