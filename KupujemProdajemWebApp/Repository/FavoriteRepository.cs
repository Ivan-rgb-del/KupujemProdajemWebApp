using KupujemProdajemWebApp.Data;
using KupujemProdajemWebApp.Interfaces;
using KupujemProdajemWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KupujemProdajemWebApp.Repository
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly ApplicationDbContext _context;

        public FavoriteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Favorite>> GetAllFavoritesByUserId(string userId)
        {
            var userSavedAds = await _context.Favorites
                .Include(f => f.Advertisement)
                .Where(f => f.UserId == userId)
                .ToListAsync();

            return userSavedAds;
        }

        public async Task<bool> RemoveFromFavorites(string userId, int adId)
        {
            var favorite = await _context.Favorites.FirstOrDefaultAsync(f => f.AdvertisementId == adId && f.UserId == userId);

            _context.Favorites.Remove(favorite);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public async Task<bool> SaveToFavorites(Favorite favorite)
        {
            var exsistingFavorite = await _context.Favorites.FirstOrDefaultAsync(f => f.UserId == favorite.UserId && f.AdvertisementId == favorite.AdvertisementId);

            if (exsistingFavorite != null)
            {
                return false;
            }

            _context.Favorites.Add(favorite);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
