using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using shoppingAppRazor_temp.Data;
using shoppingAppRazor_temp.Models;

namespace shoppingAppRazor_temp.Pages.Categories
{
    [BindProperties] //Allows to bind(populate) properties on post action(All properties)
    public class DeleteModel : PageModel
    {

        private readonly ApplicationDbContext _db;

        public Category Category { get; set; }

        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet(int? Id)
        {
            if (Id != null && Id != 0)
            {
                Category = _db.Categories.Find(Id);
            }
        }
        public IActionResult OnPost()
        {
            Category? obj = _db.Categories.Find(Category.Id);

            if (obj == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["Success"] = "Category is Deleted ";
            return RedirectToPage("Index");
        }
    }
}
