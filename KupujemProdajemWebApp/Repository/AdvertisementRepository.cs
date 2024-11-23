using KupujemProdajemWebApp.Data;
using KupujemProdajemWebApp.Interfaces;
using KupujemProdajemWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace KupujemProdajemWebApp.Repository
{
    public class AdvertisementRepository : IAdvertisementRepository
    {
        private readonly ApplicationDbContext _context;

        public AdvertisementRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Advertisement advertisement)
        {
            _context.Add(advertisement);
            return Save();
        }

        public bool Delete(Advertisement advertisement)
        {
            _context.Remove(advertisement);
            return Save();
        }

        public async Task<IEnumerable<Advertisement>> GetAll()
        {
            return await _context.Advertisements.ToListAsync();
        }

        public Task<Advertisement> GetByIdAsync(int id)
        {
            return _context.Advertisements.Include(a => a.Address).FirstOrDefaultAsync(a => a.Id == id);
        }
        public Task<Advertisement> GetByIdAsyncNoTracking(int id)
        {
            return _context.Advertisements.Include(a => a.Address).AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
        }

        public List<AdvertisementCategory> GetCategories()
        {
            return _context.AdvertisementCategories.ToList();
        }

        public List<AdvertisementGroup> GetGroups()
        {
            return _context.AdvertisementGroups.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Advertisement advertisement)
        {
            _context.Update(advertisement);
            return Save();
        }

        public async Task<IEnumerable<Advertisement>> FilterAds(string? city, int? categoryId, int? groupId, bool IsFixedPrice, bool IsReplacement, double minPrice, double maxPrice)
        {
            return await _context.Advertisements
                .Where(a => city == null || a.Address.City.Contains(city))
                .Where(a => a.AdvertisementCategoryId == categoryId)
                .Where(a => a.AdvertisementGroupId == groupId)
                .Where(a => a.IsFixedPrice == IsFixedPrice)
                .Where(a => a.IsReplacement == IsReplacement)
                .Where(a => a.Price >= minPrice && a.Price <= maxPrice)
                .ToListAsync();
        }

        public bool IncrementViews(int adId)
        {
            var ad = _context.Advertisements.FirstOrDefault(a => a.Id == adId);

            if (ad != null)
            {
                ad.Viewers = (ad.Viewers ?? 0) + 1;
            }

            return Save();
        }
    }
}
