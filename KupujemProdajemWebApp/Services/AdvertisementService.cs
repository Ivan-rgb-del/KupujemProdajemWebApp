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

        public async Task<Advertisement> CreateNewAdvertisement(CreateAdViewModel advertisementVM)
        {
            //var result = await _photoService.AddPhotoAsync(advertisementVM.ImageURL);
            var advertisement = new Advertisement
            {
                Title = advertisementVM.Title,
                Price = advertisementVM.Price,
                IsFixedPrice = advertisementVM.IsFixedPrice,
                IsReplacement = advertisementVM.IsReplacement,
                Description = advertisementVM.Description,
                ImageURL = advertisementVM.ImageURL,
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
            return advertisement;
        }

        public async Task<EditAdViewModel> GetAdForEdit(int id)
        {
            var advertisement = await _advertisementRepository.GetByIdAsync(id);

            if (advertisement == null) return null;

            var curUserId = _contextAccessor.HttpContext.User.GetUserId();

            return new EditAdViewModel
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
        }

        public async Task<Advertisement> EditAdvertisement(int id, EditAdViewModel advertisementVM)
        {
            var ad = await _advertisementRepository.GetByIdAsyncNoTracking(id);

            if (ad == null)
            {
                throw new Exception("Advertisement not found.");
            }

            if (!string.IsNullOrEmpty(ad.ImageURL))
            {
                await _photoService.DeletePhotoAsync(ad.ImageURL);
            }

            //var photoResult = await _photoService.AddPhotoAsync(advertisementVM.Image);
            var advertisement = new Advertisement
            {
                Id = id,
                Title = advertisementVM.Title,
                Price = advertisementVM.Price,
                IsFixedPrice = advertisementVM.IsFixedPrice,
                IsReplacement = advertisementVM.IsReplacement,
                Description = advertisementVM.Description,
                ImageURL = advertisementVM.Image,
                Viewers = advertisementVM.Viewers,
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
            return advertisement;
        }

        public async Task DeleteAdvertisement(int id)
        {
            var ad = await _advertisementRepository.GetByIdAsyncNoTracking(id);

            if (ad != null)
            {
                _advertisementRepository.Delete(ad);
            }
        }

        public async Task<IEnumerable<Advertisement>> FilterAds(string? city, int? categoryId, int? groupId, bool IsFixedPrice, bool IsReplacement, double minPrice, double maxPrice)
        {
            return await _advertisementRepository.FilterAds(city, categoryId, groupId, IsFixedPrice, IsReplacement, minPrice, maxPrice);
        }
    }
}
