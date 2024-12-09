using KupujemProdajemWebApp.Interfaces;
using KupujemProdajemWebApp.Models;
using KupujemProdajemWebApp.ViewModels;
using System.Security.Claims;

namespace KupujemProdajemWebApp.Services
{
    public class DashboardService
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardService(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public async Task<List<Advertisement>> GetAllCreatedUserAds(string userId)
        {
            return await _dashboardRepository.GetAllUserAds(userId);
        }
    }
}
