using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace Patoi_Final_Project_Backend.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string ImageUrl { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
