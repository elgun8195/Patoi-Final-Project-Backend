using Microsoft.AspNetCore.Mvc;
using Patoi_Final_Project_Backend.DAL;
using Patoi_Final_Project_Backend.ViewModels;
using System.Linq;

namespace Patoi_Final_Project_Backend.Controllers
{
    public class AboutController : Controller
    {
        private readonly AppDbContext _context;

        public AboutController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM();

            homeVM.Blogs = _context.Blog.ToList();
            homeVM.Brands = _context.Brands.ToList();
            homeVM.Teams = _context.Teams.ToList();
            homeVM.Users = _context.Users.ToList();
            return View(homeVM);
        }
    }
}
