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
        public AdvertisementCondition Condition { get; set; }
        public DeliveryType DeliveryType { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
