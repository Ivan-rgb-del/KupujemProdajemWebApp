﻿using KupujemProdajemWebApp.Data;
using KupujemProdajemWebApp.Interfaces;
using KupujemProdajemWebApp.Models;
using Microsoft.EntityFrameworkCore;

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
            _context.Add(advertisement);
            return Save();
        }

        public bool Delete(Advertisement advertisement)
        {
            _context.Remove(advertisement);
            return Save();
        }

        public async Task<IEnumerable<Advertisement>> GetAdsByCity(string city)
        {
            return await _context.Advertisements.Where(a => a.Address.City.Contains(city)).ToListAsync();
        }

        public async Task<IEnumerable<Advertisement>> GetAll()
        {
            return await _context.Advertisements.ToListAsync();
        }

        public Task<Advertisement> GetByIdAsync(int id)
        {
            return _context.Advertisements.Include(a => a.Address).FirstOrDefaultAsync(a => a.Id == id);
        }

        public List<AdvertisementCategory> GetCategories()
        {
            return _context.AdvertisementCategories.ToList();
        }

        public List<AdvertisementGroup> GetGroups()
        {
            return _context.AdvertisementGroups.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Advertisement advertisement)
        {
            _context.Update(advertisement);
            return Save();
        }
    }
}
