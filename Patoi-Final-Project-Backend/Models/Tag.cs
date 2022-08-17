using System.Collections.Generic;

namespace Patoi_Final_Project_Backend.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; }
    }
}
