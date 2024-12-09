using KupujemProdajemWebApp.Data;
using KupujemProdajemWebApp.Interfaces;
using KupujemProdajemWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace KupujemProdajemWebApp.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ApplicationDbContext _context;

        public DashboardRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Advertisement>> GetAllUserAds(string userId)
        {
            return await _context.Advertisements
                                 .Where(a => a.UserId == userId)
                                 .ToListAsync();
        }
    }
}
