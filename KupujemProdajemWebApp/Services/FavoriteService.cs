using KupujemProdajemWebApp.Interfaces;
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

        public async Task<SavedAdsViewModel> GetAllUserSavedAds()
        {
            var userId =  _contextAccessor.HttpContext.User.GetUserId();
            var userFavoriteAds = await _favoriteRepository.GetAllFavoritesByUserId(userId);

            return new SavedAdsViewModel
            {
                FavoriteAds = userFavoriteAds
            };
        }
    }
}
