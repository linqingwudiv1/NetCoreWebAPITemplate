using DTOModelDLL.Common;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NetApplictionServiceDLL;
using WebAPI.Model.Static;
using Npoi.Core.HSSF.Util;
using Npoi.Core.SS.UserModel;
using Npoi.Core.SS.Util;
using Npoi.Core.XSSF.UserModel;
using System.IO;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Hello World ! net Core MVC.
    /// </summary>
    [Route("api/[controller]/[action]")]
    [EnableCors("WebAPIPolicy")]
    public class TestController : BaseController
    {
        private Option_ConnctionString Opt_Conn { get; set; }
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
        public dynamic HelloNetCore(string id = "")
        {
            return "Hello! Net Core 2.0: " + id;
        }

        /// <summary>
        /// 获取数据库连接信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("NetCore_DBConn")]
        public dynamic NetCore_DBConn()
        {
            return Opt_Conn;
        }


        /// <summary>
        /// 导出 Excel 示例。标准
        /// </summary>
        /// <returns></returns>
        [HttpGet("Report")]
        public FileResult Report()
        {
            var newFile = @"newbook.core.xlsx";

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
        ///  导出 Excel 读取示例
        /// </summary>
        /// <returns></returns>
        [HttpPost("Export")]
        public DTO_ReturnModel<dynamic> Export()
        {
            var files = HttpContext.Request.Form.Files;
            var ret_str = "";
            if (files.Count > 0)
            {
                foreach (var fileitem in files)
                {
                    Stream stream = fileitem.OpenReadStream();

                    IWorkbook workbook = new XSSFWorkbook(stream);
                    var def_sheet = workbook.GetSheetAt(0);
                    var row = def_sheet.GetRow(0);
                    var info = row.Cells[0].StringCellValue;
                    ret_str = info;
                }
            }

            var ret = new DTO_ReturnModel<dynamic>("");
            return ret;
        }
    }
}
