using KupujemProdajemWebApp.Interfaces;
using KupujemProdajemWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> Create(Advertisement advertisement)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.AdvertisementCategories = _advertisementRepository.GetCategories();
                ViewBag.AdvertisementGroups = _advertisementRepository.GetGroups();

                ViewBag.ValidationErrors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return View(advertisement);
            }

            _advertisementRepository.Add(advertisement);
            return RedirectToAction("Index");
        }
    }
}
