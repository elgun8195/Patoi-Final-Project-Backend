using Microsoft.AspNetCore.Mvc;
using Patoi_Final_Project_Backend.DAL;
using Patoi_Final_Project_Backend.ViewModels;
using System.Linq;

namespace Patoi_Final_Project_Backend.Controllers
{
    public class FaqController : Controller
    {
        private readonly AppDbContext _context;

        public FaqController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM();

             
            homeVM.Brands = _context.Brands.ToList(); 

            return View(homeVM);
        }
    }
}
