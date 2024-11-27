using KupujemProdajemWebApp.Interfaces;
using KupujemProdajemWebApp.Models;
using KupujemProdajemWebApp.Repository;
using KupujemProdajemWebApp.Services;
using KupujemProdajemWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

namespace KupujemProdajemWebApp.Controllers
{
    public class FavoriteController : Controller
    {
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly FavoriteService _favoriteService;

        public FavoriteController(IFavoriteRepository favoriteRepository, IHttpContextAccessor contextAccessor, FavoriteService favoriteService)
        {
            _favoriteRepository = favoriteRepository;
            _contextAccessor = contextAccessor;
            _favoriteService = favoriteService;
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
                return RedirectToAction("Index", "Favorite");
            }
        }

        public async Task<IActionResult> Index()
        {
            var savedAdsVM = await _favoriteService.GetAllUserSavedAds();
            return View(savedAdsVM);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveSavedAdFromFavorite(int adId)
        {
            var userId = _contextAccessor.HttpContext.User.GetUserId();

            var isDeleted = await _favoriteRepository.RemoveFromFavorites(userId, adId);

            if (isDeleted)
            {
                TempData["Message"] = "Ad removed from favorites!";
            }
            else
            {
                TempData["Message"] = "Ad was not found in favorites.";
            }

            return RedirectToAction("Index", "Advertisement");
        }
    }
}
