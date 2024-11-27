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

            var advertisementVM = await _advertisementService.GetAdForEdit(id);

            if (advertisementVM == null) return View("Error");

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
            await _advertisementService.DeleteAdvertisement(id);
            return RedirectToAction("Index");
        }
    }
}