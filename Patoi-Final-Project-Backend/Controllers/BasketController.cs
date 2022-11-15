using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Patoi_Final_Project_Backend.DAL;
using Patoi_Final_Project_Backend.Models;
using Patoi_Final_Project_Backend.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Net.Http;

namespace Patoi_Final_Project_Backend.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin,Member")]

    public class BasketController : Controller
    {
        private AppDbContext _context;
        private UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContext;
        public BasketController(AppDbContext context, IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager)
        {
            _context = context;
            _httpContext = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);

            OrderVM model = new OrderVM
            {
                Username = user.UserName,
                Email = user.Email,
                BasketItems = _context.BasketItems.Include(b => b.Product).ThenInclude(f => f.Campaign).Include(b => b.Product).Where(b => b.AppUserId == user.Id).ToList(),

            };


            return View(model);

        }


        public async Task<IActionResult> AddBasket(int id)
        {
            Product product = _context.Products.Include(f => f.Campaign).FirstOrDefault(f => f.Id == id);


            if (User.Identity.IsAuthenticated)
            {
                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);

                BasketItem basketItem = _context.BasketItems.FirstOrDefault(b => b.ProductId == product.Id && b.AppUserId == user.Id);
                if (basketItem == null)
                {
                    basketItem = new BasketItem
                    {
                        AppUserId = user.Id,
                        ProductId = product.Id,
                        Count = 1
                    };
                    _context.BasketItems.Add(basketItem);
                }
                else
                {
                    basketItem.Count++;
                }
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));

                //return View("_basketPartial");
            }



            return RedirectToAction("account", "login");
        }


        public async Task<IActionResult> Plus(int Id)
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            BasketItem basket = _context.BasketItems.Include(b => b.Product).ThenInclude(b => b.Campaign).FirstOrDefault(b => b.ProductId == Id && b.AppUserId == user.Id);
            if (basket.Count < basket.Product.Stock)
            {
                basket.Count++;

                _context.SaveChanges();
                double TotalPrice = 0;
                double Price = basket.Count * (basket.Product.CampaignId == null ? basket.Product.Price : basket.Product.Price * (100 - basket.Product.Campaign.DiscountPercent) / 100);
                List<BasketItem> basketItems = _context.BasketItems.Include(b => b.AppUser).Include(b => b.Product).Where(b => b.AppUserId == user.Id).ToList();
                foreach (BasketItem item in basketItems)
                {
                    Product product = _context.Products.Include(b => b.Campaign).FirstOrDefault(b => b.Id == item.ProductId);

                    BasketItemVM basketItemVM = new BasketItemVM
                    {
                        Product = product,
                        Count = item.Count
                    };
                    basketItemVM.Price = product.CampaignId == null ? product.Price : product.Price * (100 - product.Campaign.DiscountPercent) / 100;

                    TotalPrice += basketItemVM.Price * basketItemVM.Count;

                }

                return Json(new { totalPrice = TotalPrice, Price });
            }
            else
            {
                TempData["Succeeded"] = false;

            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Minus(int Id)
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            BasketItem basket = _context.BasketItems.Include(b => b.Product).ThenInclude(b => b.Campaign).FirstOrDefault(b => b.ProductId == Id && b.AppUserId == user.Id);
            if (basket.Count == 1)
            {
                basket.Count = 1;
            }
            else
            {
                basket.Count--;
            }
            _context.SaveChanges();
            double TotalPrice = 0;
            double Price = basket.Count * (basket.Product.CampaignId == null ? basket.Product.Price : basket.Product.Price * (100 - basket.Product.Campaign.DiscountPercent) / 100);
            List<BasketItem> basketItems = _context.BasketItems.Include(b => b.AppUser).Include(b => b.Product).Where(b => b.AppUserId == user.Id).ToList();
            foreach (BasketItem item in basketItems)
            {
                Product product = _context.Products.Include(b => b.Campaign).FirstOrDefault(b => b.Id == item.ProductId);

                BasketItemVM basketItemVM = new BasketItemVM
                {
                    Product = product,
                    Count = item.Count
                };
                basketItemVM.Price = product.CampaignId == null ? product.Price : product.Price * (100 - product.Campaign.DiscountPercent) / 100;

                TotalPrice += basketItemVM.Price * basketItemVM.Count;

            }

            return Json(new { totalPrice = TotalPrice, Price });
        }

        public async Task<IActionResult> removecartitem(int Id)
        {
            if (User.Identity.IsAuthenticated)
            {
                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);

                List<BasketItem> basketItems = _context.BasketItems.Where(b => b.ProductId == Id && b.AppUserId == user.Id).ToList();
                if (basketItems == null) return Json(new { status = 404 });
                foreach (var item in basketItems)
                {

                    _context.BasketItems.Remove(item);
                }
            }

            _context.SaveChanges();


            return Json(new { status = 200 });
        }

    }
}
