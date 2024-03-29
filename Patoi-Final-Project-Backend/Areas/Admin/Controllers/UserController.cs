﻿using Microsoft.AspNetCore.Authorization;
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
    public class UserController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public UserController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<User> Users = _context.Users.ToList();
            return View(Users);
        }


        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User db = _context.Users.Find(id);
            if (db == null) return NotFound();
            Helper.DeleteImage(_env, "images/user", db.ImageUrl);
            _context.Users.Remove(db);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User User)
        {
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }

            if (!User.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Sekil Formati secin");
            }

            if (User.Photo.CheckSize(20000))
            {
                ModelState.AddModelError("Photo", "Sekil 20 mb-dan boyuk ola bilmez");
            }


            User db = new User();
            string filename = await User.Photo.SaveImage(_env, "images/user");
            db.ImageUrl = filename;
            db.Name = User.Name;
            db.Desc = User.Desc;
            db.Position = User.Position;

            _context.Users.Add(db);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            User User = _context.Users.Find(id);
            if (User == null)
            {
                return NotFound();
            }
            return View(User);
        }


        [HttpPost]
        public async Task<IActionResult> Update(int? id, User User)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }

            if (!User.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Sekil Formati secin");
            }

            if (User.Photo.CheckSize(20000))
            {
                ModelState.AddModelError("Photo", "Sekil 20 mb-dan boyuk ola bilmez");
            }
            User db = _context.Users.Find(id);
            if (db == null)
            {
                return NotFound();
            }
            Helper.DeleteImage(_env, "images/user", db.ImageUrl);
            string filename = await User.Photo.SaveImage(_env, "images/user");
            User existName = _context.Users.FirstOrDefault(x => x.Name.ToLower() == User.Name.ToLower());

            if (existName != null)
            {
                if (db != existName)
                {
                    ModelState.AddModelError("Name", "Name Already Exist");
                    return View();
                }
            }

            db.ImageUrl = filename;
            db.Name = User.Name;
            db.Desc = User.Desc;
            db.Position = User.Position;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            User User = _context.Users.Find(id);
            if (User == null)
            {
                return NotFound();
            }
            return View(User);
        }
    }
}
