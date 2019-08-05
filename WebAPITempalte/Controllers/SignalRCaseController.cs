using DTOModelDLL.Common;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using NetApplictionServiceDLL;
using System;
using System.Collections.Generic;
using System.IO;
using WebApp.SingalR;

namespace WebAPI.Controllers
{
    /// <summary>
    /// SignalRCase
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
        /// 图片上传
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult UploadImage(string ChildUE4 = "")
        {
            try
            {
                int ret_count = 0;
                IList<string> list = new List<string>();
                IFormFileCollection files = HttpContext.Request.Form.Files;

                if (files.Count > 0)
                {
                    foreach (IFormFile fileitem in files)
                    {
                        string filePath = @".Cache/Image/" + fileitem.FileName;

                        using (FileStream fs = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write))
                        {
                            fileitem.CopyTo(fs);

                            list.Add(@"Cache/Image/" +DateTime.Now.ToString("ddHHmmssff-")  + fileitem.FileName);
                            ret_count++;
                        }
                    }
                }

                IClientProxy TargetChild = _hubContext.Clients.User(ChildUE4);

                if (TargetChild != null)
                {
                    TargetChild.SendAsync(CommonHub.Event_ReceiveUploadImageComplated, list).Wait();
                }

                dynamic ret_model = new { list, effectCount = ret_count };
                DTO_ReturnModel<dynamic> ret = new DTO_ReturnModel<dynamic>(ret_model);

                return Ok(ret);
            }
            catch (Exception ex)
            {
                return NotFound((new DTO_ReturnModel<dynamic>(ex.Message)));
            }
        }
    }
}
