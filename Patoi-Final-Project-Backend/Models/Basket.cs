using System.Collections.Generic;

namespace Patoi_Final_Project_Backend.Models
{
    public class Basket
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public List<BasketItem> BasketItems { get; set; }
    }
}
