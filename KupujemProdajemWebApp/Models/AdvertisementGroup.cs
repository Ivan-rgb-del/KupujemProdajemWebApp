using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KupujemProdajemWebApp.Models
{
    public class AdvertisementGroup
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Advertisement> Advertisements { get; set; } = new List<Advertisement>();
        [ForeignKey("AdvertisementCategory")]
        public int AdvertisementCategoryId { get; set; }
        public AdvertisementCategory AdvertisementCategory { get; set; }
    }
}
