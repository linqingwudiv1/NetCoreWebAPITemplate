using Bogus;
using DBAccessDLL.EF.Context;
using DBAccessDLL.EF.Entity;
using DBAccessDLL.Static;
using DTOModelDLL.Common;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
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

namespace WebAPI.Controllers
{
    /// <summary>
    /// Hello World ! net Core MVC.
    /// </summary>
    [Route("api/[controller]/[action]")]
    [EnableCors("WebAPIPolicy")]
    public class TestController : BaseController
    {

        /// <summary>
        /// 
        /// </summary>
        private Option_ConnctionString Opt_Conn { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private Opt_API_LTEUrl Opt_API { get; set; }

        /// <summary>
        /// 测试  
        /// </summary>
        /// <param name="Opt"></param>
        /// <param name="_Opt_API"></param>
        public TestController(IOptions<Option_ConnctionString> Opt, IOptions<Opt_API_LTEUrl> _Opt_API)
        {
            Opt_Conn = Opt.Value;
            Opt_API = _Opt_API.Value;
        }

        /// <summary>
        /// 测试方法
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult HelloNetCore(string id = "")
        {
            return Ok("Hello! Net Core 2.0: " + id);
        }

        /// <summary>
        /// 获取数据库连接信息
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public dynamic NetCore_DBConn()
        {
            return Ok(Opt_Conn);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public IActionResult NetCore_SqliteInsertTest()
        {
            string sqliteDBConn = ConfigurationManager.ConnectionStrings["sqliteTestDB"].ConnectionString;
            ExamContext db = new ExamContext(sqliteDBConn);

            if (!db.Database.CanConnect())
            {
                db.Database.EnsureCreated();
            }

            #region faker 数据模拟
            const string charSet = @"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ123456789~!@#$%^&*()_+-=`[]{};':"",./<>?";
            var testAccount = new Faker<Account>(locale: "zh_CN").StrictMode(true);

            testAccount.RuleFor(entity => entity.id,             faker => faker.Random.Guid().GetHashCode());
            testAccount.RuleFor(entity => entity.name,           faker => faker.Random.String2(8, 16, charSet));
            testAccount.RuleFor(entity => entity.introduction,   faker => faker.Rant.Review());
            testAccount.RuleFor(entity => entity.avatar,         faker => faker.Image.PlaceholderUrl(256, 256));
            testAccount.RuleFor(entity => entity.email,          faker => faker.Phone.PhoneNumber() + "@Qing.com");
            testAccount.RuleFor(entity => entity.password,       faker => faker.Random.String2(8, 16, charSet));
            testAccount.RuleFor(entity => entity.username,       faker => faker.Name.FirstName() + faker.Name.LastName());
            testAccount.RuleFor(entity => entity.phone,          faker => faker.Phone.PhoneNumber());
            testAccount.RuleFor(entity => entity.Qing_IsDelete,  faker => faker.Random.Bool());
            testAccount.RuleFor(entity => entity.Qing_Version,   0 /*faker => 0*/);

            #endregion

            var accountList = testAccount.Generate(1000).ToArray();
            db.Accounts.AddRange(accountList);

            return Ok(db.SaveChanges());
        }

        /// <summary>
        /// Note：
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public IActionResult NetCore_SqliteQueryTest()
        {
            string sqliteDBConn = ConfigurationManager.ConnectionStrings["sqliteTestDB"].ConnectionString;
            ExamContext db = new ExamContext(sqliteDBConn);
            if (!db.Database.CanConnect())
            {
                db.Database.EnsureCreated();
            }

            List<Account> list = (from x in db.Accounts select x).ToList();
            return Ok(list);
        }

        /// <summary>
        /// 导出 Excel 示例。标准
        /// </summary>
        /// <returns></returns>
        [HttpGet("Export")]
        public FileResult Export()
        {
            var newFile = @".Cache/ExportExcel/newbook.core.xlsx";

            using (var fs = new FileStream(newFile, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                IWorkbook workbook = new XSSFWorkbook();

                ISheet sheet1 = workbook.CreateSheet("Sheet1");

                sheet1.AddMergedRegion(new CellRangeAddress(0, 0, 0, 10));
                var rowIndex = 0;
                IRow row = sheet1.CreateRow(rowIndex);
                row.Height = 30 * 80;
                row.CreateCell(0).SetCellValue("这是单元格内容，可以设置很长，看能不能自动调整列宽");

                sheet1.AutoSizeColumn(0);
                rowIndex++;


                var sheet2 = workbook.CreateSheet("Sheet2");
                var style1 = workbook.CreateCellStyle();
                style1.FillForegroundColor = HSSFColor.Blue.Index2;
                style1.FillPattern = FillPattern.SolidForeground;

                var style2 = workbook.CreateCellStyle();
                style2.FillForegroundColor = HSSFColor.Yellow.Index2;
                style2.FillPattern = FillPattern.SolidForeground;

                var cell2 = sheet2.CreateRow(0).CreateCell(0);
                cell2.CellStyle = style1;
                cell2.SetCellValue(0);

                cell2 = sheet2.CreateRow(1).CreateCell(1);
                cell2.CellStyle = style2;
                cell2.SetCellValue(1);

                cell2 = sheet2.CreateRow(2).CreateCell(2);
                cell2.CellStyle = style1;
                cell2.SetCellValue(2);

                cell2 = sheet2.CreateRow(3).CreateCell(3);
                cell2.CellStyle = style2;
                cell2.SetCellValue(3);

                cell2 = sheet2.CreateRow(4).CreateCell(4);
                cell2.CellStyle = style1;
                cell2.SetCellValue(4);

                workbook.Write(fs);
            }

            FileContentResult result = new FileContentResult(System.IO.File.ReadAllBytes(newFile), "application/x-xls")
            {
                FileDownloadName = "test.xls"
            };

            return result;

        }

        /// <summary>
        ///  导入 Excel 读取示例
        /// </summary>
        /// <returns></returns>
        [HttpPost()]
        public IActionResult Import()
        {
            IFormFileCollection files = HttpContext.Request.Form.Files;
            string ret_str = "";

            if (files.Count > 0)
            {
                foreach (var fileitem in files)
                {
                    Stream      stream = fileitem.OpenReadStream();
                    IWorkbook   workbook = new XSSFWorkbook(stream);
                    ISheet      def_sheet = workbook.GetSheetAt(0);
                    IRow        row = def_sheet.GetRow(0);
                    string      info = row.Cells[0].StringCellValue;
                    ret_str =   info;
                    stream.Dispose();
                }
            }

            var ret = new DTO_ReturnModel<string>(ret_str);
            return Ok(ret);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost()]
        public async Task<IActionResult> UploadImageMulitple()
        {
            int ret_count = 0;
            IList<string> list = new List<string>();
            IFormFileCollection files = HttpContext.Request.Form.Files;
            if (files.Count > 0)
            {
                foreach (var fileitem in files)
                {
                    string filePath = @".Cache/Image/" + fileitem.FileName;

                    using ( FileStream fs = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write) )
                    {
                        await fileitem.CopyToAsync(fs);
                        list.Add(Request.HttpContext.Connection.RemoteIpAddress.ToString() + "/" + filePath);
                        ret_count++;
                    }
                }
            }

            dynamic ret_model = new { list, effectCount = ret_count };
            dynamic ret = new DTO_ReturnModel<dynamic>(ret_model);
            return Ok(ret);
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
                foreach (var fileitem in files)
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

            var ret_model = new { list, effectCount = ret_count };
            var ret = new DTO_ReturnModel<dynamic>(ret_model);
            return Ok(ret);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public IActionResult ServerIPAddress()
        {
            try
            {
                IHttpConnectionFeature httpConnectionFeature = HttpContext.Features.Get<IHttpConnectionFeature>();

                IPAddress localIpAddress = httpConnectionFeature?.LocalIpAddress;
                int? localPort = httpConnectionFeature?.LocalPort;

                IPAddress RemoteIpAddress = httpConnectionFeature?.LocalIpAddress;
                int? RemotePort = httpConnectionFeature?.LocalPort;

                dynamic ret_model = new
                {
                    Local = localIpAddress.MapToIPv4().ToString() + ":" + localPort,
                    Remote = RemoteIpAddress.MapToIPv4().ToString() + ":" + RemotePort,
                    HostName = Dns.GetHostName()
                };

                DTO_ReturnModel<dynamic> ret = new DTO_ReturnModel<dynamic>(ret_model);
                return Ok(ret);
            }
            catch (Exception ex)
            {
                return NotFound( new DTO_ReturnModel<dynamic>( ex.Message,400 ) );
            }

        }
    }
}
