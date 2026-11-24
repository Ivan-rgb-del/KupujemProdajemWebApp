using KupujemProdajemWebApp.Interfaces;
using KupujemProdajemWebApp.Models;
using KupujemProdajemWebApp.ViewModels;

namespace KupujemProdajemWebApp.Services
{
    public class AdvertisementService
    {
        private readonly IAdvertisementRepository _advertisementRepository;
        private readonly IHttpContextAccessor _contextAccessor;

        public AdvertisementService(IAdvertisementRepository advertisementRepository, IHttpContextAccessor contextAccessor)
        {
            _advertisementRepository = advertisementRepository;
            _contextAccessor = contextAccessor;
        }

        public Task<IEnumerable<Advertisement>> GetAllAdvertisements() =>
            _advertisementRepository.GetAll();

        public async Task<Advertisement> GetAdById(int id)
        {
            _advertisementRepository.IncrementViews(id);
            return await _advertisementRepository.GetByIdAsync(id);
        }

        public IEnumerable<AdvertisementCategory> GetCategories() =>
            _advertisementRepository.GetCategories();

        public IEnumerable<AdvertisementGroup> GetGroups() =>
            _advertisementRepository.GetGroups();

        public async Task<Advertisement> CreateNewAdvertisement(CreateAdViewModel advertisementVM)
        {
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
                UserId = _contextAccessor.HttpContext.User.GetUserId(),
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

            var currentUserId = _contextAccessor.HttpContext.User.GetUserId();
            if (ad.UserId != currentUserId)
            {
                throw new UnauthorizedAccessException();
            }

            var advertisement = new Advertisement
            {
                Id = id,
                Title = advertisementVM.Title,
                Price = advertisementVM.Price,
                IsFixedPrice = advertisementVM.IsFixedPrice,
                IsReplacement = advertisementVM.IsReplacement,
                Description = advertisementVM.Description,
                ImageURL = advertisementVM.Image,
                Viewers = ad.Viewers,
                IsActive = advertisementVM.IsActive,
                AdvertisementCondition = advertisementVM.AdvertisementCondition,
                DeliveryType = advertisementVM.DeliveryType,
                AdvertisementCategoryId = advertisementVM.AdvertisementCategoryId,
                AdvertisementGroupId = advertisementVM.AdvertisementGroupId,
                AddressId = advertisementVM.AddressId,
                Address = advertisementVM.Address,
                UserId = ad.UserId,
            };

            _advertisementRepository.Update(advertisement);
            return advertisement;
        }

        public async Task DeleteAdvertisement(int id)
        {
            var ad = await _advertisementRepository.GetByIdAsyncNoTracking(id);

            if (ad == null) return;

            var currentUserId = _contextAccessor.HttpContext.User.GetUserId();
            if (ad.UserId != currentUserId)
            {
                throw new UnauthorizedAccessException();
            }

            _advertisementRepository.Delete(ad);
        }

        public Task<IEnumerable<Advertisement>> FilterAds(string? city, int? categoryId, int? groupId, bool IsFixedPrice, bool IsReplacement, double minPrice, double maxPrice) =>
            _advertisementRepository.FilterAds(city, categoryId, groupId, IsFixedPrice, IsReplacement, minPrice, maxPrice);
    }
}
