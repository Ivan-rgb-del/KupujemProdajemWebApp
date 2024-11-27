using KupujemProdajemWebApp.Data;
using KupujemProdajemWebApp.Interfaces;
using KupujemProdajemWebApp.Services;
using KupujemProdajemWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KupujemProdajemWebApp.Controllers
{
    public class DashboardController : Controller
    {
        private readonly DashboardService _dashboardService;

        public DashboardController(DashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public async Task<IActionResult> Index()
        {
            var dashboardViewModel = await _dashboardService.GetAllCreatedUserAds();
            return View(dashboardViewModel);
        }
    }
}
