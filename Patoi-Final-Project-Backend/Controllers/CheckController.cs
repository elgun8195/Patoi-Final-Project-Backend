﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Patoi_Final_Project_Backend.DAL;
using Patoi_Final_Project_Backend.Models;
using Patoi_Final_Project_Backend.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Patoi_Final_Project_Backend.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin,Member")]

    public class CheckController : Controller
    {

        private AppDbContext _context;
        private UserManager<AppUser> _userManager;
        public CheckController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public async Task<IActionResult> Index()
        {

            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            OrderVM model = new OrderVM
            {
                
                Username = user.UserName,
                Email = user.Email,
                BasketItems = _context.BasketItems.Include(b => b.Product).ThenInclude(f => f.Campaign).Where(b => b.AppUserId == user.Id).ToList(),

            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(OrderVM orderVM)
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            OrderVM model = new OrderVM
            {
                
                Username = orderVM.Username,
                Email = orderVM.Email,
                BasketItems = _context.BasketItems.Include(b => b.Product).ThenInclude(f => f.Campaign).Where(b => b.AppUserId == user.Id).ToList()
            };
            if (!ModelState.IsValid) return View(model);

            TempData["Succeeded"] = false;

            if (model.BasketItems.Count == 0) return RedirectToAction("index", "home");
            Order order = new Order
            { 
                CompanyName = orderVM.CompanyName,
                Region = orderVM.Region,
                City=orderVM.City, 
                Apartment=orderVM.Apartment,
                PostCode=orderVM.PostCode,
                Phone = orderVM.Phone,
                BankTransfer = orderVM.BankTransfer,
                CheckPayments = orderVM.CheckPayments,
                CashOnDelivery = orderVM.CashOnDelivery,
                Paypal = orderVM.Paypal,
                Country = orderVM.Country, 
                Address = orderVM.Address,
                TotalPrice = 0,
                Date = DateTime.Now,
                AppUserId = user.Id
            };

            foreach (BasketItem item in model.BasketItems)
            {
                order.TotalPrice += item.Product.CampaignId == null ? item.Count * item.Product.Price : item.Count * item.Product.Price * (100 - item.Product.Campaign.DiscountPercent) / 100;
                OrderItem orderItem = new OrderItem
                {
                    Name = item.Product.Name,
                    Price = item.Product.CampaignId == null ? item.Count * item.Product.Price : item.Count * item.Product.Price * (100 - item.Product.Campaign.DiscountPercent) / 100,
                    AppUserId = user.Id,
                    ProduuctId = item.Product.Id,
                    Order = order
                };
                item.Product.Stock -= item.Count;
                if (item.Product.Stock==0)
                {
                    item.Product.OnSale =false ;
                }
                _context.OrderItem.Add(orderItem);
            }
            _context.BasketItems.RemoveRange(model.BasketItems);
            _context.Order.Add(order);
            _context.SaveChanges();
            TempData["Succeeded"] = true;

            return RedirectToAction("index", "home");
        }

    }
}
