using Bogus;
using DBAccessDLL.EF.Context;
using DBAccessDLL.EF.Entity;
using DBAccessDLL.Static;
using DTOModelDLL.Common;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using NetApplictionServiceDLL;
using Npoi.Core.HSSF.Util;
using Npoi.Core.SS.UserModel;
using Npoi.Core.SS.Util;
using Npoi.Core.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WebApp.SingalR;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Hello World ! net Core MVC.
    /// </summary>
    [Route("api/[controller]/[action]")]
    [EnableCors("WebAPIPolicy")]
    public class SignalRCaseController : BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IHubContext<CommonHub> _hubContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hubContext"></param>
        public SignalRCaseController(IHubContext<CommonHub> hubContext)
        {
            _hubContext = hubContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost()]
        public IActionResult UploadImage()
        {
            int ret_count = 0;
            IList<string> list = new List<string>();
            IFormFileCollection files = HttpContext.Request.Form.Files;
            if (files.Count > 0)
            {
                foreach (IFormFile fileitem in files)
                {
                    var filePath = @".Cache/Image/" + fileitem.FileName;

                    using (FileStream fs = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write))
                    {
                        fileitem.CopyTo(fs);
                        list.Add(Request.HttpContext.Connection.RemoteIpAddress.ToString() + "/" + filePath);
                        ret_count++;
                    }
                }
            }

            _hubContext.Clients.All.SendAsync("ReceiveUploadImageComplated", list).Wait();

            var ret_model = new { list, effectCount = ret_count };
            var ret = new DTO_ReturnModel<dynamic>(ret_model);
            return Ok(ret);
        }
    }
}
