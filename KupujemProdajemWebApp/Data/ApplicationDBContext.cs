using KupujemProdajemWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace KupujemProdajemWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
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

            List<IdentityRole> roles = new List<IdentityRole>()
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                },
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);

            modelBuilder.Entity<Advertisement>()
                .Property(a => a.AdvertisementCondition)
                .HasConversion<int>();

            modelBuilder.Entity<Advertisement>()
                .Property(a => a.DeliveryType)
                .HasConversion<int>();

            //modelBuilder.Entity<Advertisement>()
            //    .HasOne<User>()
            //    .WithMany()
            //    .HasForeignKey(a => a.UserId)
            //    .OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.Entity<Advertisement>()
            //    .HasOne<AdvertisementCategory>()
            //    .WithMany()
            //    .HasForeignKey(a => a.AdvertisementCategoryId)
            //    .OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.Entity<Advertisement>()
            //    .HasOne<AdvertisementGroup>()
            //    .WithMany()
            //    .HasForeignKey(a => a.AdvertisementGroupId)
            //    .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.User)
                .WithMany(u => u.Favorites)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.Advertisement)
                .WithMany(a => a.Favorites)
                .HasForeignKey(f => f.AdvertisementId)
                .OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.Entity<AdvertisementGroup>()
            //    .HasOne(g => g.AdvertisementCategory)
            //    .WithMany(c => c.AdvertisementGroups)
            //    .HasForeignKey(g => g.AdvertisementCategoryId)
            //    .OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.Entity<Advertisement>()
            //    .HasOne(a => a.Address)
            //    .WithMany()
            //    .HasForeignKey(a => a.AddressId)
            //    .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<User>()
            //    .HasOne(u => u.Address)
            //    .WithMany()
            //    .HasForeignKey(u => u.AddressId)
            //    .OnDelete(DeleteBehavior.Cascade);
        }
        public override int SaveChanges()
        {
            foreach (var item in this.ChangeTracker.Entries()
            .Where(e => e.Entity is Models.BaseClass && (e.State == EntityState.Added || e.State == EntityState.Modified))
            .Select(e => e.Entity as Models.BaseClass)
            )
            {
                if (item.CreatedOn <= DateTime.MinValue)
                {
                    item.CreatedOn = DateTime.UtcNow;
                }
                else
                {
                    item.UpdatedOn = DateTime.UtcNow;
                }
            }

            return base.SaveChanges();
        }
    }
}
