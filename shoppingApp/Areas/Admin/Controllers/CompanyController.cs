using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoppingApp.DataAccess.Repository.IRepository;
using ShoppingApp.Models;
using ShoppingApp.Models.ViewModels;
using ShoppingApp.Utilities;

namespace ShoppingApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]

    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            
        }
        public IActionResult Index()
        {
            List<Company> objCategoryList = _unitOfWork.Company.GetAll().ToList();

            return View(objCategoryList);
        }

        public IActionResult Upsert(int? Id) //Update+Insert
        {
            //ViewBag.CategoryList = CategoryList; //Can't send via view as the model its linked to is Company, so we send via viewbag 
           
         
            if (Id == 0 || Id == null)
            {
                return View(new Company());
            }
            else
            {
              Company companyObj = _unitOfWork.Company.Get(u => u.Id == Id);
            return View(companyObj);
            }
        }
       
        [HttpPost]
        public IActionResult Upsert(Company companyObj)
        {
            if (ModelState.IsValid)
            {
                if (companyObj.Id == 0)
                {
                    _unitOfWork.Company.Add(companyObj);
                    TempData["success"] = "Company created successfully";
                }
                else
                {
                    _unitOfWork.Company.Update(companyObj); // ✅ fix for editing
                }

                _unitOfWork.Save();
                TempData["success"] = "Company updated successfully"; 
                return RedirectToAction("Index");
            }
            else
            {
              

                return View(companyObj);
            }
        }




        //public IActionResult Delete(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    Company? productFromDb = _unitOfWork.Company.Get(u => u.Id == id);

        //    if (productFromDb == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(productFromDb);
        //}
        //[HttpPost, ActionName("Delete")]
        //public IActionResult DeletePOST(int? id)
        //{
        //    Company? obj = _unitOfWork.Company.Get(u => u.Id == id);
        //    if (obj == null)
        //    {
        //        return NotFound();
        //    }
        //    _unitOfWork.Company.Remove(obj);
        //    _unitOfWork.Save();
        //    TempData["success"] = "Category deleted successfully";
        //    return RedirectToAction("Index");
        //}





        #region API Calls
        [HttpGet]
        public IActionResult GetAll() {
            List<Company> objProductList = _unitOfWork.Company.GetAll().ToList();
            return Json(new {data=objProductList});
        }

        [HttpDelete]
        public IActionResult Delete(int? Id)
        {
            var CompanyToBeDeleted = _unitOfWork.Company.Get(u => u.Id == Id);
            if (CompanyToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
         
            _unitOfWork.Company.Remove(CompanyToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "deleted successful" });
        }

        #endregion
    }
}
    