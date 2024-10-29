using System.ComponentModel.DataAnnotations;

namespace KupujemProdajemWebApp.Models
{
    public class AdvertisementCategory
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<AdvertisementGroup> AdvertisementGroups { get; set; } = new List<AdvertisementGroup>();
        public ICollection<Advertisement> Advertisements { get; set; } = new List<Advertisement>();
    }
}
