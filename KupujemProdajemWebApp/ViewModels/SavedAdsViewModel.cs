using KupujemProdajemWebApp.Models;

namespace KupujemProdajemWebApp.ViewModels
{
    public class SavedAdsViewModel
    {
        public int AdvertisementId { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
