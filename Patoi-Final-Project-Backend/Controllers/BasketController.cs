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

namespace Patoi_Final_Project_Backend.Controllers
{
    //[Authorize(Roles = "Admin,SuperAdmin,Member")]

    public class BasketController : Controller
    {
        private AppDbContext _context;
        private UserManager<AppUser> _userManager;
        public BasketController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        //#region

        //public async Task<IActionResult> AddBasket(int? id)
        //{
        //string username = User.Identity.Name;
        //    AppUser appUser = await _userManager.FindByNameAsync(username);
        //    if (id == null) return NotFound();
        //    Product dbProdudct = await _context.Products.FindAsync(id);

        //    if (dbProdudct == null) return NotFound();
        //    List<BasketProduct> products;

        //    string existBasket = Request.Cookies[$"{appUser.Id}"];

        //    if (existBasket == null)
        //    {
        //        products = new List<BasketProduct>();

        //    }
        //    else
        //    {
        //        products = JsonConvert.DeserializeObject<List<BasketProduct>>(Request.Cookies[$"{appUser.Id}"]);

        //    }
        //    BasketProduct existBasketproduct = products.FirstOrDefault(p => p.Id == dbProdudct.Id);
        //    if (existBasketproduct == null)
        //    {
        //        BasketProduct basketProduct = new BasketProduct();
        //        basketProduct.Id = dbProdudct.Id;
        //        basketProduct.Name = dbProdudct.Name;
        //        basketProduct.Count = 1;

        //        products.Add(basketProduct);
        //    }
        //    else
        //    {
        //        if (dbProdudct.Stock <= existBasketproduct.Count)
        //        {
        //            TempData["Fail"] = "Satisda bundan artiq yoxdur!";
        //            return RedirectToAction(nameof(Index));
        //        }
        //        else
        //        {
        //            existBasketproduct.Count++;
        //        }
        //    }



        //    Response.Cookies.Append($"{appUser.Id}", JsonConvert.SerializeObject(products), new CookieOptions { MaxAge = TimeSpan.FromMinutes(30) });

        //    return RedirectToAction(nameof(Index));

        //}


        //public async Task<IActionResult> Index()
        //{
        //    string username = User.Identity.Name;
        //    AppUser appUser = await _userManager.FindByNameAsync(username);
        //    List<BasketProduct> products = JsonConvert.DeserializeObject<List<BasketProduct>>(Request.Cookies[$"{appUser.Id}"]);

        //    List<BasketProduct> updatedProducts = new List<BasketProduct>();
        //    foreach (var item in products)
        //    {
        //        Product dbProduct = _context.Products.FirstOrDefault(p => p.Id == item.Id);
        //        BasketProduct basketProduct = new BasketProduct()
        //        {
        //            Id = dbProduct.Id,
        //            Price = dbProduct.Price,
        //            Name = dbProduct.Name,
        //            Image = dbProduct.Image,
        //            Count = item.Count
        //        };               
        //        updatedProducts.Add(basketProduct);
        //    }
        //    return View(updatedProducts);
        //}


        //public async Task<IActionResult> RemoveItem(int? id)
        //{
        //    string username = User.Identity.Name;
        //    AppUser appUser = await _userManager.FindByNameAsync(username);
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }
        //    string basket = Request.Cookies[$"{appUser.Id}"];
        //    List<BasketProduct> products = JsonConvert.DeserializeObject<List<BasketProduct>>(basket);

        //    BasketProduct existProduct = products.FirstOrDefault(p => p.Id == id);
        //    if (existProduct == null) return NotFound();
        //    products.Remove(existProduct);
        //    Response.Cookies.Append($"{appUser.Id}", JsonConvert.SerializeObject(products), new CookieOptions { MaxAge = TimeSpan.FromMinutes(60) });
        //    return RedirectToAction(nameof(Index));
        //}


        //public async Task<IActionResult> Plus(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }
        //    string username = User.Identity.Name;
        //    AppUser appUser = await _userManager.FindByNameAsync(username);
        //    string basket = Request.Cookies[$"{appUser.Id}"];
        //    List<BasketProduct> products = JsonConvert.DeserializeObject<List<BasketProduct>>(basket);

        //    BasketProduct existProduct = products.FirstOrDefault(p => p.Id == id);
        //    if (existProduct == null) return NotFound();

        //    Product dbproduct = _context.Products.FirstOrDefault(p => p.Id == id);

        //    if (dbproduct.Stock > existProduct.Count)
        //    {
        //        existProduct.Count++;
        //    }
        //    else
        //    {
        //        TempData["Fail"] = "Satisda bundan artiq yoxdur!";
        //        return RedirectToAction(nameof(Index));
        //    }


        //    Response.Cookies.Append($"{appUser.Id}", JsonConvert.SerializeObject(products), new CookieOptions { MaxAge = TimeSpan.FromMinutes(60) });
        //    return RedirectToAction(nameof(Index));
        //}


        //public async Task<IActionResult> Minus(int? id)
        //{
        //    string username = User.Identity.Name;
        //    AppUser appUser = await _userManager.FindByNameAsync(username);
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }
        //    string basket = Request.Cookies[$"{appUser.Id}"];
        //    List<BasketProduct> products = JsonConvert.DeserializeObject<List<BasketProduct>>(basket);

        //    BasketProduct existProduct = products.FirstOrDefault(p => p.Id == id);
        //    if (existProduct == null) return NotFound();


        //    if (existProduct.Count > 1)
        //    {
        //        existProduct.Count--;
        //    }
        //    else
        //    {
        //        RemoveItem(existProduct.Id);
        //        return RedirectToAction(nameof(Index));
        //    }

        //    Response.Cookies.Append($"{appUser.Id}", JsonConvert.SerializeObject(products), new CookieOptions { MaxAge = TimeSpan.FromMinutes(60) });
        //    return RedirectToAction(nameof(Index));
        //}

        //#endregion


        public IActionResult Index()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (currentUserId == null) return RedirectToAction("login", "account");

            Basket basket = _context.Baskets
                    .Include(b => b.BasketItems)
                    .FirstOrDefault(b => b.UserId == currentUserId);


            if (basket == null)
            {
                return RedirectToAction("index", "shop");
            }


            List<BasketItem> basketItems = _context.BasketItems.ToList();

            List<BasketProduct> products = new List<BasketProduct>();

            foreach (var item in basketItems)
            {
                Product product = _context.Products.Include(p => p.Image).FirstOrDefault(p => p.Id == item.ProductId);

                BasketProduct BasketProduct = new BasketProduct
                {
                    Id = item.ProductId,
                    Price = product.Price,
                    Name = product.Name,
                    BasketCount = item.Count,
                    Image = product.Image
                };
                products.Add(BasketProduct);

            }
            return View(products);
        }


        public IActionResult AddBasket(int? id, string ReturnUrl)
        {

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (currentUserId == null) return RedirectToAction("login", "account");
            Basket basket = _context.Baskets
                    .Include(b => b.BasketItems)
                    .FirstOrDefault(b => b.UserId == currentUserId);

            if (id == null) return NoContent();

            Product dbproduct = _context.Products.FirstOrDefault(x => x.Id == id);
            if (dbproduct == null) return NoContent();

            List<BasketItem> basketItems = _context.BasketItems.Where(b => b.BasketId == basket.Id).ToList();

            BasketItem isexsist = basketItems.Find(p => p.ProductId == id);



            if (isexsist == null)
            {
                BasketItem basketItem = new BasketItem();

                basketItem.ProductId = dbproduct.Id;
                basketItem.Count = 1;
                basketItem.BasketId = basket.Id;

                _context.Add(basketItem);
            }
            else
            {
                if (dbproduct.Stock < isexsist.Count) return RedirectToAction("index", "shop");
                isexsist.Count++;
            }

            _context.SaveChanges();
            if (ReturnUrl != null) return Redirect(ReturnUrl);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult RemoveItem(int id, string ReturnUrl)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null) return RedirectToAction("login", "account");

            Basket basket = _context.Baskets.FirstOrDefault(b => b.UserId == userId);

            List<BasketItem> basketItems = _context.BasketItems.Where(b => b.BasketId == basket.Id).ToList();

            BasketItem deleteItem = basketItems.FirstOrDefault(p => p.ProductId == id);

            _context.BasketItems.Remove(deleteItem);

            _context.SaveChanges();

            if (ReturnUrl != null) return Redirect(ReturnUrl);

            return RedirectToAction("index");
        }

        public IActionResult Plus(int id, string Returnurl)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null) return RedirectToAction("login", "account");

            Basket basket = _context.Baskets.FirstOrDefault(b => b.UserId == userId);

            List<BasketItem> basketItems = _context.BasketItems.Where(b => b.BasketId == basket.Id).ToList();

            BasketItem plusItem = basketItems.Find(p => p.ProductId == id);
            if (plusItem == null) return RedirectToAction("additem", new { id = id, Returnurl = Returnurl });

            var dbProduct = _context.Products.FirstOrDefault(p => p.Id == id);

            if (plusItem.Count < dbProduct.Stock)
            {
                _context.BasketItems.FirstOrDefault(b => b.Id == plusItem.Id).Count++;
                _context.SaveChanges();
            }
            if (Returnurl != null) return Redirect(Returnurl);

            return RedirectToAction("index", "basket");
        }

        public IActionResult Minus(int id, string Returnurl)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null) return RedirectToAction("login", "account");

            Basket basket = _context.Baskets.FirstOrDefault(b => b.UserId == userId);

            List<BasketItem> basketItems = _context.BasketItems.Where(b => b.BasketId == basket.Id).ToList();

            BasketItem decreaseItem = basketItems.FirstOrDefault(p => p.ProductId == id);

            if (decreaseItem == null) return RedirectToAction("additem", new { id = id, Returnurl = Returnurl });


            if (decreaseItem.Count > 1)
            {
                _context.BasketItems.FirstOrDefault(b => b.Id == decreaseItem.Id).Count--;

            }
            else
            {
                _context.BasketItems.Remove(decreaseItem);
            }
            _context.SaveChanges();


            if (Returnurl != null) return Redirect(Returnurl);
            return RedirectToAction("index", "basket");
        }

    }
}
