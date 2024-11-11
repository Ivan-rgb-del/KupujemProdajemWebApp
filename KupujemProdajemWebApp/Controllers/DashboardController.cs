using KupujemProdajemWebApp.Data;
using KupujemProdajemWebApp.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KupujemProdajemWebApp.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardController(ApplicationDbContext context, IDashboardRepository dashboardRepository)
        {
            _context = context;
            _dashboardRepository = dashboardRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
