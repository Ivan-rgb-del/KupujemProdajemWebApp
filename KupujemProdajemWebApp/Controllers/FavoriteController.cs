using KupujemProdajemWebApp.Interfaces;
using KupujemProdajemWebApp.Models;
using KupujemProdajemWebApp.Repository;
using KupujemProdajemWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

namespace KupujemProdajemWebApp.Controllers
{
    public class FavoriteController : Controller
    {
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        public FavoriteController(IFavoriteRepository favoriteRepository, IHttpContextAccessor contextAccessor)
        {
            _favoriteRepository = favoriteRepository;
            _contextAccessor = contextAccessor;
        }

        [HttpPost]
        public async Task<IActionResult> SaveToFavorites(int adId)
        {
            var user = _contextAccessor.HttpContext.User.GetUserId();

            var favorite = new Favorite
            {
                UserId = user,
                AdvertisementId = adId,
            };

            bool isAdded = await _favoriteRepository.SaveToFavorites(favorite);

            if (!isAdded)
            {
                TempData["Message"] = "Ad is already in favorites.";
                return RedirectToAction("Index", "Advertisement");
            } else
            {
                TempData["Message"] = "Ad saved to favorites!";
                return RedirectToAction("Index", "Favorites");
            }
        }

        public async Task<IActionResult> Index()
        {
            var userId = _contextAccessor.HttpContext.User.GetUserId();
            var userFavoriteAds = await _favoriteRepository.GetAllFavoritesByUserId(userId);

            var savedAdsVM = new SavedAdsViewModel
            {
                FavoriteAds = userFavoriteAds
            };

            return View(savedAdsVM);
        }
    }
}
