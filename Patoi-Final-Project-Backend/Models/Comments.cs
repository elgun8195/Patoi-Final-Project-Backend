using System.ComponentModel.DataAnnotations;
using System;

namespace Patoi_Final_Project_Backend.Models
{
    public class Comments
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 200)]
        public string Message { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public bool IsAccess { get; set; }

        public int? BlogId { get; set; }
        public Blog Blog { get; set; }
        public int? ProductId { get; set; }
        public Product Product { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
