using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace Patoi_Final_Project_Backend.Models
{
    public class Order
    {
        public int Id { get; set; }
         
        [Required]
        public string CompanyName { get; set; }
        
        public string Region { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string City { get; set; }
        
        [Required]
        public string StreetAddress { get; set; }
        [Required]
        public string Apartment { get; set; }
        [Required]
        public double TotalPrice { get; set; }
        [Required]
        public string PostCode { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public bool BankTransfer { get; set; }
        [Required]
        public bool CheckPayments { get; set; }
        [Required]
        public bool Paypal { get; set; }
        [Required]
        public bool CashOnDelivery { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public bool? Status { get; set; }
        public string Message { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
