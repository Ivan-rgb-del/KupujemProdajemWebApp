using KupujemProdajemWebApp.Models;

namespace KupujemProdajemWebApp.Interfaces
{
    public interface IAdvertisementRepository
    {
        Task<IEnumerable<Advertisement>> GetAll();
        Task<Advertisement> GetByIdAsync(int id);
        bool Add(Advertisement advertisement);
        bool Update(Advertisement advertisement);
        bool Delete(Advertisement advertisement);
        bool Save();
    }
}
