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
    public class TeamController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public TeamController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<Team> Teams = _context.Teams.ToList();
            return View(Teams);
        }


        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Team db = _context.Teams.Find(id);
            if (db == null) return NotFound();
            Helper.DeleteImage(_env, "images/team", db.ImageUrl);
            _context.Teams.Remove(db);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Team Team)
        {
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }

            if (!Team.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Sekil Formati secin");
            }

            if (Team.Photo.CheckSize(20000))
            {
                ModelState.AddModelError("Photo", "Sekil 20 mb-dan boyuk ola bilmez");
            }


            Team db = new Team();
            string filename = await Team.Photo.SaveImage(_env, "images/team");
            db.ImageUrl = filename;
            db.Name = Team.Name;
            db.Position = Team.Position;

            _context.Teams.Add(db);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Team Team = _context.Teams.Find(id);
            if (Team == null)
            {
                return NotFound();
            }
            return View(Team);
        }


        [HttpPost]
        public async Task<IActionResult> Update(int? id, Team Team)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }

            if (!Team.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Sekil Formati secin");
            }

            if (Team.Photo.CheckSize(20000))
            {
                ModelState.AddModelError("Photo", "Sekil 20 mb-dan boyuk ola bilmez");
            }
            Team db = _context.Teams.Find(id);
            if (db == null)
            {
                return NotFound();
            }
            Helper.DeleteImage(_env, "images/team", db.ImageUrl);
            string filename = await Team.Photo.SaveImage(_env, "images/team");
            Team existName = _context.Teams.FirstOrDefault(x => x.Name.ToLower() == Team.Name.ToLower());

            if (existName != null)
            {
                if (db != existName)
                {
                    ModelState.AddModelError("Name", "Name Already Exist");
                    return View();
                }
            }

            db.ImageUrl = filename;
            db.Name = Team.Name;
            db.Position = Team.Position;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Team Team = _context.Teams.Find(id);
            if (Team == null)
            {
                return NotFound();
            }
            return View(Team);
        }
    }
}
