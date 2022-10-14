using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Patoi_Final_Project_Backend.DAL;
using Patoi_Final_Project_Backend.Models;
using Patoi_Final_Project_Backend.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Xml.Linq;

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

            ViewBag.Bio = _context.Bio.FirstOrDefault();
            ViewBag.Tags = _context.Tags.ToList();
            ViewBag.Category = _context.Categories.ToList();
            List<Product> products = _context.Products.Include(pc => pc.ProductCategories).ThenInclude(x => x.Category).Skip((pagesize - 1) * take).Take(take).ToList();
            ViewBag.Pcount = _context.Products.Count();


            Pagination<Product> pagination = new Pagination<Product>(ReturnPageCount(take), pagesize, products);
            return View(pagination);
        }


        public IActionResult Search(string search)
        {
            List<Product> products = _context.Products.Where(p => p.Name.ToLower().Contains(search.ToLower())).ToList();
            return PartialView("_Search", products);
        }
        public IActionResult Searchh(string search)
        {
            List<Product> products = _context.Products.Where(p => p.Name.ToLower().Contains(search.ToLower())).ToList();
            return PartialView("_Search", products);
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            AppUser user = new AppUser();
            if (User.Identity.IsAuthenticated)
            {
                user = await _userManager.FindByNameAsync(User.Identity.Name);
            }
            ViewBag.Bio = _context.Bio.FirstOrDefault();
            ViewBag.Product = _context.Products.Include(pt => pt.Tag).Include(pc => pc.ProductCategories).ThenInclude(x => x.Category).FirstOrDefault(p => p.Id == id);
            ViewBag.ProCount = _context.Products.Count();
            Product dbProcduct = _context.Products.Include(pt => pt.Tag).Include(pc => pc.ProductCategories).ThenInclude(x => x.Category).FirstOrDefault(p => p.Id == id);
            ShopVM shopVM = new ShopVM();
            shopVM.Comments = _context.Comments.Include(c => c.Product).Include(c => c.AppUser).Where(c => c.ProductId == id).ToList();

            shopVM.Product = dbProcduct;
            return View(shopVM);

        }

        private int ReturnPageCount(int take)
        {
            int product = _context.Products.Count();
            return (int)Math.Ceiling(((decimal)product / take));
        }

        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public async Task<IActionResult> AddComment(Comments comment)
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (!ModelState.IsValid) return RedirectToAction("Detail", "Shop", new { id = comment.ProductId });
            if (!_context.Products.Any(f => f.Id == comment.ProductId)) return NotFound();
            Comments cmnt = new Comments
            {
                Message = comment.Message,
                ProductId = comment.ProductId,
                Date = DateTime.Now,
                AppUserId = user.Id,
                IsAccess = true
            };
            _context.Comments.Add(cmnt);
            _context.SaveChanges();
            return RedirectToAction("Detail", "Shop", new { id = comment.ProductId });
        }


        public async Task<IActionResult> DeleteComment(int id)
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (!ModelState.IsValid) return RedirectToAction("Detail", "Shop");
            if (!_context.Comments.Any(c => c.Id == id && c.IsAccess == true && c.AppUserId == user.Id)) return NotFound();
            Comments comment = _context.Comments.FirstOrDefault(c => c.Id == id && c.AppUserId == user.Id);
            _context.Comments.Remove(comment);
            _context.SaveChanges();
            return RedirectToAction("Detail", "Shop", new { id = comment.ProductId });
        }
    }
}
