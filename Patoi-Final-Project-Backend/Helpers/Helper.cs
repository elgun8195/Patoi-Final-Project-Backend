using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Patoi_Final_Project_Backend.Helpers
{
    public static class Helper
    {
        public static bool IsImage(this IFormFile file)
        {
            return file.ContentType.Contains("image/");
        }

        public static bool CheckSize(this IFormFile file, int kb)
        {
            return file.Length / 1024 > kb;
        }


        public static void DeleteImage(IWebHostEnvironment env, string folder, string file)
        {
            string path = env.WebRootPath;
            string result = Path.Combine(path, folder, file);

            if (System.IO.File.Exists(result))
            {
                System.IO.File.Delete(result);
            }

        }
    }
    public enum Roless
    {
        Admin, Member, SuperAdmin
    }
}

//public class BasketController : Controller
//{
//    private AppDbContext _context;
//    private UserManager<AppUser> _userManager;
//    public BasketController(AppDbContext context, UserManager<AppUser> userManager)
//    {
//        _context = context;
//        _userManager = userManager;
//    }


//    public IActionResult Index()
//    {
//        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

//        if (currentUserId == null) return RedirectToAction("login", "account");

//        Basket basket = _context.Baskets
//                .Include(b => b.BasketItems)
//                .FirstOrDefault(b => b.UserId == currentUserId);


//        if (basket == null)
//        {
//            return RedirectToAction("index", "shop");
//        }


//        List<BasketItem> basketItems = _context.BasketItems.ToList();

//        List<BasketProduct> products = new List<BasketProduct>();

//        foreach (var item in basketItems)
//        {
//            Product product = _context.Products.FirstOrDefault(p => p.Id == item.ProductId);

//            BasketProduct BasketProduct = new BasketProduct
//            {
//                Id = item.ProductId,
//                Price = product.Price,
//                Name = product.Name,
//                BasketCount = item.Count,
//                Image = product.Image
//            };
//            products.Add(BasketProduct);

//        }
//        return View(products);
//    }


//    public IActionResult AddBasket(int? id, string ReturnUrl)
//    {

//        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

//        if (currentUserId == null) return RedirectToAction("login", "account");
//        Basket basket = _context.Baskets
//                .Include(b => b.BasketItems)
//                .FirstOrDefault(b => b.UserId == currentUserId);

//        if (id == null) return NoContent();

//        Product dbproduct = _context.Products.FirstOrDefault(x => x.Id == id);
//        if (dbproduct == null) return NoContent();

//        List<BasketItem> basketItems = _context.BasketItems.Where(b => b.BasketId == basket.Id).ToList();

//        BasketItem isexsist = basketItems.Find(p => p.ProductId == id);



//        if (isexsist == null)
//        {
//            BasketItem basketItem = new BasketItem();

//            basketItem.ProductId = dbproduct.Id;
//            basketItem.Count = 1;
//            basketItem.BasketId = basket.Id;

//            _context.Add(basketItem);
//        }
//        else
//        {
//            if (dbproduct.Stock < isexsist.Count) return RedirectToAction("index", "shop");
//            isexsist.Count++;
//        }

//        _context.SaveChanges();
//        if (ReturnUrl != null) return Redirect(ReturnUrl);

//        return RedirectToAction(nameof(Index));
//    }

//    public IActionResult RemoveItem(int id, string ReturnUrl)
//    {
//        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

//        if (userId == null) return RedirectToAction("login", "account");

//        Basket basket = _context.Baskets.FirstOrDefault(b => b.UserId == userId);

//        List<BasketItem> basketItems = _context.BasketItems.Where(b => b.BasketId == basket.Id).ToList();

//        BasketItem deleteItem = basketItems.FirstOrDefault(p => p.ProductId == id);

//        _context.BasketItems.Remove(deleteItem);

//        _context.SaveChanges();

//        if (ReturnUrl != null) return Redirect(ReturnUrl);

//        return RedirectToAction("index");
//    }

//    public IActionResult Plus(int id, string Returnurl)
//    {
//        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

//        if (userId == null) return RedirectToAction("login", "account");

//        Basket basket = _context.Baskets.FirstOrDefault(b => b.UserId == userId);

//        List<BasketItem> basketItems = _context.BasketItems.Where(b => b.BasketId == basket.Id).ToList();

//        BasketItem plusItem = basketItems.Find(p => p.ProductId == id);
//        if (plusItem == null) return RedirectToAction("AddBasket", new { id = id, Returnurl = Returnurl });

//        var dbProduct = _context.Products.FirstOrDefault(p => p.Id == id);

//        if (plusItem.Count < dbProduct.Stock)
//        {
//            _context.BasketItems.FirstOrDefault(b => b.Id == plusItem.Id).Count++;
//            _context.SaveChanges();
//        }
//        if (Returnurl != null) return Redirect(Returnurl);

//        return RedirectToAction("index", "basket");
//    }

//    public IActionResult Minus(int id, string Returnurl)
//    {
//        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

//        if (userId == null) return RedirectToAction("login", "account");

//        Basket basket = _context.Baskets.FirstOrDefault(b => b.UserId == userId);

//        List<BasketItem> basketItems = _context.BasketItems.Where(b => b.BasketId == basket.Id).ToList();

//        BasketItem decreaseItem = basketItems.FirstOrDefault(p => p.ProductId == id);

//        if (decreaseItem == null) return RedirectToAction("AddBasket", new { id = id, Returnurl = Returnurl });


//        if (decreaseItem.Count > 1)
//        {
//            _context.BasketItems.FirstOrDefault(b => b.Id == decreaseItem.Id).Count--;

//        }
//        else
//        {
//            _context.BasketItems.Remove(decreaseItem);
//        }
//        _context.SaveChanges();


//        if (Returnurl != null) return Redirect(Returnurl);
//        return RedirectToAction("index", "basket");
//    }

//}
