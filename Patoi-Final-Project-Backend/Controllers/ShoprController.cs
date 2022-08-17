using Microsoft.AspNetCore.Mvc;
using Patoi_Final_Project_Backend.DAL;
using Patoi_Final_Project_Backend.Models;
using Patoi_Final_Project_Backend.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Patoi_Final_Project_Backend.Controllers
{
    public class ShoprController : Controller
    {

        private readonly AppDbContext _context;

        public ShoprController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int take = 6, int pagesize = 1)
        {

            ViewBag.Bio = _context.Bio.FirstOrDefault();
            ViewBag.Tags = _context.Tags.ToList();
            ViewBag.Categories= _context.Categories.ToList();
            List<Product> products = _context.Products.Skip((pagesize - 1) * take).Take(take).ToList();
            ViewBag.Pcount = _context.Products.Count();
            Pagination<Product> pagination = new Pagination<Product>(

               ReturnPageCount(take), pagesize, products);
            return View(pagination);
        }







        private int ReturnPageCount(int take)
        {
            int product = _context.Products.Count();
            return (int)Math.Ceiling(((decimal)product / take));
        }

        
    }
}
