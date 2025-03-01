﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace KupujemProdajemWebApp.Models
{
    public class User : IdentityUser
    {
        public ICollection<Advertisement> Advertisements { get; set; } = new List<Advertisement>();
        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
        [ForeignKey("Address")]
        public int AddressId { get; set; }
        public Address Address { get; set; }
    }
}
