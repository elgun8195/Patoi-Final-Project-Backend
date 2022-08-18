using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Patoi_Final_Project_Backend.DAL;
using Patoi_Final_Project_Backend.Extensions;
using Patoi_Final_Project_Backend.Helpers;
using Patoi_Final_Project_Backend.Models;
using Patoi_Final_Project_Backend.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Patoi_Final_Project_Backend.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public BlogController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int take = 4, int pagesize = 1)
        {
            List<Blog> blogs = _context.Blog.Skip((pagesize - 1) * take).Take(take).ToList();
            Pagination<Blog> pagination = new Pagination<Blog>(

               ReturnPageCount(take), pagesize, blogs);
            return View(pagination);
        }
        private int ReturnPageCount(int take)
        {
            int blog = _context.Blog.Count();
            return (int)Math.Ceiling(((decimal)blog / take));
        }



        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Blog db = _context.Blog.Find(id);
            if (db == null) return NotFound();
            Helper.DeleteImage(_env, "images/blog", db.ImageUrl);
            _context.Blog.Remove(db);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Blog Blog)
        {
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }

            if (!Blog.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Sekil Formati secin");
            }

            if (Blog.Photo.CheckSize(20000))
            {
                ModelState.AddModelError("Photo", "Sekil 20 mb-dan boyuk ola bilmez");
            }


            Blog db = new Blog();
            string filename = await Blog.Photo.SaveImage(_env, "images/blog");
            db.ImageUrl = filename;
            db.Name = Blog.Name;
            db.Desc = Blog.Desc;
            db.Title = Blog.Title;
            db.Date = Blog.Date;

            _context.Blog.Add(db);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Blog blog = _context.Blog.Find(id);
            if (blog == null)
            {
                return NotFound();
            }
            return View(blog);
        }


        [HttpPost]
        public async Task<IActionResult> Update(int? id, Blog Blog)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }

            if (!Blog.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Sekil Formati secin");
            }

            if (Blog.Photo.CheckSize(20000))
            {
                ModelState.AddModelError("Photo", "Sekil 20 mb-dan boyuk ola bilmez");
            }
            Blog db = _context.Blog.Find(id);
            if (db == null)
            {
                return NotFound();
            }
            Helper.DeleteImage(_env, "images/blog", db.ImageUrl);
            string filename = await Blog.Photo.SaveImage(_env, "images/blog");
            Blog existName = _context.Blog.FirstOrDefault(x => x.Name.ToLower() == Blog.Name.ToLower());

            if (existName != null)
            {
                if (db != existName)
                {
                    ModelState.AddModelError("Name", "Name Already Exist");
                    return View();
                }
            }

            db.ImageUrl = filename;
            db.Name = Blog.Name;
            db.Desc = Blog.Desc;

            db.Date = Blog.Date;
            db.Title = Blog.Title;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Blog blog = _context.Blog.Find(id);
            if (blog == null)
            {
                return NotFound();
            }
            return View(blog);
        }
    }
}
