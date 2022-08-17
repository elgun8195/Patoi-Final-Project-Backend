using Patoi_Final_Project_Backend.Models;
using System.Collections.Generic;

namespace Patoi_Final_Project_Backend.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Brands> Brands { get; set; }
        public IEnumerable<Blog> Blogs { get; set; }
        public IEnumerable<Categories> Categories { get; set; }
        public IEnumerable<Team> Teams { get; set; }
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<Offer> Offers { get; set; }
        public Blog Blog { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
        public Bio Bio { get; set; }
        public Product Product { get; set; }
    }
}
