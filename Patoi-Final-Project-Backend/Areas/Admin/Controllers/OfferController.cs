using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Patoi_Final_Project_Backend.DAL;
using Patoi_Final_Project_Backend.Extensions;
using Patoi_Final_Project_Backend.Helpers;
using Patoi_Final_Project_Backend.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Patoi_Final_Project_Backend.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class OfferController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public OfferController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<Offer> Offers = _context.Offers.ToList();
            return View(Offers);
        }


        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Offer db = _context.Offers.Find(id);
            if (db == null) return NotFound();
            Helper.DeleteImage(_env, "images/offers", db.ImageUrl);
            _context.Offers.Remove(db);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Offer Offer)
        {
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }

            if (!Offer.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Sekil Formati secin");
            }

            if (Offer.Photo.CheckSize(20000))
            {
                ModelState.AddModelError("Photo", "Sekil 20 mb-dan boyuk ola bilmez");
            }


            Offer db = new Offer();
            string filename = await Offer.Photo.SaveImage(_env, "images/offers");
            db.ImageUrl = filename;

            _context.Offers.Add(db);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Offer Offer = _context.Offers.Find(id);
            if (Offer == null)
            {
                return NotFound();
            }
            return View(Offer);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, Offer Offer)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }

            if (!Offer.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Sekil Formati secin");
            }

            if (Offer.Photo.CheckSize(20000))
            {
                ModelState.AddModelError("Photo", "Sekil 20 mb-dan boyuk ola bilmez");
            }
            Offer db = _context.Offers.Find(id);
            if (db == null)
            {
                return NotFound();
            }
            Helper.DeleteImage(_env, "images/offers", db.ImageUrl);
            string filename = await Offer.Photo.SaveImage(_env, "images/offers");


            db.ImageUrl = filename;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Offer Offer = _context.Offers.Find(id);
            if (Offer == null)
            {
                return NotFound();
            }
            return View(Offer);
        }
    }
}
