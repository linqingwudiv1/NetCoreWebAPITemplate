using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseDLL.Helper.Asset;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetApplictionServiceDLL;

namespace WebAdminService.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class HomeController : BaseController
    {

        IAssetHelper assetHelper;
        public HomeController(IAssetHelper _assetHelper) 
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
    }
}