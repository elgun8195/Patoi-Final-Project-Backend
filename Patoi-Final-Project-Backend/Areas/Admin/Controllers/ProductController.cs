using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Patoi_Final_Project_Backend.DAL;
using Patoi_Final_Project_Backend.Extensions;
using Patoi_Final_Project_Backend.Helpers;
using Patoi_Final_Project_Backend.Models;
using Patoi_Final_Project_Backend.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Patoi_Final_Project_Backend.Areas.Admin.Controllers
{
    [Area("Admin")]
   // [Authorize(Roles = "Admin,SuperAdmin")]
    public class ProductController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public ProductController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }


        public IActionResult Index(int take = 5, int pagesize = 1)
        {
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Tags = _context.Tags.ToList();
            List<Product> products = _context.Products.Include(p => p.ProductCategories).ThenInclude(pc => pc.Category).Skip((pagesize - 1) * take).Take(take).ToList();
            Pagination<Product> pagination = new Pagination<Product>(

               ReturnPageCount(take),pagesize,products);
            return View(pagination);
        }
        private int ReturnPageCount(int take)
        {
            int product = _context.Products.Count();
            return (int)Math.Ceiling(((decimal)product / take));
        }
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            Product product = _context.Products.Include(p => p.ProductCategories).ThenInclude(pc => pc.Category).FirstOrDefault(p => p.Id == id);

            Helpers.Helper.DeleteImage(_env, "images/products", product.Image);

            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Create()
        {
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Tags = _context.Tags.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Tags = _context.Tags.ToList();

            if (!ModelState.IsValid) return View();
            if (product.Photo != null)
            {
                if (!product.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Sekil formati duzgun deyil");
                    return View();
                }
                if (!product.Photo.CheckSize(5))
                {
                    ModelState.AddModelError("Photo", "Max 5mb ola biler");
                    return View();
                }
                product.Image = await product.Photo.SaveImage(_env, "images/products");
            }
            else
            {
                ModelState.AddModelError("Photo", "Sekil elave edin");
                return View();
            }

            product.ProductCategories = new List<ProductCategories>();
            if (product.CategoryIds != null)
            {
                foreach (var categoryId in product.CategoryIds)
                {
                    ProductCategories pCategory = new ProductCategories
                    {
                        Product = product,
                        CategoryId = categoryId
                    };
                    _context.ProductCategories.Add(pCategory);
                }
            }

            _context.Products.Add(product);
            _context.SaveChanges();
            List<AppUser> subscribes =new List<AppUser>();

            foreach (var sub in subscribes)
            {
                string link = "https://localhost:44352/shop/detail/" + product.Id + $"?categoryId={product.ProductCategories.FirstOrDefault().CategoryId}";
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("qolaelo@gmail.com", "Booky");
                mail.To.Add(new MailAddress(sub.Email));


                mail.Subject = "New book";
                string body = string.Empty;

                using (StreamReader reader = new StreamReader("wwwroot/Assets/Template/NewSubscribe.html"))
                {
                    body = reader.ReadToEnd();
                }

                string about = $"<strong>Hello</strong><br /> a new <strong>{product.Name} {product.Name}</strong> book added to our shop <br/>click the link down below to discover new book";
                body = body.Replace("{{link}}", link);
                mail.Body = body.Replace("{{about}}", about);
                mail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;

                smtp.Credentials = new NetworkCredential("qolaelo@gmail.com", "olkdjlioakxrczvx");
                smtp.Send(mail);
            } 
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {

            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Tags = _context.Tags.ToList();

            Product product = _context.Products.Include(p => p.ProductCategories).ThenInclude(pc => pc.Category).FirstOrDefault(p => p.Id == id);
            if (product == null) return RedirectToAction("Index");
            return View(product);
        }
        [HttpPost]

        public async Task<IActionResult> Edit(int? id, Product product)
        {
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Tags = _context.Tags.ToList();
            if (id == null) { return NotFound(); }
            Product existProduct = _context.Products.Include(p => p.ProductCategories).ThenInclude(pc => pc.Category).FirstOrDefault(p => p.Id == id);

            if (existProduct == null) return RedirectToAction("Index");

            if (product.Photo != null)
            {
                if (!product.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Sekil duzgun formatda deyil");
                    return View();
                }
                if (!product.Photo.CheckSize(5))
                {
                    ModelState.AddModelError("Photo", "Sekil max 5 mb ola biler");
                    return View();
                }
                Helpers.Helper.DeleteImage(_env, "images/products", existProduct.Image);
                existProduct.Image = await product.Photo.SaveImage(_env, "images/products");
            }

            if (!ModelState.IsValid)
            {
                return View(existProduct);
            }

            var existCategory = _context.ProductCategories.Where(pc => pc.ProductId == product.Id).ToList();
            if (product.CategoryIds != null)
            {
                foreach (var categoryId in product.CategoryIds)
                {
                    var excategory = existCategory.FirstOrDefault(c => c.CategoryId == categoryId);
                    if (excategory != null)
                    {
                        ProductCategories pc = new ProductCategories
                        {
                            ProductId = product.Id,
                            CategoryId = categoryId,
                        };
                        _context.ProductCategories.Add(pc);
                    }
                    else
                    {
                        existCategory.Remove(excategory);
                    }

                }
            }
            _context.ProductCategories.RemoveRange(existCategory);

            existProduct.Name = product.Name;
            existProduct.Price = product.Price;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Product Product = _context.Products.Find(id);
            if (Product == null)
            {
                return NotFound();
            }
            return View(Product);

        }
    }
}
