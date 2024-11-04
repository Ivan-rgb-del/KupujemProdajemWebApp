using KupujemProdajemWebApp.Data.Enum;
using KupujemProdajemWebApp.Models;

namespace KupujemProdajemWebApp.ViewModels
{
    public class CreateAdViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public bool IsFixedPrice { get; set; }
        public bool IsReplacement { get; set; }
        public string Description { get; set; }
        public IFormFile ImageURL { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsActive { get; set; }
        public AdvertisementCondition AdvertisementCondition { get; set; }
        public DeliveryType DeliveryType { get; set; }
        public int AdvertisementCategoryId { get; set; }
        public int AdvertisementGroupId { get; set; }
        public Address Address { get; set; }
    }
}
