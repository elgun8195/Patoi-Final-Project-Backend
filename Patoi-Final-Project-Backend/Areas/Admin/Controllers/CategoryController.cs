using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Patoi_Final_Project_Backend.DAL;
using Patoi_Final_Project_Backend.Extensions;
using Patoi_Final_Project_Backend.Helpers;
using Patoi_Final_Project_Backend.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Patoi_Final_Project_Backend.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public CategoryController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {

            List<Categories> model = _context.Categories.ToList();

            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Categories Categories)
        {
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }

            if (!Categories.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Sekil Formati secin");
            }

            if (Categories.Photo.CheckSize(20000))
            {
                ModelState.AddModelError("Photo", "Sekil 20 mb-dan boyuk ola bilmez");
            }


            Categories db = new Categories();
            string filename = await Categories.Photo.SaveImage(_env, "images/categories");
            db.ImageUrl = filename;
            db.Name = Categories.Name;

            _context.Categories.Add(db);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Categories Categories = _context.Categories.Find(id);
            if (Categories == null)
            {
                return NotFound();
            }
            return View(Categories);
        }


        [HttpPost]
        public async Task<IActionResult> Update(int? id, Categories Categories)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }

            if (!Categories.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Sekil Formati secin");
            }

            if (Categories.Photo.CheckSize(20000))
            {
                ModelState.AddModelError("Photo", "Sekil 20 mb-dan boyuk ola bilmez");
            }
            Categories db = _context.Categories.Find(id);
            if (db == null)
            {
                return NotFound();
            }
            Helper.DeleteImage(_env, "images/categories", db.ImageUrl);
            string filename = await Categories.Photo.SaveImage(_env, "images/categories");
            Categories existName = _context.Categories.FirstOrDefault(x => x.Name.ToLower() == Categories.Name.ToLower());

            if (existName != null)
            {
                if (db != existName)
                {
                    ModelState.AddModelError("Name", "Name Already Exist");
                    return View();
                }
            }

            db.ImageUrl = filename;
            db.Name = Categories.Name;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            Categories category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null) return Json(new { status = 404 });

            _context.Categories.Remove(category);
            _context.SaveChanges();

            return Json(new { status = 200 });

        }


        public IActionResult Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Categories Categories = _context.Categories.Find(id);
            if (Categories == null)
            {
                return NotFound();
            }
            return View(Categories);
        }
    }
}
