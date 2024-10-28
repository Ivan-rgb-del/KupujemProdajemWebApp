using System.ComponentModel.DataAnnotations;

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
        public DateOnly CreatedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
