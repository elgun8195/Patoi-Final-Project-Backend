using Microsoft.AspNetCore.Mvc;
using Patoi_Final_Project_Backend.DAL;
using Patoi_Final_Project_Backend.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Patoi_Final_Project_Backend.ViewComponents
{
    public class ProductViewComponent : ViewComponent
    {
        private readonly AppDbContext _db;

        public ProductViewComponent(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync(int take)
        {
            List<Product> model = _db.Products.Take(take).ToList();
            return View(await Task.FromResult(model));
        }
    }
}
