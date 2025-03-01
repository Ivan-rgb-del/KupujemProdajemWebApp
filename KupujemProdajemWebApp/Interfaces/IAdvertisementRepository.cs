﻿using KupujemProdajemWebApp.Models;

namespace KupujemProdajemWebApp.Interfaces
{
    public interface IAdvertisementRepository
    {
        Task<IEnumerable<Advertisement>> GetAll();
        Task<Advertisement> GetByIdAsync(int id);
        Task<Advertisement> GetByIdAsyncNoTracking(int id);
        Task<IEnumerable<Advertisement>> FilterAds(string? city, int? categoryId, int? groupId, bool IsFixedPrice, bool IsReplacement, double minPrice, double maxPrice);
        bool Add(Advertisement advertisement);
        bool Update(Advertisement advertisement);
        bool Delete(Advertisement advertisement);
        bool Save();
        bool IncrementViews(int adId);
        List<AdvertisementCategory> GetCategories();
        List<AdvertisementGroup> GetGroups();
    }
}
