using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using System;
using Patoi_Final_Project_Backend.Models;

namespace Patoi_Final_Project_Backend.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Brands> Brands { get; set; }
        public DbSet<Blog> Blog { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Bio> Bio { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public DbSet<ProductCategories> ProductCategories { get; set; }

    }
}
