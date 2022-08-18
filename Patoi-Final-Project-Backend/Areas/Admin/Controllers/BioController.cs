using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Patoi_Final_Project_Backend.DAL;
using Patoi_Final_Project_Backend.Extensions;
using Patoi_Final_Project_Backend.Helpers;
using Patoi_Final_Project_Backend.Models;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Patoi_Final_Project_Backend.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class BioController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public BioController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            Bio bio = _context.Bio.FirstOrDefault();
            return View(bio);
        }


        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Bio db = _context.Bio.Find(id);
            if (db == null) return NotFound();
            Helper.DeleteImage(_env, "images", db.LogoWhite);
            Helper.DeleteImage(_env, "images", db.LogoBlack);
            _context.Bio.Remove(db);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Bio Bio)
        {
            if (ModelState["WhitePhoto"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }

            if (!Bio.WhitePhoto.IsImage())
            {
                ModelState.AddModelError("WhitePhoto", "Sekil Formati secin");
            }

            if (Bio.WhitePhoto.CheckSize(20000))
            {
                ModelState.AddModelError("WhitePhoto", "Sekil 20 mb-dan boyuk ola bilmez");
            }
            if (ModelState["BlackPhoto"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }

            if (!Bio.BlackPhoto.IsImage())
            {
                ModelState.AddModelError("BlackPhoto", "Sekil Formati secin");
            }

            if (Bio.WhitePhoto.CheckSize(20000))
            {
                ModelState.AddModelError("BlackPhoto", "Sekil 20 mb-dan boyuk ola bilmez");
            }

            Bio db = new Bio();
            string filename = await Bio.WhitePhoto.SaveImage(_env, "images");
            string filename2 = await Bio.BlackPhoto.SaveImage(_env, "images");

            db.LogoWhite = filename;
            db.LogoBlack = filename2;
            db.Facebook = Bio.Facebook;
            db.Instagram = Bio.Instagram;
            db.Twitter = Bio.Twitter;
            db.YouTube = Bio.YouTube;
            db.Hotline = Bio.Hotline;
            db.Support = Bio.Support;
            db.Email = Bio.Email;
            db.Address = Bio.Address;


            _context.Bio.Add(db);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Bio Bio = _context.Bio.Find(id);
            if (Bio == null)
            {
                return NotFound();
            }
            return View(Bio);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, Bio Bio)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (ModelState["WhitePhoto"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }

            if (!Bio.WhitePhoto.IsImage())
            {
                ModelState.AddModelError("WhitePhoto", "Sekil Formati secin");
            }

            if (Bio.WhitePhoto.CheckSize(20000))
            {
                ModelState.AddModelError("WhitePhoto", "Sekil 20 mb-dan boyuk ola bilmez");
            }
            if (ModelState["BlackPhoto"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }

            if (!Bio.BlackPhoto.IsImage())
            {
                ModelState.AddModelError("BlackPhoto", "Sekil Formati secin");
            }

            if (Bio.WhitePhoto.CheckSize(20000))
            {
                ModelState.AddModelError("BlackPhoto", "Sekil 20 mb-dan boyuk ola bilmez");
            }


            Bio db = _context.Bio.Find(id);
            if (db == null)
            {
                return NotFound();
            }
            Helper.DeleteImage(_env, "images", db.LogoWhite);
            Helper.DeleteImage(_env, "images", db.LogoBlack);
            string filename = await Bio.WhitePhoto.SaveImage(_env, "images");
            string filename2 = await Bio.BlackPhoto.SaveImage(_env, "images");

            db.LogoWhite = filename;
            db.LogoBlack = filename2;
            db.Facebook = Bio.Facebook;
            db.Instagram = Bio.Instagram;
            db.Twitter = Bio.Twitter;
            db.YouTube = Bio.YouTube;
            db.Hotline = Bio.Hotline;
            db.Support = Bio.Support;
            db.Email = Bio.Email;
            db.Address = Bio.Address;


            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
