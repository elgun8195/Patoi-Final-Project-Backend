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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;

namespace Patoi_Final_Project_Backend.Controllers
{
    //[Authorize(Roles = "Admin,SuperAdmin,Member")]

    public class WishController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public WishController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (User.Identity.IsAuthenticated)
            {
            List<WishListItem> WishListItems = _context.WishListItems.Include(b => b.Product).Where(b => b.AppUserId == user.Id).ToList();
          
            return View(WishListItems);
            }
            else
            {
                return RedirectToAction("login","account");
            }



        }
 

        public async Task<IActionResult> AddWish(int id)
        {
            Product product = _context.Products.Include(p => p.Campaign).FirstOrDefault(p => p.Id == id);

            if (User.Identity.IsAuthenticated)
            {
                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);

                WishListItem wishlistItem = _context.WishListItems.FirstOrDefault(b => b.ProductId == product.Id && b.AppUserId == user.Id);
                if (wishlistItem == null)
                {
                    wishlistItem = new WishListItem
                    {
                        AppUserId = user.Id,
                        ProductId = product.Id,
                        Count = 1,
                    };
                    _context.WishListItems.Add(wishlistItem);
                }
                else
                {
                    wishlistItem.Count = 1;
                }
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction("login", "account");
            }
        }



        public async Task<IActionResult> RemoveItem(int id)
        {

            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            List<WishListItem> wishlistItems = _context.WishListItems.Where(b => b.ProductId == id && b.AppUserId == user.Id).ToList();
            foreach (var item in wishlistItems)
            {
                _context.WishListItems.Remove(item);
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
