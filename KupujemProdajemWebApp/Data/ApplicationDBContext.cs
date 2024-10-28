using KupujemProdajemWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace KupujemProdajemWebApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<AdvertisementCategory> AdvertisementCategories { get; set; }
        public DbSet<AdvertisementGroup> AdvertisementGroups { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
    }
}
