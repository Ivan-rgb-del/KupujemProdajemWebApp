using System.ComponentModel.DataAnnotations;

namespace KupujemProdajemWebApp.Models
{
    public class AdvertisementCategory
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
