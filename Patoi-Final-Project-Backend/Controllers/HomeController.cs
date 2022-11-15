﻿using Microsoft.AspNetCore.Mvc;
using Patoi_Final_Project_Backend.DAL;
using Patoi_Final_Project_Backend.Models;
using Patoi_Final_Project_Backend.ViewModels;
using System.Linq;

namespace Patoi_Final_Project_Backend.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int? id)
        {
            HomeVM homeVM = new HomeVM();

            homeVM.Blogs = _context.Blog.ToList();
            homeVM.Brands = _context.Brands.ToList();
            homeVM.Offers = _context.Offers.ToList();
            homeVM.Categories = _context.Categories.ToList();
            homeVM.Products = _context.Products.ToList();
            homeVM.Product = _context.Products.FirstOrDefault(x => x.Id == id);
            return View(homeVM);
        }
    }
}
