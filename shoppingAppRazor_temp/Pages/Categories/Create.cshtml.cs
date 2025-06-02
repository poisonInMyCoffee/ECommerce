using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using shoppingAppRazor_temp.Data;
using shoppingAppRazor_temp.Models;

namespace shoppingAppRazor_temp.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        [BindProperty] //Allows to bind(populate) properties on post action
        public Category Category { get; set; }

        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            _db.Categories.Add(Category); //Passing Category directly here as it will be binded by BindProperty method
            _db.SaveChanges();
            TempData["Success"] = "Category is created";
            return RedirectToPage("Index");
        }

    }
}
