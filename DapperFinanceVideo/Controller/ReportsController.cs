using ClosedXML.Excel;
using DapperFinanceVideo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Text;

namespace DapperFinanceVideo.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IFinanceService _service;

        public ReportsController(IFinanceService service)
        {
            _service = service;
        }

        [HttpGet("CSV")]
        public async Task<IActionResult> ExportCSV([FromQuery] int? moth, [FromQuery] int? year, [FromQuery] char? type)
        {
            var transaction = await _service.GetTransactionsAsync(moth, year, type);

            var sb = new StringBuilder();
            sb.AppendLine("Id,Type,CategoryId,Description,Amount,Date");

            foreach (var t in transaction)
            {
                var line = string.Format(CultureInfo.InvariantCulture, "{0},{1},{2},\"{3}\", {4},{5}",
                    t.Id,
                    t.Type,
                    t.CategoryId,
                    t.Description?.Replace("\"", "\"\""),
                    t.Amount,
                    t.Date.ToString("dd/MM/yyyy")

                    ); 
                sb.AppendLine(line);
            }
            var bytes = Encoding.UTF8.GetBytes(sb.ToString());
            return File(bytes, "text/csv",$"transactions_{moth ?? 0}_{year ?? 0}.csv");
        }

        [HttpGet("Excel")]
        public async Task<IActionResult> ExportExcel([FromQuery] int? moth, [FromQuery] int? year, [FromQuery] char? type)
        { 
            var transactions = await _service.GetTransactionsAsync(moth, year, type);

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Transactions");
            worksheet.Cell(1, 1).Value = "Id";
            worksheet.Cell(1, 2).Value = "Type";
            worksheet.Cell(1, 3).Value = "CategoryId";  
            worksheet.Cell(1, 4).Value = "Description";
            worksheet.Cell(1, 5).Value = "Amount";
            worksheet.Cell(1, 6).Value = "Date";

            int row = 2;
            foreach (var t in transactions)
            {
                worksheet.Cell(row, 1).Value = t.Id;
                worksheet.Cell(row, 2).Value = t.Type.ToString();
                worksheet.Cell(row, 3).Value = t.CategoryId;
                worksheet.Cell(row, 4).Value = t.Description;
                worksheet.Cell(row, 5).Value = t.Amount;
                worksheet.Cell(row, 6).Value = t.Date.ToString("dd/MM/yyyy");
                row++;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Seek(0, SeekOrigin.Begin);
            var bytes = stream.ToArray();

            return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"transactions_{moth ?? 0}_{year ?? 0}.xlsx");
        }

    }
}
