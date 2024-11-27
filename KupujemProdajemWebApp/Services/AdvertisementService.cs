using KupujemProdajemWebApp.Interfaces;
using KupujemProdajemWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace KupujemProdajemWebApp.Services
{
    public class AdvertisementService
    {
        private readonly IAdvertisementRepository _advertisementRepository;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _contextAccessor;

        public AdvertisementService(IAdvertisementRepository advertisementRepository, IPhotoService photoService, IHttpContextAccessor contextAccessor)
        {
            _advertisementRepository = advertisementRepository;
            _photoService = photoService;
            _contextAccessor = contextAccessor;
        }

        public async Task<IEnumerable<Advertisement>> GetAllAdvertisements()
        {
            return await _advertisementRepository.GetAll();
        }

        public async Task<Advertisement> GetAdById(int id)
        {
            _advertisementRepository.IncrementViews(id);
            return await _advertisementRepository.GetByIdAsync(id);
        }
    }
}
