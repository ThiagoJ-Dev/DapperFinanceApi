using DapperFinanceVideo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DapperFinanceVideo.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashBoardController : ControllerBase
    {
        private readonly IFinanceService _service;

        public DashBoardController(IFinanceService service)
        {
            _service = service;
        }

        [HttpGet("Summary")]
        public async Task<IActionResult> GetSummary([FromQuery] int month, [FromQuery] int year)
        {
            var result = await _service.GetDashboardAsync(month, year);
            return Ok(result);
        }

        [HttpGet("SummaryByCategory")]
        public async Task<IActionResult> GetSummaryByCategory([FromQuery] int? month, [FromQuery] int? year)
        {
            var result = await _service.GetCategoryTotalsAsync(month, year);
            return Ok(result);
        }
    }
}
