using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Patoi_Final_Project_Backend.Models
{
    public class AppUser : IdentityUser
    {
        public string Fullname { get; set; }
        public List<BasketItem> BasketItems { get; set; }
    }
}
