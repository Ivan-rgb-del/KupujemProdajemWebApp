using KupujemProdajemWebApp.Interfaces;
using KupujemProdajemWebApp.Models;
using KupujemProdajemWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

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

        public IEnumerable<AdvertisementCategory> GetCategories()
        {
            return _advertisementRepository.GetCategories();
        }

        public IEnumerable<AdvertisementGroup> GetGroups()
        {
            return _advertisementRepository.GetGroups();
        }

        public async Task CreateNewAdvertisement(CreateAdViewModel advertisementVM)
        {
            var result = await _photoService.AddPhotoAsync(advertisementVM.ImageURL);
            var advertisement = new Advertisement
            {
                Title = advertisementVM.Title,
                Price = advertisementVM.Price,
                IsFixedPrice = advertisementVM.IsFixedPrice,
                IsReplacement = advertisementVM.IsReplacement,
                Description = advertisementVM.Description,
                ImageURL = result.Url.ToString(),
                IsActive = advertisementVM.IsActive,
                AdvertisementCondition = advertisementVM.AdvertisementCondition,
                DeliveryType = advertisementVM.DeliveryType,
                AdvertisementCategoryId = advertisementVM.AdvertisementCategoryId,
                AdvertisementGroupId = advertisementVM.AdvertisementGroupId,
                UserId = advertisementVM.AppUserId,
                Address = new Address
                {
                    City = advertisementVM.Address.City,
                    Street = advertisementVM.Address.Street
                }
            };

            _advertisementRepository.Add(advertisement);
        }
    }
}
