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
