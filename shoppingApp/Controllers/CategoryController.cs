using Microsoft.AspNetCore.Mvc;
using ShoppingApp.DataAccess.Data;
using ShoppingApp.DataAccess.Repository.IRepository;
using ShoppingApp.Models;


namespace ShoppingApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;
        public CategoryController(IUnitOfWork UnitOfWork) //Since it's mentioned in program.cs then we can call it here as argument
        {
            _UnitOfWork = UnitOfWork;
        }
        public IActionResult Index()
        {
          List<Category>objCategoryList = _UnitOfWork.Category.GetAll().ToList();
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
                _UnitOfWork.Category.Add(obj);
                _UnitOfWork.Save();
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
            Category? CategoryFromDb = _UnitOfWork.Category.Get(u=>u.Id==Id);
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
                _UnitOfWork.Category.Add(obj);
                _UnitOfWork.Save();
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
            Category? CategoryFromDb = _UnitOfWork.Category.Get(u=>u.Id==Id);

            if (CategoryFromDb == null)
            {
                return NotFound();
            }
            return View(CategoryFromDb);
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePOST(int? Id)
        {
            Category? obj = _UnitOfWork.Category.Get(u => u.Id == Id);

            if (obj == null)
            {
                return NotFound();
            }
            _UnitOfWork.Category.Remove(obj);
            _UnitOfWork.Save();
            TempData["Success"] = "Category is Deleted ";

            return RedirectToAction("Index");           
                 }
    }
}
