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

namespace Patoi_Final_Project_Backend.Controllers
{
    public class WishController : Controller
    {
        private AppDbContext _context;

        public WishController(AppDbContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> AddWish(int? id)
        {
            if (id == null) return NotFound();
            Product dbProdudct = await _context.Products.FindAsync(id);

            if (dbProdudct == null) return NotFound();
            List<WishProduct> products;

            string existBasket = Request.Cookies["wish"];

            if (existBasket == null)
            {
                products = new List<WishProduct>();

            }
            else
            {
                products = JsonConvert.DeserializeObject<List<WishProduct>>(Request.Cookies["wish"]);

            }
            WishProduct existWishProduct = products.FirstOrDefault(p => p.Id == dbProdudct.Id);
            if (existWishProduct == null)
            {
                WishProduct WishProduct = new WishProduct();

                WishProduct.Id = dbProdudct.Id;
                WishProduct.Name = dbProdudct.Name;
                WishProduct.OnSale = dbProdudct.OnSale;
                WishProduct.Count = 1;

                products.Add(WishProduct);
            }
            else
            {
                if (dbProdudct.Stock <= existWishProduct.Count)
                {
                    TempData["Fail"] = "Satisda bundan artiq yoxdur!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    existWishProduct.Count++;
                }
            }



            Response.Cookies.Append("wish", JsonConvert.SerializeObject(products), new CookieOptions { MaxAge = TimeSpan.FromMinutes(60) });

            return RedirectToAction(nameof(Index));

        }

        public IActionResult Index()
        {
            List<WishProduct> products = JsonConvert.DeserializeObject<List<WishProduct>>(Request.Cookies["wish"]);

            List<WishProduct> updatedProducts = new List<WishProduct>();

            foreach (var item in products)
            {
                Product dbProduct = _context.Products.FirstOrDefault(p => p.Id == item.Id);
                WishProduct WishProduct = new WishProduct()
                {
                    Id = dbProduct.Id,
                    Price = dbProduct.Price,
                    Name = dbProduct.Name,
                    Image = dbProduct.Image,
                    OnSale = dbProduct.OnSale,
                    Count = item.Count

                };


                updatedProducts.Add(WishProduct);

              
            }
            return View(updatedProducts);
        }



        public IActionResult RemoveItem(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            string wish = Request.Cookies["wish"];
            List<WishProduct> products = JsonConvert.DeserializeObject<List<WishProduct>>(wish);

            WishProduct existProduct = products.FirstOrDefault(p => p.Id == id);
            if (existProduct == null) return NotFound();
            products.Remove(existProduct);
            Response.Cookies.Append("wish", JsonConvert.SerializeObject(products), new CookieOptions { MaxAge = TimeSpan.FromMinutes(60) });
            return RedirectToAction(nameof(Index));
        }


    }
}
