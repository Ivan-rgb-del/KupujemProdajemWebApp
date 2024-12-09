using KupujemProdajemWebApp.Models;
using KupujemProdajemWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        public async Task<IActionResult> Dashboard()
        {
            try
            {
                var userId = User.GetUserId();

                var userAds = await _dashboardService.GetAllCreatedUserAds(userId);

                return Ok(userAds);
            }
            catch (InvalidOperationException ex)
            {
                return Unauthorized(new { message = "User is not authenticated!", error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred.", error = ex.Message });
            }
        }
    }
}