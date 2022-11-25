using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            List<Product> products = _context.Products.Include(p => p.ProductCategories).ThenInclude(pc => pc.Category).Include(p => p.ProductImages).Where(p=>p.IsDeleted==false).Skip((pagesize - 1) * take).Take(take).ToList();
            Pagination<Product> pagination = new Pagination<Product>(

               ReturnPageCount(take), pagesize, products);
            return View(pagination);
        }
        private int ReturnPageCount(int take)
        {
            int product = _context.Products.Count();
            return (int)Math.Ceiling(((decimal)product / take));
        }
        public async Task<IActionResult> Delete(int? id)
        {
           
            if (id == null) return NotFound();
            Product product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();
            product.IsDeleted = true; 
            await _context.SaveChangesAsync();
            return RedirectToAction("index");
        }
       


        public IActionResult Edit(int id)
        {

            var altTags = _context.Tags.Where(c => c.ParentId != null).Where(p => p.IsDeleted != true).ToList();
            //  ViewBag.Brands = new SelectList(_context.Brands.ToList(), "Id", "Name");
            ViewBag.Tags = new SelectList(_context.Tags.Where(c => c.IsDeleted != true).Where(c => c.ParentId == null).ToList(), "Id", "Name");

            ViewBag.altTags = new SelectList((altTags).ToList(), "Id", "Name");
            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "Id", "Name");

            Product product = _context.Products.Include(p => p.ProductCategories).ThenInclude(pc => pc.Category).Where(p => p.IsDeleted == false).FirstOrDefault(p => p.Id == id);
            if (product == null) return RedirectToAction("Index");
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
     
        public async Task<IActionResult> Edit(int? id, Product product)
        {
            var altTags = _context.Tags.Where(c => c.ParentId != null).Where(p => p.IsDeleted != true).ToList();
            //  ViewBag.Brands = new SelectList(_context.Brands.ToList(), "Id", "Name");
            ViewBag.Tags = new SelectList(_context.Tags.Where(c => c.IsDeleted != true).Where(c => c.ParentId == null).ToList(), "Id", "Name");

            ViewBag.altTags = new SelectList((altTags).ToList(), "Id", "Name");
            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "Id", "Name");

            if (!ModelState.IsValid)
            {
                return View();
            }
            Product dbProduct = await _context.Products
                .Include(p => p.ProductImages)
                .Include(p => p.ProductCategories)
                .ThenInclude(t => t.Category)
                .Include(c => c.Tag)
                .Where(c => c.IsDeleted != true)
                .FirstOrDefaultAsync(b => b.Id == product.Id);
            if (dbProduct == null)
            {
                return View();
            }
            List<ProductImage> images = new List<ProductImage>();
            string path = "";
            if (product.Photo == null)
            {
                foreach (var item in dbProduct.ProductImages)
                {
                    item.ImageUrl = item.ImageUrl;
                    _context.Add(item);
                }
            }
            else
            {
                foreach (var item in product.Photo)
                {
                    if (item == null)
                    {
                        ModelState.AddModelError("Photo", "Can not be empty");
                        return View();
                    }
                    if (!item.IsImage())
                    {
                        ModelState.AddModelError("Photo", "Only images");
                        return View();
                    }

                    if (item.CheckSize(20000))
                    {
                        ModelState.AddModelError("Photo", "The image size is larger than required size(max 20 mb)");
                        return View();
                    }
                    ProductImage image = new ProductImage();
                    image.ImageUrl = item.Savemage(_env, "images/products/p");

                    if (product.Photo.Count == 1)
                    {
                        image.IsMain = true;
                    }
                    else
                    {
                        for (int i = 0; i < images.Count; i++)
                        {
                            images[0].IsMain = true;
                        }
                    }
                    images.Add(image);
                }

                foreach (var item in product.Photo)
                {
                    if (!item.IsImage())
                    {
                        ModelState.AddModelError("Photo", "Images only");
                        return View();
                    }

                    if (item.CheckSize(20000))
                    {
                        ModelState.AddModelError("Photo", "The image size is larger than required size(max 20 mb)");
                        return View();
                    }
                }
            }

            foreach (var item in dbProduct.ProductImages)
            {
                if (item.ImageUrl != null)
                {
                    path = Path.Combine(_env.WebRootPath, "images/products/p", item.ImageUrl);
                }
            }
            if (path != null)
            {
                Helper.Deletemage(path);
            }
            else return NotFound();

            if (product.TagId == null)
            {
                foreach (var item1 in dbProduct.ProductCategories)
                {
                    item1.CategoryId = item1.CategoryId;
                }
            }
            else
            {
                List<ProductCategories> productTags = new List<ProductCategories>();
                foreach (int item in product.CategoryId)
                {
                    ProductCategories productTag = new ProductCategories();
                    productTag.CategoryId = item;
                    productTag.ProductId = dbProduct.Id;
                    productTags.Add(productTag);
                }
                dbProduct.ProductCategories = productTags;
            }
            if (product.Tag == null && product.Tag == null)
            {
                dbProduct.CategoryId = dbProduct.CategoryId;
            }
            else
            {
                dbProduct.CategoryId = product.CategoryId;
            }

            if (product.Stock == 0)
            {
                dbProduct.OnSale = false;
            }
            List<Tag> categories = _context.Tags.Where(p => p.IsDeleted != true).ToList();
            for (int i = 0; i < categories.Count; i++)
            {
                if (product.Tag == categories[0])
                {
                    dbProduct.TagId = dbProduct.TagId;
                }
            }




            dbProduct.Name = product.Name;
            dbProduct.Price = product.Price;
            dbProduct.ProductImages = images;
            dbProduct.Stock = product.Stock;
            dbProduct.TagId = product.TagId;
            dbProduct.IsNew = product.IsNew;
            dbProduct.IsHot = product.IsHot;
            dbProduct.Desc = product.Desc;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        public IActionResult Create()
        {
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Tags = _context.Tags.ToList();
            ViewBag.Campaigns = _context.Campaigns.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (!ModelState.IsValid) return View();

            ViewBag.Campaigns = _context.Campaigns.ToList();


            List<ProductImage> Images = new List<ProductImage>();
            foreach (var item in product.Photo)
            {

                ProductImage image = new ProductImage();
                image.ImageUrl = item.Savemage(_env, "images/products/p");
                Images.Add(image);
            }
            product.ProductImages = Images;
            product.ProductImages[0].IsMain = true;
            product.ProductCategories = new List<ProductCategories>();
            if (product.CategoryId != null)
            {
                foreach (var categoryId in product.CategoryId)
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
            List<Subscribe> subscribes = _context.Subscribes.ToList();
            foreach (var sub in subscribes)
            {
                string link = "https://localhost:5001/shop/detail/" + product.Id;
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("qolaelo@gmail.com", "patoi");
                mail.To.Add(new MailAddress(sub.Email));


                mail.Subject = "New Product";
                string body = string.Empty;

                using (StreamReader reader = new StreamReader("wwwroot/Assets/Template/NewSubscribe.html"))
                {
                    body = reader.ReadToEnd();
                }

                string about = $"<strong>Hello</strong><br /> a new <strong>{product.Name} </strong> product added to our shop <br/>click the link down below to discover new product!!!";
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


            return RedirectToAction("index");
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
        public IActionResult GetSubTag(int cid)
        {
            var SubTag_List = _context.Tags.Where(s => s.ParentId == cid).Where(s => s.ParentId != null).Select(c => new { Id = c.Id, Name = c.Name }).ToList();
            return Json(SubTag_List);
        }
    }
}
