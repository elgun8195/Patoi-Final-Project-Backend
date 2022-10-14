using Patoi_Final_Project_Backend.Models;
using System.Collections.Generic;

namespace Patoi_Final_Project_Backend.ViewModels
{
    public class BasketVM
    {
        public List<BasketItemVM> BasketItems { get; set; }
        public double TotalPrice { get; set; }
        public int Count { get; set; }
    }
}
