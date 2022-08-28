using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Patoi_Final_Project_Backend.DAL;
using Patoi_Final_Project_Backend.Models;
using Patoi_Final_Project_Backend.ViewModels;
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
            string username = User.Identity.Name;
            AppUser appUser = await _userManager.FindByNameAsync(username);
            List<BasketProduct> products = JsonConvert.DeserializeObject<List<BasketProduct>>(Request.Cookies[$"{appUser.Id}"]);

            List<BasketProduct> updatedProducts = new List<BasketProduct>();

            foreach (var item in products)
            {
                Product dbProduct = _context.Products.FirstOrDefault(p => p.Id == item.Id);
                BasketProduct basketProduct = new BasketProduct()
                {
                    Id = dbProduct.Id,
                    Price = dbProduct.Price,
                    Name = dbProduct.Name,
                    //Image = dbProduct.Image,
                    //Count = item.Count

                };


                updatedProducts.Add(basketProduct);

                //ViewBag.Total = dbProduct.Price * basketProduct.Count;
            }
            return View(updatedProducts);
        }

    }
}
