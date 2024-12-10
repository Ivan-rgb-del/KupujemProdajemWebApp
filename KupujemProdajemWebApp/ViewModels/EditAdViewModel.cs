using KupujemProdajemWebApp.Data.Enum;
using KupujemProdajemWebApp.Models;

namespace KupujemProdajemWebApp.ViewModels
{
    public class EditAdViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public bool IsFixedPrice { get; set; }
        public bool IsReplacement { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string? URL { get; set; }
        public int? Viewers { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsActive { get; set; }
        public AdvertisementCondition AdvertisementCondition { get; set; }
        public DeliveryType DeliveryType { get; set; }
        public int? AdvertisementCategoryId { get; set; }
        public int? AdvertisementGroupId { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public string AppUserId { get; set; }
    }
}
