using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Patoi_Final_Project_Backend.Models
{
    public class Campaign
    {
        public int Id { get; set; }
        [Required]
        public int DiscountPercent { get; set; }
        public List<Product> Products { get; set; }
    }
}
