using Microsoft.AspNetCore.Mvc;
using ShoppingApp.DataAccess.Data;
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
         public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if(obj.Name==obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The name and display order can't be same");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["Success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Edit(int? Id)
        {
            if(Id==null|| Id == 0)
            {
                return NotFound();
            }
            Category? CategoryFromDb = _db.Categories.Find(Id);
            //Category? CategoryFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==Id);           DIFFERENT WAYS TO RETRIEVE DATA FROM DB(PREFERRABLE IS 2ND ONE)
            //Category? CategoryFromDb2 = _db.Categories.Where(u=>u.Id==Id).FirstOrDefault();

            if(CategoryFromDb == null){
                return NotFound();
            }
            return View(CategoryFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The name and display order can't be same");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["Success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            Category? CategoryFromDb = _db.Categories.Find(Id);

            if (CategoryFromDb == null)
            {
                return NotFound();
            }
            return View(CategoryFromDb);
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePOST(int? Id)
        {
            Category? obj = _db.Categories.Find(Id);

            if (obj == null)
            {
                return NotFound();
            }
                 _db.Categories.Remove(obj);
                _db.SaveChanges();
            TempData["Success"] = "Category is Deleted ";

            return RedirectToAction("Index");           
                 }
    }
}
