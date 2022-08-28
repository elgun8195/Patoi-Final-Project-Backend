using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Patoi_Final_Project_Backend.DAL;
using Patoi_Final_Project_Backend.Models;
using Patoi_Final_Project_Backend.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Patoi_Final_Project_Backend.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _context;
        private UserManager<AppUser> _userManager;
        public ShopController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Index(int take = 8, int pagesize = 1)
        {
          
            ViewBag.Bio= _context.Bio.FirstOrDefault();
            ViewBag.Tags = _context.Tags.ToList();
            ViewBag.Category = _context.Categories.ToList();
            List<Product> products = _context.Products.Include(pc => pc.ProductCategories).ThenInclude(x => x.Category).Skip((pagesize - 1) * take).Take(take).ToList();
            ViewBag.Pcount= _context.Products.Count();


            Pagination<Product> pagination = new Pagination<Product>(ReturnPageCount(take), pagesize, products);
            return View(pagination);
        }
        //public IActionResult LoadMore(int skip)
        //{
        //    List<Product> product = _context.Products.Skip(skip).Take(2).ToList();
        //    return PartialView("_ProductPartial", product);
        //}

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

        public  IActionResult Detail(int id)
        {
            
            ViewBag.Bio = _context.Bio.FirstOrDefault();
            ViewBag.Product = _context.Products.Include(pt => pt.Tag).Include(pc=>pc.ProductCategories).ThenInclude(x => x.Category).FirstOrDefault(p => p.Id == id);
            ViewBag.ProCount = _context.Products.Count();

            return View();
        
        }
      
        private int ReturnPageCount(int take)
        {
            int product = _context.Products.Count();
            return (int)Math.Ceiling(((decimal)product / take));
        }
    }
}
