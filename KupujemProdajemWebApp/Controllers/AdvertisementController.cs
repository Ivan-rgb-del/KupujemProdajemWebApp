using KupujemProdajemWebApp.Interfaces;
using KupujemProdajemWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KupujemProdajemWebApp.Controllers
{
    public class AdvertisementController : Controller
    {
        private readonly IAdvertisementRepository _advertisementRepository;

        public AdvertisementController(IAdvertisementRepository advertisementRepository)
        {
            _advertisementRepository = advertisementRepository;
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
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Advertisement advertisement)
        {
            if (!ModelState.IsValid)
            {
                return View(advertisement);
            }

            _advertisementRepository.Add(advertisement);
            return RedirectToAction("Index");
        }
    }
}
