using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Patoi_Final_Project_Backend.DAL;
using Patoi_Final_Project_Backend.Models;
using System;
using System.Linq;

namespace Patoi_Final_Project_Backend.Controllers
{
    public class ContactController : Controller
    {

        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public ContactController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            ViewBag.Bio=_context.Bio.FirstOrDefault();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Contact msg)
        {
            if (!ModelState.IsValid) return View();
            Contact contact = new Contact
            {
                Id = msg.Id,
                Name=msg.Name,
                Subject=msg.Subject,
                Accept=msg.Accept,
                Message = msg.Message,
                Email = msg.Email,
                PhoneNumber=msg.PhoneNumber,
                Date = DateTime.Now,
            };
            if (contact.Accept==true)
            {
                _context.Contacts.Add(contact);
                _context.SaveChanges();

            return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("index","contact");
            }
        }
    }
}
