using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Data;
using ShoppingApp.Models;

namespace ShoppingApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db) //Since it's mentioned in program.cs then we can call it here as argument
        {
            _db = db;
        }
        public IActionResult Index()
        {
          List<Category>objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }
    }
}
