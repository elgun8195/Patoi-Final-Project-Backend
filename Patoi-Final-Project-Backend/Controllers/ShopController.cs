using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Patoi_Final_Project_Backend.DAL;
using Patoi_Final_Project_Backend.Models;
using Patoi_Final_Project_Backend.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Patoi_Final_Project_Backend.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _context;

        public ShopController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int take = 8, int pagesize = 1)
        {
          
            ViewBag.Bio= _context.Bio.FirstOrDefault();
            ViewBag.Tags = _context.Tags.ToList();

            List<Product> products = _context.Products.Skip((pagesize - 1) * take).Take(take).ToList();
            ViewBag.Pcount=products.Count;
            Pagination<Product> pagination = new Pagination<Product>(

               ReturnPageCount(take), pagesize, products);
            return View(pagination);
        }


        public IActionResult Search(string search)
        {
            List<Product> products = _context.Products.Where(p => p.Name.ToLower().Contains(search.ToLower())).ToList();
            return PartialView("_Search",products);
        }
        public IActionResult Searchh(string search)
        {
            List<Product> products = _context.Products.Where(p => p.Name.ToLower().Contains(search.ToLower())).ToList();
            return PartialView("_Search", products);
        }

        public IActionResult Detail(int id)
        {
            HomeVM home = new HomeVM();
            home.Bio = _context.Bio.FirstOrDefault();
            home.Product = _context.Products.Include(pt => pt.Tag).Include(pc => pc.ProductCategories).ThenInclude(x => x.Category).FirstOrDefault(p => p.Id == id);
            ViewBag.ProCount = _context.Products.Count();
            return View(home);
        }
        private int ReturnPageCount(int take)
        {
            int product = _context.Products.Count();
            return (int)Math.Ceiling(((decimal)product / take));
        }
    }
}
