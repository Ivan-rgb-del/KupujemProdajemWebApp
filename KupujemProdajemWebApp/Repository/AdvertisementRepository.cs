using KupujemProdajemWebApp.Data;
using KupujemProdajemWebApp.Interfaces;
using KupujemProdajemWebApp.Models;

namespace KupujemProdajemWebApp.Repository
{
    public class AdvertisementRepository : IAdvertisementRepository
    {
        private readonly ApplicationDbContext _context;

        public AdvertisementRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Advertisement advertisement)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Advertisement advertisement)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Advertisement>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Advertisement> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public bool Update(Advertisement advertisement)
        {
            throw new NotImplementedException();
        }
    }
}
