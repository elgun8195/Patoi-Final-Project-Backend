﻿using Patoi_Final_Project_Backend.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Patoi_Final_Project_Backend.ViewModels
{
    public class OrderVM
    {
        //[Required]
        //[StringLength(maximumLength: 30)]
        //public string Name { get; set; }
        //[Required]
        //[StringLength(maximumLength: 30)]
        //public string Surname { get; set; }
        [Required]
        [StringLength(maximumLength: 25)]
        public string Username { get; set; }
        [Required]
        [StringLength(maximumLength: 50)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [StringLength(maximumLength: 150)]
        public string CompanyName { get; set; }
        [Required]
        [StringLength(maximumLength: 40)]
        public string Region { get; set; }
        [Required]
        [StringLength(maximumLength: 40)]
        public string Country { get; set; }
        [Required]
        [StringLength(maximumLength: 30)]
        public string City { get; set; }
        [Required]
        [StringLength(maximumLength: 30)]
        public string StreetAddress { get; set; }
        [Required]
        [StringLength(maximumLength: 30)]
        public string Apartment { get; set; }
        [Required]
        [StringLength(maximumLength: 30)]
        public string PostCode { get; set; }
        [Required]
        [StringLength(maximumLength: 30)]
        public string Phone { get; set; }
        [Required]
        [StringLength(maximumLength: 30)]
        public bool BankTransfer { get; set; }
        [Required]
        [StringLength(maximumLength: 30)]
        public bool CheckPayments { get; set; }
        [Required]
        [StringLength(maximumLength: 30)]
        public bool Paypal { get; set; }
        [Required]
        [StringLength(maximumLength: 30)]
        public bool CashOnDelivery { get; set; }
        public List<BasketItem> BasketItems { get; set; }
    }
}
