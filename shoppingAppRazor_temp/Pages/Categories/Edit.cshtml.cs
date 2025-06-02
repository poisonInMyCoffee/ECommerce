using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using shoppingAppRazor_temp.Data;
using shoppingAppRazor_temp.Models;

namespace shoppingAppRazor_temp.Pages.Categories
{
    [BindProperties] //Allows to bind(populate) properties on post action(All properties)
    public class EditModel : PageModel
    {
       
        private readonly ApplicationDbContext _db;
        
        public Category Category { get; set; }

        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet(int? Id)
        {
            if(Id !=null && Id != 0)
            {
                Category = _db.Categories.Find(Id);
            }
        }
        public IActionResult OnPost()
        {       
            if (ModelState.IsValid)
            {
                _db.Categories.Update(Category);
                _db.SaveChanges();
               TempData["Success"] = "Category updated successfully";
                return RedirectToPage("Index");
            }
            return Page();
        }
        }
}

