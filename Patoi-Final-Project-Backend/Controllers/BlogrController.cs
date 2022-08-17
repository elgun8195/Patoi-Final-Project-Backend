using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Patoi_Final_Project_Backend.DAL;
using Patoi_Final_Project_Backend.Models;
using Patoi_Final_Project_Backend.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Patoi_Final_Project_Backend.Controllers
{
    public class BlogrController : Controller
    {
        private readonly AppDbContext _context;

        public BlogrController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int take = 6, int pagesize = 1)
        {
            ViewBag.Bio = _context.Bio.FirstOrDefault();
            ViewBag.Teams = _context.Teams.Take(1).ToList();
            ViewBag.Blogs = _context.Blog.Take(4).ToList();
            ViewBag.Users = _context.Users.ToList();
            ViewBag.Tags= _context.Tags.ToList();
            ViewBag.Categories= _context.Categories.ToList();
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
    }
}
