using KupujemProdajemWebApp.Data;
using KupujemProdajemWebApp.Interfaces;
using KupujemProdajemWebApp.Models;

namespace KupujemProdajemWebApp.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DashboardRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<Advertisement>> GetAllUserAds()
        {
            var curUser = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userAds = _context.Advertisements.Where(a => a.UserId == curUser);

            return userAds.ToList();
        }
    }
}
