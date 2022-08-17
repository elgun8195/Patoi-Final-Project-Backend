using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Patoi_Final_Project_Backend.Models
{
    public class Categories
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
        public List<ProductCategories> ProductCategories { get; set; }

    }
}
