using KupujemProdajemWebApp.Data;
using KupujemProdajemWebApp.Interfaces;
using KupujemProdajemWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KupujemProdajemWebApp.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardController(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public async Task<IActionResult> Index()
        {
            var userAds = await _dashboardRepository.GetAllUserAds();
            var dashboardViewModel = new DashboardViewModel
            {
                Advertisements = userAds,
            };
            return View(dashboardViewModel);
        }
    }
}
