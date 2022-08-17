using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Patoi_Final_Project_Backend.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Position { get; set; }
        public string ImageUrl { get; set; }
        public string Desc { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
