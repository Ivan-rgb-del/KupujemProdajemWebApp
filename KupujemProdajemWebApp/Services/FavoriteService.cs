using KupujemProdajemWebApp.Interfaces;
using KupujemProdajemWebApp.Models;
using KupujemProdajemWebApp.ViewModels;
using Microsoft.AspNetCore.SignalR;

namespace KupujemProdajemWebApp.Services
{
    public class FavoriteService
    {
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IHubContext<NotificationHub> _notificationHubContext;

        public FavoriteService(IFavoriteRepository favoriteRepository, IHttpContextAccessor contextAccessor, IHubContext<NotificationHub> _notificationHubContext)
        {
            _favoriteRepository = favoriteRepository;
            _contextAccessor = contextAccessor;
            _notificationHubContext = _notificationHubContext;
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

            var isSaved = await _favoriteRepository.SaveToFavorites(favorite);

            if (isSaved)
            {
                var adOwner = await _favoriteRepository.GetAdOwnerId(adId);
                var message = $"Korisnik sa ID-em {user} je sačuvao vaš oglas!";

                await _notificationHubContext.Clients.User(adOwner.ToString()).SendAsync("ReceiveNotification", message);
            }

            return isSaved;
        }

        public async Task<bool> RemovedSavedAd(int adId)
        {
            var userId = _contextAccessor.HttpContext.User.GetUserId();

            return await _favoriteRepository.RemoveFromFavorites(userId, adId);
        }
    }
}
