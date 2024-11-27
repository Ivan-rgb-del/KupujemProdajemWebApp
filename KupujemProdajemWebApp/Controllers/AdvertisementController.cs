﻿using KupujemProdajemWebApp.Interfaces;
using KupujemProdajemWebApp.Models;
using KupujemProdajemWebApp.Services;
using KupujemProdajemWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KupujemProdajemWebApp.Controllers
{
    public class AdvertisementController : Controller
    {
        private readonly IAdvertisementRepository _advertisementRepository;
        private readonly AdvertisementService _advertisementService;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AdvertisementController(IAdvertisementRepository advertisementRepository, IPhotoService photoService, IHttpContextAccessor httpContextAccessor, AdvertisementService advertisementService)
        {
            _advertisementRepository = advertisementRepository;
            _photoService = photoService;
            _httpContextAccessor = httpContextAccessor;
            _advertisementService = advertisementService;
        }

        public async Task<IActionResult> Index()
        {
            var ads = await _advertisementService.GetAllAdvertisements();
            return View(ads);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var advertisement = await _advertisementService.GetAdById(id);
            return View(advertisement);
        }

        public IActionResult Filter()
        {
            ViewBag.AdvertisementCategories = _advertisementRepository.GetCategories();
            ViewBag.AdvertisementGroups = _advertisementRepository.GetGroups();
            return View();
        }

        public async Task<IActionResult> FilterAds(string? city, int? categoryId, int? groupId, bool IsFixedPrice, bool IsReplacement, double minPrice, double maxPrice)
        {
            var ads = await _advertisementRepository.FilterAds(city, categoryId, groupId, IsFixedPrice, IsReplacement, minPrice, maxPrice);

            return View("Index", ads);
        }

        public IActionResult Create()
        {
            ViewBag.AdvertisementCategories = _advertisementService.GetCategories();
            ViewBag.AdvertisementGroups = _advertisementService.GetGroups();

            var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var createAdViewModel = new CreateAdViewModel
            {
                AppUserId = curUserId,
            };

            return View(createAdViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAdViewModel advertisementVM)
        {
            if (ModelState.IsValid)
            {
                await _advertisementService.CreateNewAdvertisement(advertisementVM);
                return RedirectToAction("Index");
            }

            return View(advertisementVM);
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.AdvertisementCategories = _advertisementRepository.GetCategories();
            ViewBag.AdvertisementGroups = _advertisementRepository.GetGroups();

            var advertisement = await _advertisementRepository.GetByIdAsync(id);

            if (advertisement == null) return View("Error");

            var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();

            var advertisementVM = new EditAdViewModel
            {
                Title = advertisement.Title,
                Price = advertisement.Price,
                IsFixedPrice = advertisement.IsFixedPrice,
                IsReplacement = advertisement.IsReplacement,
                Description = advertisement.Description,
                URL = advertisement.ImageURL,
                CreatedOn = advertisement.CreatedOn,
                IsActive = advertisement.IsActive,
                AdvertisementCondition = advertisement.AdvertisementCondition,
                DeliveryType = advertisement.DeliveryType,
                AdvertisementCategoryId = advertisement.AdvertisementCategoryId,
                AdvertisementGroupId = advertisement.AdvertisementGroupId,
                AddressId = advertisement.AddressId,
                Address = advertisement.Address,
                AppUserId = curUserId,
            };

            return View(advertisementVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditAdViewModel advertisementVM)
        {
            ViewBag.AdvertisementCategories = _advertisementRepository.GetCategories();
            ViewBag.AdvertisementGroups = _advertisementRepository.GetGroups();

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club!");
                return View("Edit", advertisementVM);
            }

            var ad = await _advertisementRepository.GetByIdAsyncNoTracking(id);

            if (ad != null)
            {
                try
                {
                    await _photoService.DeletePhotoAsync(ad.ImageURL);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could not delete photo!");
                    return View(advertisementVM);
                }

                var photoResult = await _photoService.AddPhotoAsync(advertisementVM.Image);

                var advertisement = new Advertisement
                {
                    Id = id,
                    Title = advertisementVM.Title,
                    Price = advertisementVM.Price,
                    IsFixedPrice = advertisementVM.IsFixedPrice,
                    IsReplacement = advertisementVM.IsReplacement,
                    Description = advertisementVM.Description,
                    ImageURL = photoResult.Url.ToString(),
                    IsActive = advertisementVM.IsActive,
                    AdvertisementCondition = advertisementVM.AdvertisementCondition,
                    DeliveryType = advertisementVM.DeliveryType,
                    AdvertisementCategoryId = advertisementVM.AdvertisementCategoryId,
                    AdvertisementGroupId = advertisementVM.AdvertisementGroupId,
                    AddressId = advertisementVM.AddressId,
                    Address = advertisementVM.Address,
                    UserId = advertisementVM.AppUserId,
                };

                _advertisementRepository.Update(advertisement);

                return RedirectToAction("Index");
            }
            else
            {
                return View(advertisementVM);
            }
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var ad = await _advertisementRepository.GetByIdAsyncNoTracking(id);

            if (ad == null) return View("Error");

            _advertisementRepository.Delete(ad);
            return RedirectToAction("Index");
        }
    }
}