using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Patoi_Final_Project_Backend.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public int Stock { get; set; }
        public double Price { get; set; }
        public bool IsHot { get; set; }
        public bool OnSale { get; set; }
        public bool IsNew { get; set; }
        public bool IsDeleted { get; set; }

        public int? TagId { get; set; }
        public Tag Tag { get; set; } 


        [NotMapped]
        public List<IFormFile> Photo { get; set; }
        public List<ProductCategories> ProductCategories { get; set; }
        [NotMapped]
        public List<int> CategoryId { get; set; }




        public int? CampaignId { get; set; }
        public Campaign Campaign { get; set; }
        public List<Comments> Comments { get; set; }
        public List<WishListItem> WishListItems { get; set; }
        public List<ProductImage> ProductImages { get; set; }
    }
}
