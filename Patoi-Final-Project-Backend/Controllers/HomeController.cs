using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Patoi_Final_Project_Backend.DAL;
using Patoi_Final_Project_Backend.Models;
using Patoi_Final_Project_Backend.ViewModels;
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
            homeVM.Products = _context.Products.Include(p=>p.ProductImages).Include(p=>p.ProductCategories).ThenInclude(pc=>pc.Category).ToList();
            homeVM.Product = _context.Products.Include(p => p.ProductImages).Include(p => p.ProductCategories).ThenInclude(pc => pc.Category).FirstOrDefault();
            return View(homeVM);
        }
        public async Task<IActionResult>ModalProduct(int? id)
        {
            Product modal=_context.Products.Include(p => p.ProductImages).Include(p => p.ProductCategories).ThenInclude(pc => pc.Category).FirstOrDefault(p=>p.Id==id);
            return PartialView("_ModalProduct");

        }
    }
}
