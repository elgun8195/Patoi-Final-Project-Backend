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
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;

        public BlogController(AppDbContext context)
        {
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
            return View(home);
        }
    }
}
