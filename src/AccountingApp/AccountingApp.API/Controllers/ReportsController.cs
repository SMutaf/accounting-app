using AccountingApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AccountingApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("account-balances")]
        public async Task<IActionResult> GetAccountBalances()
        {
            var report = await _reportService.GetAccountBalancesAsync();
            return Ok(report);
        }
    }
}