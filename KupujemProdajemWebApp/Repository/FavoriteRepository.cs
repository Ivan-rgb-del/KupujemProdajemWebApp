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
        private readonly IHttpContextAccessor _contextAccessor;

        public FavoriteRepository(ApplicationDbContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;
        }

        public async Task<List<Favorite>> GetAllFavoritesByUserId()
        {
            var userId = _contextAccessor.HttpContext.User.GetUserId();
            var userSavedAds = _context.Favorites.Where(f => f.UserId == userId);

            return userSavedAds.ToList();
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
