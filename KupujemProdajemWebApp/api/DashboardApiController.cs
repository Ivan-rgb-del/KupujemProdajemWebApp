using KupujemProdajemWebApp.Models;
using KupujemProdajemWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace KupujemProdajemWebApp.api
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardApiController : ControllerBase
    {
        private readonly DashboardService _dashboardService;

        public DashboardApiController(DashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("dashboard")]
        public async Task<ActionResult<IEnumerable<Advertisement>>> Dashboard()
        {
            var ads = await _dashboardService.GetAllCreatedUserAds();
            return Ok(ads);
        }
    }
}