using Microsoft.AspNetCore.Mvc;
using Patoi_Final_Project_Backend.DAL;
using Patoi_Final_Project_Backend.Models;
using System.Collections.Generic;
using System.Linq;

namespace Patoi_Final_Project_Backend.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TagController : Controller
    {
        private readonly AppDbContext _context;
        public TagController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Tag> tags = _context.Tags.ToList();
            return View(tags);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Tag tag)
        {
            if (!ModelState.IsValid) return NotFound();

            Tag sameName = _context.Tags.FirstOrDefault(t => t.Name.ToLower().Trim().Contains(tag.Name.ToLower().Trim()));

            foreach (var name in _context.Tags)
            {
                if (name.Name.ToLower().Trim().Contains(tag.Name.ToLower().Trim()))
                {
                    ModelState.AddModelError("Name", "Eyni adda tag var");
                    return View(name);
                }
            }
            _context.Tags.Add(tag);

            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int? id)
        {
            Tag tag = _context.Tags.FirstOrDefault(t => t.Id == id);
            return View(tag);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Tag tag)
        {
            if (!ModelState.IsValid) return View();

            Tag existTag = _context.Tags.FirstOrDefault(t => t.Id == tag.Id);
            if (existTag == null) return NotFound();

            existTag.Name = tag.Name;

            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int? id)
        {
            Tag tag = _context.Tags.Find(id);
            if (id == null) return NotFound();

            _context.Tags.Remove(tag);

            _context.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
