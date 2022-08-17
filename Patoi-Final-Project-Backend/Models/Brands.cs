using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace Patoi_Final_Project_Backend.Models
{
    public class Brands
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
