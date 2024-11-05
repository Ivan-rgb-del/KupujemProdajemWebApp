using KupujemProdajemWebApp.Interfaces;
using KupujemProdajemWebApp.Models;
using KupujemProdajemWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KupujemProdajemWebApp.Controllers
{
    public class AdvertisementController : Controller
    {
        private readonly IAdvertisementRepository _advertisementRepository;

        private readonly IPhotoService _photoService;

        public AdvertisementController(IAdvertisementRepository advertisementRepository, IPhotoService photoService)
        {
            _advertisementRepository = advertisementRepository;
            _photoService = photoService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Advertisement> advertisements = await _advertisementRepository.GetAll();
            return View(advertisements);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Advertisement advertisement = await _advertisementRepository.GetByIdAsync(id);
            return View(advertisement);
        }

        public IActionResult Create()
        {
            ViewBag.AdvertisementCategories = _advertisementRepository.GetCategories();
            ViewBag.AdvertisementGroups = _advertisementRepository.GetGroups();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAdViewModel advertisementVM)
        {
            if (ModelState.IsValid)
            {
                ViewBag.AdvertisementCategories = _advertisementRepository.GetCategories();
                ViewBag.AdvertisementGroups = _advertisementRepository.GetGroups();

                ViewBag.ValidationErrors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                var result = await _photoService.AddPhotoAsync(advertisementVM.ImageURL);
                var advertisement = new Advertisement
                {
                    Title = advertisementVM.Title,
                    Price = advertisementVM.Price,
                    IsFixedPrice = advertisementVM.IsFixedPrice,
                    IsReplacement = advertisementVM.IsReplacement,
                    Description = advertisementVM.Description,
                    ImageURL = result.Url.ToString(),
                    CreatedOn = advertisementVM.CreatedOn,
                    IsActive = advertisementVM.IsActive,
                    AdvertisementCondition = advertisementVM.AdvertisementCondition,
                    DeliveryType = advertisementVM.DeliveryType,
                    AdvertisementCategoryId = advertisementVM.AdvertisementCategoryId,
                    AdvertisementGroupId = advertisementVM.AdvertisementGroupId,
                    Address = new Address
                    {
                        City = advertisementVM.Address.City,
                        Street = advertisementVM.Address.Street
                    }
                    
                };

                _advertisementRepository.Add(advertisement);
                return RedirectToAction("Index");
            } else
            {
                ModelState.AddModelError("", "Photo upload failed!");
            }

            return View(advertisementVM);
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.AdvertisementCategories = _advertisementRepository.GetCategories();
            ViewBag.AdvertisementGroups = _advertisementRepository.GetGroups();

            var advertisement = await _advertisementRepository.GetByIdAsync(id);

            if (advertisement == null) return View("Error");

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
                Address = advertisement.Address
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
                    CreatedOn = advertisementVM.CreatedOn,
                    IsActive = advertisementVM.IsActive,
                    AdvertisementCondition = advertisementVM.AdvertisementCondition,
                    DeliveryType = advertisementVM.DeliveryType,
                    AdvertisementCategoryId = advertisementVM.AdvertisementCategoryId,
                    AdvertisementGroupId = advertisementVM.AdvertisementGroupId,
                    AddressId = advertisementVM.AddressId,
                    Address = advertisementVM.Address
                };

                _advertisementRepository.Update(advertisement);

                return RedirectToAction("Index");
            } else
            {
                return View(advertisementVM);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var ad = await _advertisementRepository.GetByIdAsync(id);

            if (ad == null) return View("Error");

            _advertisementRepository.Delete(ad);
            return RedirectToAction("Index");
        }
    }
}
