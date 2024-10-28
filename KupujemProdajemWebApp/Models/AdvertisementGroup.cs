using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KupujemProdajemWebApp.Models
{
    public class AdvertisementGroup
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("AdvertisementCategory")]
        public int AdvertisementGroupId { get; set; }
        public AdvertisementCategory Category { get; set; }
    }
}
