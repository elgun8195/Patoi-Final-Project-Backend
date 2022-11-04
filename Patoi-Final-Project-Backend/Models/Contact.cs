using System;
using System.ComponentModel.DataAnnotations;

namespace Patoi_Final_Project_Backend.Models
{
    public class Contact
    {
        public int Id { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public bool Accept { get; set; }
        public DateTime Date { get; set; }
    }
}
