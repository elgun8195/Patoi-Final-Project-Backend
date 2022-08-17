using Microsoft.AspNetCore.Identity;

namespace Patoi_Final_Project_Backend.Models
{
    public class AppUser : IdentityUser
    {
        public string Fullname { get; set; }
    }
}
