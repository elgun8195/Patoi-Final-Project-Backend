using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace Patoi_Final_Project_Backend.Models
{
    public class Fake
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        [Required, MinLength(35)]
        public string Description { get; set; }
        [Required]
        public double DiscountPercent { get; set; }
        [Required]
        public double DiscountPrice { get; set; }
        [Required]
        public double TaxPercent { get; set; }
        [Required]
        public int Count { get; set; }
        public bool IsAvailability { get; set; }
        public bool IsSpecial { get; set; }
        public bool IsFeatured { get; set; }
        public bool Bestseller { get; set; }
        public bool NewArrival { get; set; }
        public bool InStock { get; set; }
        public bool IsDeleted { get; set; }


        [NotMapped]
        public List<IFormFile> Photo { get; set; }

        

        public Nullable<DateTime> CreatedTime { get; set; }
        public Nullable<DateTime> DeletedAt { get; set; }
        public Nullable<DateTime> LastUpdatedAt { get; set; }


 

 
       // public List<ProductImage> ProductImages { get; set; }
         
    }
}
