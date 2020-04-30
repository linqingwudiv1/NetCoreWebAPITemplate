using BaseDLL.Helper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using NetApplictionServiceDLL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace WebCoreService.Areas.AppAPI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Area("AppAPI")]
    [Route("AppAPI/api/[controller]/[action]")]
    [EnableCors("WebAPIPolicy")]
    [ApiController]
    public class CoreController : BaseController
    {
        private readonly IWebHostEnvironment env;


        class DTO_AppVersion
        {
            public string Version { get; set; }
            public string Msg { get; set; }
            public bool bForceUpdate { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_env"></param>
        public CoreController(IWebHostEnvironment _env)
        {
            this.env = _env;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AppVersion()
        {
            string path = Path.GetFullPath(Path.Combine(env.WebRootPath, "public/appVersion.json"));
            dynamic jsonobj = JsonHelper.loadJsonFromFile<DTO_AppVersion>(path);

            jsonobj = jsonobj != null ? jsonobj : new
            {
                Version = "0.0.1",
                Msg = "中文测试test",
                bForceUpdate = false
            };

            return Json(jsonobj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetNeedDownloadList(string Version)
        {
            Regex rx = new Regex(@".zip$");
            
            string directory = Path.GetFullPath(Path.Combine(env.WebRootPath, $"public/AppPack/{Version}"));

            string[] paths = Array.Empty<string>();

            if (Directory.Exists(directory))
            {
                paths = Directory.GetFiles(directory, "*", SearchOption.AllDirectories);
            }

            IList<dynamic> ret_list = new List<dynamic>();

            foreach (string path in paths)
            {
                string filename = Path.GetFileName(path);

                string relativePath = path.Split(directory)[1].Replace(@"\", "/").Remove(0, 1);

                bool ret = rx.IsMatch(filename);

                ret_list.Add(new
                {
                    title = filename,
                    uri = $"public/AppPack/{Version}/{relativePath}",
                    relativePath = relativePath,
                    contentSize  = 0 ,
                    transferSize = 0 ,
                    segment = 0 ,
                    segment_transferSize = 0 ,
                    fileType = ( rx.IsMatch(filename) ? 1 : 0) ,
                    state = 0 ,
                    requests = new List<dynamic>()
                });
            }

            return Json(ret_list);
        }
    }
}