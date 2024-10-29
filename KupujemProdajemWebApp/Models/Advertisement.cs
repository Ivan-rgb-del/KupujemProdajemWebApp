using KupujemProdajemWebApp.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KupujemProdajemWebApp.Models
{
    public class Advertisement
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public bool IsFixedPrice { get; set; }
        public bool IsReplacement { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public int Likes { get; set; }
        public int Viewers { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsActive { get; set; }

        public AdvertisementCondition AdvertisementCondition { get; set; }
        public DeliveryType DeliveryType { get; set; }

        [ForeignKey("AdvertisementCategory")]
        public int AdvertisementCategoryId { get; set; }
        public AdvertisementCategory AdvertisementCategory { get; set; }

        [ForeignKey("AdvertisementGroup")]
        public int AdvertisementGroupId { get; set; }
        public AdvertisementGroup AdvertisementGroup { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
    }
}
