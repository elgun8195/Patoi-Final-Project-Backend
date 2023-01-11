using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Patoi_Final_Project_Backend.DAL;
using Patoi_Final_Project_Backend.Models;
using Patoi_Final_Project_Backend.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Patoi_Final_Project_Backend.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM();

            homeVM.Blogs = _context.Blog.ToList();
            homeVM.Brands = _context.Brands.ToList();
            homeVM.Offers = _context.Offers.ToList();
            homeVM.Categories = _context.Categories.ToList();
            homeVM.Products = _context.Products.Include(p => p.ProductImages).Include(p => p.ProductCategories).ThenInclude(pc => pc.Category).Take(1).ToList();
            homeVM.Product = _context.Products.Include(p => p.ProductImages).Include(p => p.ProductCategories).ThenInclude(pc => pc.Category).FirstOrDefault();
            return View(homeVM);
        }
         
        public IActionResult Modalim(int id)
        {
            Product products = _context.Products.Include(b=>b.ProductImages).Include(b=>b.Tag).Include(b=>b.ProductCategories).ThenInclude(b=>b.Category).FirstOrDefault(b => b.Id==id);

            return PartialView("_ModalProduct", products);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Subscribe subscribe)
        {

            HomeVM homeVM = new HomeVM();

            homeVM.Blogs = _context.Blog.ToList();
            homeVM.Brands = _context.Brands.ToList();
            homeVM.Offers = _context.Offers.ToList();
            homeVM.Categories = _context.Categories.ToList();
            homeVM.Products = _context.Products.Include(p => p.ProductImages).Include(p => p.ProductCategories).ThenInclude(pc => pc.Category).Take(1).ToList();
            homeVM.Product = _context.Products.Include(p => p.ProductImages).Include(p => p.ProductCategories).ThenInclude(pc => pc.Category).FirstOrDefault();
            Subscribe emailCheck = _context.Subscribes.FirstOrDefault(s => s.Email == subscribe.Email);

            if (emailCheck != null)
            {
                ModelState.AddModelError("", "This email address is already subscriber,please enter different email address");
                return View();
            }
            Subscribe subscriber = new Subscribe
            {
                Email = subscribe.Email,
            };
            _context.Subscribes.Add(subscribe);
            _context.SaveChanges();
            return View(homeVM);
        }
       

    }
}
