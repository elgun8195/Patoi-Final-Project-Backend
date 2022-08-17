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
    public class BasketController : Controller
    {
        private AppDbContext _context;

        public BasketController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> AddBasket(int? id)
        {
            if (id == null) return NotFound();
            Product dbProdudct = await _context.Products.FindAsync(id);

            if (dbProdudct == null) return NotFound();
            List<BasketProduct> products;

            string existBasket = Request.Cookies["basket"];

            if (existBasket == null)
            {
                products = new List<BasketProduct>();

            }
            else
            {
                products = JsonConvert.DeserializeObject<List<BasketProduct>>(Request.Cookies["basket"]);

            }
            BasketProduct existBasketproduct = products.FirstOrDefault(p => p.Id == dbProdudct.Id);
            if (existBasketproduct == null)
            {
                BasketProduct basketProduct = new BasketProduct();

                basketProduct.Id = dbProdudct.Id;
                basketProduct.Name = dbProdudct.Name;
                basketProduct.Count = 1;

                products.Add(basketProduct);
            }
            else
            {
                if (dbProdudct.Stock <= existBasketproduct.Count)
                {
                    TempData["Fail"] = "Satisda bundan artiq yoxdur!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    existBasketproduct.Count++;
                }
            }



            Response.Cookies.Append("basket", JsonConvert.SerializeObject(products), new CookieOptions { MaxAge = TimeSpan.FromMinutes(30) });

            return RedirectToAction("Index", "Shop");

        }


        public IActionResult Basket()
        {
            List<BasketProduct> products = JsonConvert.DeserializeObject<List<BasketProduct>>(Request.Cookies["basket"]);

            List<BasketProduct> updatedProducts = new List<BasketProduct>();

            foreach (var item in products)
            {
                Product dbProduct = _context.Products.FirstOrDefault(p => p.Id == item.Id);
                BasketProduct basketProduct = new BasketProduct()
                {
                    Id = dbProduct.Id,
                    Price = dbProduct.Price,
                    Name = dbProduct.Name,
                    Image = dbProduct.Image,
                    Count = item.Count

                };

               
                updatedProducts.Add(basketProduct);

            ViewBag.Total =dbProduct.Price*basketProduct.Count ;
            }
            return View(updatedProducts);
        }


        public IActionResult RemoveItem(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            string basket = Request.Cookies["basket"];
            List<BasketProduct> products = JsonConvert.DeserializeObject<List<BasketProduct>>(basket);

            BasketProduct existProduct = products.FirstOrDefault(p => p.Id == id);
            if (existProduct == null) return NotFound();
            products.Remove(existProduct);
            Response.Cookies.Append("basket", JsonConvert.SerializeObject(products), new CookieOptions { MaxAge = TimeSpan.FromMinutes(60) });
            return RedirectToAction(nameof(basket));
        }


        public IActionResult Plus(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            string basket = Request.Cookies["basket"];
            List<BasketProduct> products = JsonConvert.DeserializeObject<List<BasketProduct>>(basket);

            BasketProduct existProduct = products.FirstOrDefault(p => p.Id == id);
            if (existProduct == null) return NotFound();

            Product dbproduct = _context.Products.FirstOrDefault(p => p.Id == id);

            if (dbproduct.Stock > existProduct.Count)
            {
                existProduct.Count++;
            }
            else
            {
                TempData["Fail"] = "Satisda bundan artiq yoxdur!";
                return RedirectToAction("basket", "basket");
            }


            Response.Cookies.Append("basket", JsonConvert.SerializeObject(products), new CookieOptions { MaxAge = TimeSpan.FromMinutes(60) });
            return RedirectToAction(nameof(basket));
        }


        public IActionResult Minus(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            string basket = Request.Cookies["basket"];
            List<BasketProduct> products = JsonConvert.DeserializeObject<List<BasketProduct>>(basket);

            BasketProduct existProduct = products.FirstOrDefault(p => p.Id == id);
            if (existProduct == null) return NotFound();


            if (existProduct.Count > 1)
            {
                existProduct.Count--;
            }
            else
            {
                RemoveItem(existProduct.Id);
                return RedirectToAction(nameof(basket));
            }

            Response.Cookies.Append("basket", JsonConvert.SerializeObject(products), new CookieOptions { MaxAge = TimeSpan.FromMinutes(60) });
            return RedirectToAction(nameof(basket));
        }
    }
}
