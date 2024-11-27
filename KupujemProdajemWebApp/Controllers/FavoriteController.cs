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
        private readonly FavoriteService _favoriteService;

        public FavoriteController(FavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }

        [HttpPost]
        public async Task<IActionResult> SaveToFavorites(int adId)
        {
            bool isAdded = await _favoriteService.SaveAdToFavorite(adId);

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
            var isDeleted = await _favoriteService.RemovedSavedAd(adId);

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
