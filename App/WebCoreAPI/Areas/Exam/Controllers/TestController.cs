using Bogus;
using BusinessCoreDLL.Users;
using DBAccessBaseDLL.Static;
using DBAccessCoreDLL.EF.Context;
using DBAccessCoreDLL.EF.Entity;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NetApplictionServiceDLL;
using NLog;
using Npoi.Core.HSSF.Util;
using Npoi.Core.SS.UserModel;
using Npoi.Core.SS.Util;
using Npoi.Core.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace WebCoreService.Areas.TestArea.Controllers
{
    /// <summary>
    /// Hello World ! net Core MVC.
    /// </summary>
    [Area("Exam")]
    [Route("exam/api/[controller]/[action]")]
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
        /// 
        /// </summary>
        /// <param name="Opt"></param>
        /// <param name="_Opt_API"></param>
        public TestController( IOptions<Option_ConnctionString> Opt, IOptions<Opt_API_LTEUrl> _Opt_API)
        {
            if (Opt == null || _Opt_API == null) 
            {
                LogManager.GetLogger("TestController").Error("Opt == null || _Opt_API == null");

                return;
            }

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
        /// <param name="generateCount"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult EFCore_InsertTest(int generateCount = 1000)
        {
            string sqliteDBConn = ConfigurationManager.ConnectionStrings["sqliteTestDB"].ConnectionString;

            using (CoreContext db = new CoreContext(sqliteDBConn))
            {
                if ( !db.Database.CanConnect() )
                {
                    db.Database.EnsureCreated();
                }

                #region Faker 数据模拟

                //const string charSet = @"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789~!@#$%^&*()_+-=`[]{};':"",./<>?";
                const string charSet = @"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                var testAccount = new Faker<Account>(locale: "zh_CN");//.StrictMode(true);

                long max_id_account = (
                    from
                        x
                    in
                        db.Accounts.IgnoreQueryFilters().AsEnumerable()
                    select
                        x.Id).DefaultIfEmpty(0).Max();

                long max_id_accountRole = (
                    from
                        x
                    in
                        db.AccountRoles.IgnoreQueryFilters().AsEnumerable()
                    select
                        x.Id).DefaultIfEmpty(0).Max();

                // long max_id_accountRole = db.AccountRoles

                testAccount.RuleFor( entity => entity.Id,            faker => max_id_account + faker.IndexFaker + 1 );
                testAccount.RuleFor( entity => entity.Name,          faker => faker.Random.String2(8, 16, charSet ) );
                testAccount.RuleFor( entity => entity.Introduction,  faker => faker.Rant.Review());
                testAccount.RuleFor( entity => entity.Avatar,        faker => faker.Image.PlaceholderUrl( 256, 256 ));
                testAccount.RuleFor( entity => entity.Email,         faker => faker.Phone.PhoneNumber() + "@Qing.com");
                testAccount.RuleFor( entity => entity.Password,      faker => faker.Random.String2(8, 16, charSet));
                testAccount.RuleFor( entity => entity.Username,      faker => faker.Name.FirstName() + faker.Name.LastName());
                testAccount.RuleFor( entity => entity.Phone,         faker => faker.Phone.PhoneNumber());
                testAccount.RuleFor( entity => entity.Sex,           faker => faker.Random.Int(0, 2));
                testAccount.RuleFor( entity => entity.Q_IsDelete, faker => faker.Random.Bool());

                testAccount.RuleFor( entity => entity.AccountRoles, faker =>
                {
                    bool bAddRole = faker.Random.Bool();
                    //随机生成
                    if ( bAddRole )
                    {
                        List<AccountRole> accountRoleList = faker.Make<AccountRole>(1, () =>
                        {
                            return new AccountRole
                            {
                                Id = max_id_accountRole + faker.IndexFaker +    1 ,
                                AccountId = max_id_account + faker.IndexFaker + 1 ,
                                account = null  ,
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

                Faker<RoutePage> testRoutePage = new Faker<RoutePage>(locale: "zh_CN");

                testRoutePage.RuleFor( e => e.Id, faker => (faker.IndexFaker + 1) );
                testRoutePage.RuleFor( e => e.Component, faker => "admin" );
                testRoutePage.RuleFor( e => e.Meta, faker => faker.Make<RoutePageMeta>( 1, (i) => new RoutePageMeta())[0] );
                testRoutePage.RuleFor( e => e.Name, faker => faker.Name.FirstName() + faker.Name.LastName() );
                testRoutePage.RuleFor( e => e.Path, faker => faker.Rant.Review() );
                testRoutePage.RuleFor( e => e.ParentId, faker => 1 );
                testRoutePage.RuleFor( entity => entity.Q_IsDelete, faker => faker.Random.Bool() );

                #endregion

                Stopwatch sw_insert = new Stopwatch();
                Stopwatch sw_generate = new Stopwatch();

                sw_generate.Start();
                Account[] accountList = testAccount.Generate(generateCount).ToArray();
                sw_generate.Stop();

                sw_insert.Start();
                db.Accounts.AddRange(accountList);
                int effectRowCount = db.SaveChanges();
                sw_insert.Stop();

                return Ok(new
                {
                    effectRowCount,
                    GenerateTime = sw_generate.ElapsedMilliseconds + "ms",
                    InsertTime = sw_insert.ElapsedMilliseconds + "ms"
                });
            }

        }

        /// <summary>
        /// Note：
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult EFCore_QueryTest()
        {
            string sqliteDBConn = ConfigurationManager.ConnectionStrings["sqliteTestDB"].ConnectionString;

            using (CoreContext db = new CoreContext(sqliteDBConn))
            {
                if (!db.Database.CanConnect())
                {
                    db.Database.EnsureCreated();
                }

                Stopwatch Sw = new Stopwatch();
                Sw.Start();

                var query = (from x in
                                db.Accounts.Include(e => e.AccountRoles)
                                           .ThenInclude(o => o.role)
                             select
                                 new
                                 {
                                     x.Id ,
                                     x.Name ,
                                     x.Username ,
                                     x.Sex ,
                                     x.Email ,
                                     x.Avatar ,
                                     x.Password ,
                                     x.Introduction ,
                                     x.Phone ,
                                     x.Q_IsDelete ,
                                     x.Q_CreateTime ,
                                     x.Q_DeleteTime ,
                                     x.Q_Sequence ,
                                     x.Q_Version ,
                                     x.Q_UpdateTime ,
                                     Roles = x.AccountRoles.Select( xx => xx.role.Name)
                                 });

                var list = query.Take(100).ToList();

                Sw.Stop();

                return Ok(new { time = Sw.ElapsedMilliseconds.ToString() + "ms.", list /*,list_2*/ });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ThreadingPoolTest() 
        {
            return View();
        }

        /// <summary>
        /// 乐观更新 Optimistic locking Test
        /// </summary>
        /// <param name="DebugThreadCount"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult EFCore_UpdateTest(int DebugThreadCount = 10, Int64 Id = 1)
        {
            Logger logger = LogManager.GetLogger("SQLite3Store"); // LogManager.GetLogger("UpdateTest"); 
            
            string sqliteDBConn = ConfigurationManager.ConnectionStrings["sqliteTestDB"].ConnectionString;
            Faker faker = new Faker(locale: "zh_CN");

            for (int i = 0; i < DebugThreadCount; i++)
            {

                Task.Run(() =>
                {
                    int count = 0;
                    while (true)
                    {
                        try
                        {
                            CoreContext db = new CoreContext(sqliteDBConn);
                            Account account = db.Accounts.Find(Id);

                            if (account != null)
                            {
                                account.Name = (faker.Name.FirstName() + faker.Name.LastName());
                                account.Q_Sequence++;

                                int effectCount = db.SaveChanges();
                                
                                if (effectCount < 0)
                                {
                                    Thread.Sleep(10);
                                    continue;
                                }
                                else
                                {
                                    logger.Trace($@" Optimistic locking..... Name : { account.Name          } , 
                                                     Qing_Version                 : { account.Q_Version  } ,
                                                     Qing_Sequence                : { account.Q_Sequence } ");
                                }
                            }
                            else
                            {
                                Console.WriteLine($" not account.....{count} ");
                                break;
                            }

                            count++;

                            if (count >= 1000)
                            {
                                break;
                            }
                        }
                        catch (Exception /*ex*/)
                        {
                            //Other Exception....
                            break;
                        }
                    }
                });
            }

            return Ok();
        }

        /// <summary>
        /// 导出 Excel 示例。标准
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public FileResult Export()
        {
            string newFile = @".Cache/ExportExcel/newbook.core.xlsx";

            using (FileStream fs = new FileStream(newFile, FileMode.OpenOrCreate, FileAccess.ReadWrite))
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

            if (files.Count > 0)
            {
                foreach (var fileitem in files)
                {
                    Stream stream = fileitem.OpenReadStream();
                    IWorkbook workbook = new XSSFWorkbook(stream);
                    ISheet def_sheet = workbook.GetSheetAt(0);
                    IRow row = def_sheet.GetRow(0);
                    string info = row.Cells[0].StringCellValue;
                    ret_str = info;
                    stream.Dispose();
                }
            }

            return Ok(ret_str);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UploadImageMulitple([FromForm] IFormFileCollection files)
        {
            int ret_count = 0;
            IList<string> list = new List<string>();
            IFormFileCollection files_temp = HttpContext.Request.Form.Files;
            if (files != null && files.Count > 0)
            {
                foreach (var fileitem in files)
                {
                    string filePath = @".Cache/Image/" + fileitem.FileName;

                    using (FileStream fs = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write))
                    {
                        await fileitem.CopyToAsync(fs).ConfigureAwait(false);
                        list.Add(Request.HttpContext.Connection.RemoteIpAddress.ToString() + "/" + filePath);
                        ret_count++;
                    }
                }
            }

            dynamic ret_model = new { list, effectCount = ret_count };
 
            return Ok(ret_model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult UploadImage([FromForm]IFormFileCollection files)
        {
            int ret_count = 0;
            IList<string> list = new List<string>();
            //IFormFileCollection files = HttpContext.Request.Form.Files;
            //TaskScheduler.
            if (files != null && files.Count > 0)
            {
                foreach (var fileitem in files)
                {
                    string filePath = @".Cache/Image/" + fileitem.FileName;

                    using ( FileStream fs = new FileStream( filePath, FileMode.CreateNew, FileAccess.Write ))
                    {
                        fileitem.CopyTo(fs);
                        list.Add(Request.HttpContext.Connection.RemoteIpAddress.ToString() + "/" + filePath);
                        ret_count++;
                    }
                }
            }

            dynamic ret_model = new { list, effectCount = ret_count };

            return Ok(ret_model);
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
            if (this.Request.Body != null &&
                 size > 0)
            {
                Stream stream = this.Request.Body;
                byte[] buffer = new byte[size];

                while (true)
                {
                    if (this.Request.Body.CanRead)
                    {
                        int offset = stream.Read(buffer, index, size - index);
                        index = index + offset - 1;

                        if (offset == 0)
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

            return Ok(null);
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
                    Local = localIpAddress.MapToIPv4().ToString() + ":" + localPort,
                    Remote = RemoteIpAddress.MapToIPv4().ToString() + ":" + RemotePort,
                    HostName = Dns.GetHostName()
                };

                return Ok(ret_model);
            }
            catch ( Exception ex )
            {
                return NotFound(ex.Message);
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


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult T() 
        {
            //CoreHelper q = Program.serviceProvider.GetService<CoreHelper>();
            //itestservice service = Program.serviceProvider.GetService<itestservice>();
            
            return Ok( );
        }
    }
}
