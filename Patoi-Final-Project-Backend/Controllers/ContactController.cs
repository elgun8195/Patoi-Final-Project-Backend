using Microsoft.AspNetCore.Mvc;
using Patoi_Final_Project_Backend.DAL;
using Patoi_Final_Project_Backend.Models;
using System.Linq;

namespace Patoi_Final_Project_Backend.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext _context;

        public ContactController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            Bio bio = _context.Bio.FirstOrDefault();
            return View(bio);
        }
    }
}
