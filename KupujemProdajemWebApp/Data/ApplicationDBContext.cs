using KupujemProdajemWebApp.Data.Enum;
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
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Advertisement>()
                .Property(a => a.AdvertisementCondition)
                .HasConversion<int>();

            modelBuilder.Entity<Advertisement>()
                .Property(a => a.DeliveryType)
                .HasConversion<int>();

            modelBuilder.Entity<Advertisement>()
                .HasOne(a => a.User)
                .WithMany(u => u.Advertisements)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Advertisement>()
                .HasOne(a => a.AdvertisementCategory)
                .WithMany(c => c.Advertisements)
                .HasForeignKey(a => a.AdvertisementCategoryId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Advertisement>()
                .HasOne(a => a.AdvertisementGroup)
                .WithMany(g => g.Advertisements)
                .HasForeignKey(a => a.AdvertisementGroupId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.User)
                .WithMany(u => u.Favorites)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.Advertisement)
                .WithMany(a => a.Favorites)
                .HasForeignKey(f => f.AdvertisementId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AdvertisementGroup>()
                .HasOne(g => g.AdvertisementCategory)
                .WithMany(c => c.AdvertisementGroups)
                .HasForeignKey(g => g.AdvertisementCategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Advertisement>()
                .HasOne(a => a.Address)
                .WithMany()
                .HasForeignKey(a => a.AddressId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Address)
                .WithMany()
                .HasForeignKey(u => u.AddressId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
