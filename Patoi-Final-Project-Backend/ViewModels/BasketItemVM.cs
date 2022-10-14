using Patoi_Final_Project_Backend.Models;

namespace Patoi_Final_Project_Backend.ViewModels
{
    public class BasketItemVM
    {
        public Product Product { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
    }
}
