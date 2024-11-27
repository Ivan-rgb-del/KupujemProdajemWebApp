using KupujemProdajemWebApp.Interfaces;
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
            ViewBag.AdvertisementCategories = _advertisementService.GetCategories();
            ViewBag.AdvertisementGroups = _advertisementService.GetGroups();

            var advertisement = await _advertisementService.GetAdById(id);

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
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club!");
                return View("Edit", advertisementVM);
            }

            await _advertisementService.EditAdvertisement(id, advertisementVM);
            return RedirectToAction("Index");
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