using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetApplictionServiceDLL;

namespace WebCoreService.Areas.Exam.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Area("Exam")]
    [Controller]
    public class JWTCaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}