using Patoi_Final_Project_Backend.Migrations;
using Patoi_Final_Project_Backend.Models;
using System.Collections.Generic;

namespace Patoi_Final_Project_Backend.ViewModels
{
    public class ShopVM
    {
        public List<Product> Products { get; set; }
        public Product Product { get; set; }
        public List<Comments> Comments { get; set; }
        public string Username { get; set; }
        //public Basket UserBakset { get; set; }
        public int UserBasketProductCount { get; set; }
    }
}
