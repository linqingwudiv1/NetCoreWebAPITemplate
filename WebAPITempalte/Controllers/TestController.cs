using Bogus;
using DBAccessDLL.EF.Context;
using DBAccessDLL.EF.Entity;
using DBAccessDLL.Static;
using DTOModelDLL.Common;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
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
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Hello World ! net Core MVC.
    /// </summary>
    [Route("api/[controller]/[action]")]
    [EnableCors("WebAPIPolicy")]
    [ApiController]
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
        [HttpGet]
        public dynamic NetCore_DBConn()
        {
            return Ok(Opt_Conn);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult NetCore_SqliteInsertTest()
        {
            string sqliteDBConn = ConfigurationManager.ConnectionStrings["sqliteTestDB"].ConnectionString;
            ExamContext db = new ExamContext(sqliteDBConn);

            if (!db.Database.CanConnect())
            {
                db.Database.EnsureCreated();
            }

            #region Faker 数据模拟

            const string charSet = @"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ123456789~!@#$%^&*()_+-=`[]{};':"",./<>?";

            var testAccount = new Faker<Account>(locale: "zh_CN").StrictMode(true);

            testAccount.RuleFor( entity => entity.Id,            faker => faker.IndexFaker + 1                            );
            testAccount.RuleFor( entity => entity.Name,          faker => faker.Random.String2(8, 16, charSet)            );
            testAccount.RuleFor( entity => entity.Introduction,  faker => faker.Rant.Review()                             );
            testAccount.RuleFor( entity => entity.Avatar,        faker => faker.Image.PlaceholderUrl(256, 256)            );
            testAccount.RuleFor( entity => entity.Email,         faker => faker.Phone.PhoneNumber() + "@Qing.com"         );
            testAccount.RuleFor( entity => entity.Password,      faker => faker.Random.String2( 8, 16, charSet)           );
            testAccount.RuleFor( entity => entity.Username,      faker => faker.Name.FirstName() + faker.Name.LastName()  );
            testAccount.RuleFor( entity => entity.Phone,         faker => faker.Phone.PhoneNumber()                       );
            testAccount.RuleFor( entity => entity.Sex,           faker => faker.Random.Int(0, 2)                          );

            testAccount.RuleFor( entity => entity.Qing_CreateTime,  faker => DateTime.Now );
            testAccount.RuleFor( entity => entity.Qing_UpdateTime,  faker => DateTime.Now );
            testAccount.RuleFor( entity => entity.Qing_DeleteTime,  faker => null         );
            
            testAccount.RuleFor( entity => entity.Qing_Version  ,  faker => 0  );
            testAccount.RuleFor( entity => entity.Qing_IsDelete ,  faker => faker.Random.Bool() );
            testAccount.RuleFor( entity => entity.Qing_Sequence ,  faker => 0 );
            testAccount.RuleFor( entity => entity.AccountRoles , faker => 
            {
                var bAddRole = faker.Random.Bool();
                if (bAddRole)
                {
                    var accountRoleList = faker.Make<AccountRole>(1, ()=> 
                    {
                        return new AccountRole
                        {
                            Id = Math.Abs(Guid.NewGuid().GetHashCode()),
                            AccountId = faker.IndexFaker + 1,
                            account = null,
                            Qing_CreateTime = DateTime.Now,
                            Qing_DeleteTime = DateTime.Now,
                            Qing_UpdateTime = DateTime.Now,
                            Qing_IsDelete = false,
                            Qing_Sequence = 0,
                            Qing_Version = 0,
                            RoleId = 1
                        };
                    }).ToList();

                    return accountRoleList;
                }
                else 
                {
                    return null;
                }
            });

            var testRoutePage = new Faker<RoutePage>(locale: "zh_CN").StrictMode(true);

            testRoutePage.RuleFor( e => e.Id ,       faker => (faker.IndexFaker + 1) );
            testRoutePage.RuleFor( e => e.Component, faker => "admin" );
            testRoutePage.RuleFor( e => e.Meta,      faker => faker.Make<RoutePageMeta>(1, (i) => new RoutePageMeta())[0] );
            testRoutePage.RuleFor( e => e.Name,      faker => faker.Name.FirstName() + faker.Name.LastName() );
            testRoutePage.RuleFor( e => e.Path,      faker => faker.Rant.Review());

            testRoutePage.RuleFor( e => e.ParentId, faker => 1);

            testRoutePage.RuleFor(entity => entity.Qing_CreateTime, faker => DateTime.Now);
            testRoutePage.RuleFor(entity => entity.Qing_UpdateTime, faker => DateTime.Now);
            testRoutePage.RuleFor(entity => entity.Qing_DeleteTime, faker => null);

            testRoutePage.RuleFor(entity => entity.Qing_Version, faker => 0);
            testRoutePage.RuleFor(entity => entity.Qing_IsDelete, faker => faker.Random.Bool());
            testRoutePage.RuleFor(entity => entity.Qing_Sequence, faker => 0);

            #endregion

            Account[] accountList = testAccount.Generate(1000).ToArray();
            //RoutePage[] RoutePages = testRoutePage.Generate(100).ToArray();

            db.Accounts.AddRange(accountList);
            //db.RoutePages.AddRange(RoutePages);
            
            return Ok(db.SaveChanges());
        }

        /// <summary>
        /// Note：
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult NetCore_SqliteQueryTest()
        {
            string sqliteDBConn = ConfigurationManager.ConnectionStrings["sqliteTestDB"].ConnectionString;

            ExamContext db = new ExamContext(sqliteDBConn);

            if (!db.Database.CanConnect())
            {
                db.Database.EnsureCreated();
            }

            List<Account> list = (from x in db.Accounts select x).ToList();

            db.RoutePages.Include(p => p.ParentId);

            List<View_AccountFemale> list_2 = ( from x in db.view_AccountFemales select x ).ToList();
            return Ok(new { list,list_2});
        }

        /// <summary>
        /// 导出 Excel 示例。标准
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public FileResult Export()
        {
            string newFile = @".Cache/ExportExcel/newbook.core.xlsx";

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

                ISheet sheet2 = workbook.CreateSheet("Sheet2");
                ICellStyle style1 = workbook.CreateCellStyle();
                style1.FillForegroundColor = HSSFColor.Blue.Index2;
                style1.FillPattern = FillPattern.SolidForeground;

                ICellStyle style2 = workbook.CreateCellStyle();
                style2.FillForegroundColor = HSSFColor.Yellow.Index2;
                style2.FillPattern = FillPattern.SolidForeground;

                ICell cell2 = sheet2.CreateRow(0).CreateCell(0);
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
        [HttpPost]
        public IActionResult Import()
        {
            IFormFileCollection files = HttpContext.Request.Form.Files;
            string ret_str = "";

            if ( files.Count > 0 )
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
        [HttpPost]
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
        [HttpPost]
        public IActionResult UploadImage()
        {
            int ret_count = 0;
            IList<string> list = new List<string>();
            IFormFileCollection files = HttpContext.Request.Form.Files;

            if (files.Count > 0)
            {
                foreach (var fileitem in files)
                {
                    string filePath = @".Cache/Image/" + fileitem.FileName;

                    using (FileStream fs = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write))
                    {
                        fileitem.CopyTo(fs);
                        list.Add(Request.HttpContext.Connection.RemoteIpAddress.ToString() + "/" + filePath);
                        ret_count++;
                    }
                }
            }

            dynamic ret_model = new { list, effectCount = ret_count };
            DTO_ReturnModel<dynamic> ret = new DTO_ReturnModel<dynamic>(ret_model);


            return Ok(ret);
        }

        /// <summary>
        /// call upload on UE4 Client example 
        /// </summary>
        /// <param name="size"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult UploadImageFromCS(int size = 0, string filename = "")
        {
            Console.WriteLine($" file {size}");

            int index = 0;
            if ( this.Request.Body != null && 
                 size > 0 )
            {
                Stream stream = this.Request.Body;
                byte[] buffer = new byte[size];

                while ( true )
                {
                    if ( this.Request.Body.CanRead )
                    {
                        int offset = stream.Read(buffer, index, size - index);
                        index = index + offset - 1;

                        if ( offset == 0 )
                        {
                            break;
                        }
                    }
                    else
                    {
                        //Thread.Sleep(100);
                    }
                }

                using (FileStream fs = new FileStream($@"d:/Cache/{filename}", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    fs.Write(buffer, 0, size);
                }
            }

            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
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
                    Local = localIpAddress.MapToIPv4().ToString() + ":" + localPort     ,
                    Remote = RemoteIpAddress.MapToIPv4().ToString() + ":" + RemotePort  ,
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ScriptCase() 
        {
            return Ok();
        }

    }
}
