using Microsoft.AspNetCore.Mvc;

namespace Patoi_Final_Project_Backend.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
