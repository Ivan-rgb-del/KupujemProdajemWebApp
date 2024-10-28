namespace KupujemProdajemWebApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string City { get; set; }
        public string Addrress { get; set; }
        public int PhoneNumber { get; set; }
        public ICollection<Advertisement> Advertisements { get; set; } = new List<Advertisement>();
        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
    }
}
