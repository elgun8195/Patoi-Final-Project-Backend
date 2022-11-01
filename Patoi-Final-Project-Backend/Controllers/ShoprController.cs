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
    public class ShoprController : Controller
    {

        private readonly AppDbContext _context;

        public ShoprController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int sortId,int page = 1)
        {

            ViewBag.Categories= _context.Categories.ToList();
            ViewBag.Bio = _context.Bio.FirstOrDefault();
            ViewBag.Tags = _context.Tags.ToList();
            ViewBag.Best = _context.Products.Take(4).ToList();

            ViewBag.Pcount = _context.Products.Count();
            ViewBag.id = sortId;

            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Products.Count() / 6);
            ViewBag.CurrentPage = page;
            List<Product> model = _context.Products.Include(pc => pc.ProductCategories).ThenInclude(x => x.Category).Skip((page - 1) * 8).Take(8).ToList();
            switch (sortId)
            {
                case 1:
                    model = _context.Products.Include(f => f.ProductCategories).ThenInclude(fc => fc.Category).Include(f => f.Campaign).ToList();
                    break;
                case 2:
                    model = _context.Products.Include(f => f.ProductCategories).ThenInclude(fc => fc.Category).Include(f => f.Campaign).OrderByDescending(s => s.Name).ToList();
                    break;
                case 3:
                    model = _context.Products.Include(f => f.ProductCategories).ThenInclude(fc => fc.Category).Include(f => f.Campaign).OrderBy(s => s.Name).ToList();
                    break;
                case 4:
                    model = _context.Products.Include(f => f.ProductCategories).ThenInclude(fc => fc.Category).Include(f => f.Campaign).OrderByDescending(s => s.CampaignId == null ? s.Price : (s.Price * (100 - s.Campaign.DiscountPercent) / 100)).ToList();
                    break;
                case 5:
                    model = _context.Products.Include(f => f.ProductCategories).ThenInclude(fc => fc.Category).Include(f => f.Campaign).OrderBy(s => s.CampaignId == null ? s.Price : (s.Price * (100 - s.Campaign.DiscountPercent) / 100)).ToList();
                    break;
                default:

                    break;
            }
            return View(model);
        }







        private int ReturnPageCount(int take)
        {
            int product = _context.Products.Count();
            return (int)Math.Ceiling(((decimal)product / take));
        }

        
    }
}
