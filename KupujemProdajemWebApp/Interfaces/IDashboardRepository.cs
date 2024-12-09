using KupujemProdajemWebApp.Models;

namespace KupujemProdajemWebApp.Interfaces
{
    public interface IDashboardRepository
    {
        Task<List<Advertisement>> GetAllUserAds(string userId);
    }
}
