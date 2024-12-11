using KupujemProdajemWebApp.Interfaces;
using KupujemProdajemWebApp.Models;
using KupujemProdajemWebApp.ViewModels;

namespace KupujemProdajemWebApp.Services
{
    public class FavoriteService
    {
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IHttpContextAccessor _contextAccessor;

        public FavoriteService(IFavoriteRepository favoriteRepository, IHttpContextAccessor contextAccessor)
        {
            _favoriteRepository = favoriteRepository;
            _contextAccessor = contextAccessor;
        }

        public async Task<List<SavedAdsViewModel>> GetAllUserSavedAds()
        {
            var userId = _contextAccessor.HttpContext.User.GetUserId();
            var userFavoriteAds = await _favoriteRepository.GetAllFavoritesByUserId(userId);

            return userFavoriteAds.Select(f => new SavedAdsViewModel
            {
                AdvertisementId = f.Advertisement.Id,
                Title = f.Advertisement.Title,
                Price = f.Advertisement.Price,
                Description = f.Advertisement.Description,
                ImageUrl = f.Advertisement.ImageURL
            }).ToList();
        }

        public async Task<bool> SaveAdToFavorite(int adId)
        {
            var user = _contextAccessor.HttpContext.User.GetUserId();

            var favorite = new Favorite
            {
                UserId = user,
                AdvertisementId = adId,
            };

            return await _favoriteRepository.SaveToFavorites(favorite);
        }

        public async Task<bool> RemovedSavedAd(int adId)
        {
            var userId = _contextAccessor.HttpContext.User.GetUserId();

            return await _favoriteRepository.RemoveFromFavorites(userId, adId);
        }
    }
}
