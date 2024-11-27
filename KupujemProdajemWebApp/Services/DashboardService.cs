using KupujemProdajemWebApp.Interfaces;
using KupujemProdajemWebApp.ViewModels;

namespace KupujemProdajemWebApp.Services
{
    public class DashboardService
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardService(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public async Task<DashboardViewModel> GetAllCreatedUserAds()
        {
            var userAds = await _dashboardRepository.GetAllUserAds();
            
            return new DashboardViewModel
            {
                Advertisements = userAds,
            };
        }
    }
}
