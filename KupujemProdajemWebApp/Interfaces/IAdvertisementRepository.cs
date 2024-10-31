using KupujemProdajemWebApp.Models;

namespace KupujemProdajemWebApp.Interfaces
{
    public interface IAdvertisementRepository
    {
        Task<IEnumerable<Advertisement>> GetAll();
        Task<Advertisement> GetByIdAsync(int id);
        Task<IEnumerable<Advertisement>> GetAdsByCity(string city);
        bool Add(Advertisement advertisement);
        bool Update(Advertisement advertisement);
        bool Delete(Advertisement advertisement);
        bool Save();
        List<AdvertisementCategory> GetCategories();
        List<AdvertisementGroup> GetGroups();
    }
}
