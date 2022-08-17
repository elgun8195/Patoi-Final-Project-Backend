using Microsoft.AspNetCore.Mvc;
using Patoi_Final_Project_Backend.DAL;
using Patoi_Final_Project_Backend.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Patoi_Final_Project_Backend.ViewComponents
{
    public class HeaderViewComponent: ViewComponent
    {
        private readonly AppDbContext _db;

        public HeaderViewComponent(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {

            Bio model = _db.Bio.FirstOrDefault();
            return View(await Task.FromResult(model));
        }
    }
}
