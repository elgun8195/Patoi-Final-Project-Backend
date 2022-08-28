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
     [Authorize(Roles = "Admin,SuperAdmin,Member")]
    #region
    public class BasketController : Controller
    {
        private AppDbContext _context;
        private UserManager<AppUser> _userManager;
        public BasketController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        #region

        public async Task<IActionResult> AddBasket(int? id)
        {
            string username = User.Identity.Name;
            AppUser appUser = await _userManager.FindByNameAsync(username);
            if (id == null) return NotFound();
            Product dbProdudct = await _context.Products.FindAsync(id);

            if (dbProdudct == null) return NotFound();
            List<BasketProduct> products;

            string existBasket = Request.Cookies[$"{appUser.Id}"];

            if (existBasket == null)
            {
                products = new List<BasketProduct>();

            }
            else
            {
                products = JsonConvert.DeserializeObject<List<BasketProduct>>(Request.Cookies[$"{appUser.Id}"]);

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



            Response.Cookies.Append($"{appUser.Id}", JsonConvert.SerializeObject(products), new CookieOptions { MaxAge = TimeSpan.FromDays(100) });

            return RedirectToAction(nameof(Index));

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
                    Image = dbProduct.Image,
                    Count = item.Count
                };
                updatedProducts.Add(basketProduct);
            }
            return View(updatedProducts);
        }


        public async Task<IActionResult> RemoveItem(int? id)
        {
            string username = User.Identity.Name;
            AppUser appUser = await _userManager.FindByNameAsync(username);
            if (id == null)
            {
                return NotFound();
            }
            string basket = Request.Cookies[$"{appUser.Id}"];
            List<BasketProduct> products = JsonConvert.DeserializeObject<List<BasketProduct>>(basket);

            BasketProduct existProduct = products.FirstOrDefault(p => p.Id == id);
            if (existProduct == null) return NotFound();
            products.Remove(existProduct);
            Response.Cookies.Append($"{appUser.Id}", JsonConvert.SerializeObject(products), new CookieOptions { MaxAge = TimeSpan.FromMinutes(60) });
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Plus(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            string username = User.Identity.Name;
            AppUser appUser = await _userManager.FindByNameAsync(username);
            string basket = Request.Cookies[$"{appUser.Id}"];
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
                return RedirectToAction(nameof(Index));
            }


            Response.Cookies.Append($"{appUser.Id}", JsonConvert.SerializeObject(products), new CookieOptions { MaxAge = TimeSpan.FromMinutes(60) });
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Minus(int? id)
        {
            string username = User.Identity.Name;
            AppUser appUser = await _userManager.FindByNameAsync(username);
            if (id == null)
            {
                return NotFound();
            }
            string basket = Request.Cookies[$"{appUser.Id}"];
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
                return RedirectToAction(nameof(Index));
            }

            Response.Cookies.Append($"{appUser.Id}", JsonConvert.SerializeObject(products), new CookieOptions { MaxAge = TimeSpan.FromMinutes(60) });
            return RedirectToAction(nameof(Index));
        }

        #endregion
    }
    #endregion

    #region fake
    //public class BasketController : Controller
    //{
    //    private readonly AppDbContext _context;
    //    private readonly SignInManager<AppUser> _signInManager;
    //    private readonly UserManager<AppUser> _usermanager;

    //    public BasketController(AppDbContext context,
    //    SignInManager<AppUser> signInManager,
    //    UserManager<AppUser> userManager)
    //    {
    //        _context = context;
    //        _signInManager = signInManager;
    //        _usermanager = userManager;
    //    }


    //    [Authorize]
    //    public async Task<IActionResult> AddBasket(int? id, string returnurl)
    //    {
    //        string username = "";
    //        bool online = false;
    //        if (!User.Identity.IsAuthenticated)
    //        {
    //            return RedirectToAction("login", "account");
    //            online = false;
    //        }
    //        else
    //        {
    //            username = User.Identity.Name;
    //            online = true;
    //        }
    //        if (id == null)
    //            if (id == null) return NotFound();
    //        Product dbProduct = await _context.Products.FindAsync(id);
    //        if (dbProduct == null) return NotFound();
    //        List<BasketProduct> products;
    //        if (Request.Cookies[$"basket{username}"] == null)
    //        {
    //            products = new List<BasketProduct>();
    //        }
    //        else
    //        {
    //            products = JsonConvert.DeserializeObject<List<BasketProduct>>(Request.Cookies[$"basket{username}"]);
    //        }
    //        BasketProduct IsExist = products.Find(x => x.Id == id);
    //        if (IsExist == null)
    //        {
    //            BasketProduct BasketProduct = new BasketProduct
    //            {
    //                Id = dbProduct.Id,
    //                ProductCount = 1,
    //            };

    //            products.Add(BasketProduct);
    //        }
    //        else
    //        {
    //            IsExist.ProductCount++;
    //        }

    //        Response.Cookies.Append($"basket{username}", JsonConvert.SerializeObject(products), new CookieOptions { MaxAge = TimeSpan.FromDays(100) });
    //        double price = 0;
    //        double count = 0;

    //        foreach (var product in products)
    //        {
    //            //price += product.Price * product.ProductCount;
    //            count += product.ProductCount;
    //        }

    //        var obj = new
    //        {
    //            Price = price,
    //            Count = count,
    //            online = online
    //        };
    //        //obj data-id ile baghlidir. response "obj" obyektidir,
    //        //Ok'in icnde return edilmelidir ki API'de response gorsun
    //        if (returnurl != null)
    //        {
    //            return Redirect(returnurl);
    //        }
    //        return RedirectToAction("index", "home");
    //    }

    //    public IActionResult Index()
    //    {
    //        string username = "";
    //        if (!User.Identity.IsAuthenticated)
    //        {
    //            return RedirectToAction("login", "account");
    //        }
    //        else
    //        {
    //            username = User.Identity.Name;
    //        }
    //        string basket = Request.Cookies[$"basket{username}"];
    //        List<BasketProduct> products;
    //        if (basket != null)
    //        {
    //            products = JsonConvert.DeserializeObject<List<BasketProduct>>(basket);
    //            foreach (var item in products)
    //            {
    //                Product dbProducts = _context.Products.FirstOrDefault(x => x.Id == item.Id);
    //                item.Name = dbProducts.Name;
    //                item.ImageUrl = dbProducts.Image;
    //                item.Price = dbProducts.Price;

    //            }
    //        }
    //        else
    //        {
    //            products = new List<BasketProduct>();
    //        }
    //        return View(products);
    //    }

    //    public IActionResult RemoveItem(int? id, string returnurl)
    //    {
    //        string username = "";
    //        if (!User.Identity.IsAuthenticated)
    //        {
    //            return RedirectToAction("login", "account");
    //        }
    //        else
    //        {
    //            username = User.Identity.Name;
    //        }
    //        if (id == null) return NotFound();
    //        string basket = Request.Cookies[$"basket{username}"];
    //        List<BasketProduct> products = JsonConvert.DeserializeObject<List<BasketProduct>>(basket);
    //        BasketProduct dbProduct = products.Find(p => p.Id == id);
    //        if (dbProduct == null) return NotFound();


    //        products.Remove(dbProduct);
    //        Response.Cookies.Append($"basket{username}", JsonConvert.SerializeObject(products), new CookieOptions { MaxAge = TimeSpan.FromDays(100) });

    //        double subtotal = 0;
    //        int basketCount = 0;

    //        if (products.Count > 0)
    //        {
    //            foreach (BasketProduct item in products)
    //            {
    //                //subtotal += item.Price * dbProduct.ProductCount;
    //                basketCount += item.ProductCount;
    //            }
    //        }

    //        var obj = new
    //        {
    //            Price = $"(${subtotal})",
    //            Count = basketCount
    //        };
    //        if (returnurl != null)
    //        {
    //            return Redirect(returnurl);
    //        }
    //        return RedirectToAction(nameof(Index));
    //    }




    //    public IActionResult Minus(int? id)
    //    {
    //        string username = "";
    //        if (!User.Identity.IsAuthenticated)
    //        {
    //            return RedirectToAction("login", "account");
    //        }
    //        else
    //        {
    //            username = User.Identity.Name;
    //        }
    //        if (id == null) return NotFound();
    //        string basket = Request.Cookies[$"basket{username}"];
    //        List<BasketProduct> products = JsonConvert.DeserializeObject<List<BasketProduct>>(basket);
    //        BasketProduct dbproducts = products.Find(p => p.Id == id);
    //        if (dbproducts == null) return NotFound();


    //        double subtotal = 0;
    //        int basketCount = 0;

    //        if (dbproducts.ProductCount > 1)
    //        {
    //            dbproducts.ProductCount--;
    //            Response.Cookies.Append($"basket{username}", JsonConvert.SerializeObject(dbproducts));
    //        }
    //        else
    //        {
    //            products.Remove(dbproducts);


    //            List<BasketProduct> productsNew = products.FindAll(p => p.Id != id);

    //            Response.Cookies.Append($"basket{username}", JsonConvert.SerializeObject(productsNew));

    //            foreach (BasketProduct pr in productsNew)
    //            {
    //                //subtotal += pr.Price * pr.ProductCount;
    //                basketCount += pr.ProductCount;
    //            }

    //            var obje = new
    //            {
    //                count = 0,
    //                price = subtotal,
    //                main = basketCount
    //            };

    //            return RedirectToAction(nameof(Index));
    //        }
    //        Response.Cookies.Append($"basket{username}", JsonConvert.SerializeObject(products), new CookieOptions
    //        {
    //            MaxAge = TimeSpan.FromDays(100)
    //        });


    //        foreach (var product in products)
    //        {
    //            //subtotal += product.Price * product.ProductCount;
    //            basketCount += product.ProductCount;
    //        }

    //        var obj = new
    //        {
    //            Price = subtotal,
    //            Count = dbproducts.ProductCount,
    //            main = basketCount,
    //            itemTotal = dbproducts.Price * dbproducts.ProductCount
    //        };
    //        return RedirectToAction(nameof(Index));
    //    }



    //    public IActionResult Plus(int? id)
    //    {
    //        string username = "";
    //        if (!User.Identity.IsAuthenticated)
    //        {
    //            return RedirectToAction("login", "account");
    //        }
    //        else
    //        {
    //            username = User.Identity.Name;
    //        }
    //        if (id == null) return NotFound();
    //        string basket = Request.Cookies[$"basket{username}"];
    //        List<BasketProduct> products;
    //        products = JsonConvert.DeserializeObject<List<BasketProduct>>(basket);
    //        BasketProduct dbproducts = products.Find(p => p.Id == id);
    //        if (dbproducts == null) return NotFound();
    //        dbproducts.ProductCount++;
    //        Response.Cookies.Append($"basket{username}", JsonConvert.SerializeObject(products), new CookieOptions
    //        {
    //            MaxAge = TimeSpan.FromDays(100)
    //        });

    //        double price = 0;
    //        double count = 0;

    //        foreach (var product in products)
    //        {
    //            //price += product.Price * product.ProductCount;
    //            count += product.ProductCount;
    //        }
    //        var obj = new
    //        {
    //            Price = price,
    //            Count = count,
    //            main = dbproducts.ProductCount,
    //            itemTotal = dbproducts.Price * dbproducts.ProductCount
    //        };

    //        return RedirectToAction(nameof(Index));
    //    }


    //}
    #endregion
}
