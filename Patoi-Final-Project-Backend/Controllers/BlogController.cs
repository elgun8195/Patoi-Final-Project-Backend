using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Patoi_Final_Project_Backend.DAL;
using Patoi_Final_Project_Backend.Models;
using Patoi_Final_Project_Backend.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Patoi_Final_Project_Backend.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        private UserManager<AppUser> _userManager;
        public BlogController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Index(int take = 3, int pagesize = 1)
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
        
        
        
        
        public IActionResult Detail(int id)
        {
            HomeVM home = new HomeVM();
            home.Bio=_context.Bio.FirstOrDefault();
            home.Blog =  _context.Blog.FirstOrDefault(p => p.Id == id); 
            home.Blogs=_context.Blog.ToList();
            home.Teams = _context.Teams.ToList();
            home.Users=_context.Users.ToList();
            home.Tags=_context.Tags.ToList();
            home.Categories=_context.Categories.ToList();
            home.Comments = _context.Comments.Include(c => c.Blog).Include(c => c.AppUser).Where(c => c.BlogId == id).ToList();
            return View(home);
        }
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public async Task<IActionResult> AddComment(Comments comment)
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (!ModelState.IsValid) return RedirectToAction("Detail", "Blog", new { id = comment.BlogId });
            if (!_context.Blog.Any(f => f.Id == comment.BlogId)) return NotFound();
            Comments cmnt = new Comments
            {
                Message = comment.Message,
                BlogId = comment.BlogId,
                Date = DateTime.Now,
                AppUserId = user.Id,
                IsAccess = true,
            };
            _context.Comments.Add(cmnt);
            _context.SaveChanges();
            return RedirectToAction("Detail", "Blog", new { id = comment.BlogId });
        }

        public async Task<IActionResult> DeleteComment(int id)
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (!ModelState.IsValid) return RedirectToAction("Index", "Blog");
            if (!_context.Comments.Any(c => c.Id == id && c.IsAccess == true && c.AppUserId == user.Id)) return NotFound();
            Comments comment = _context.Comments.FirstOrDefault(c => c.Id == id && c.AppUserId == user.Id);
            _context.Comments.Remove(comment);
            _context.SaveChanges();
            return RedirectToAction("Detail", "Blog", new { id = comment.BlogId });
        }
    }
}
