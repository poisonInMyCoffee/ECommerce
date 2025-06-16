using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.DataAccess.Repository.IRepository;
using ShoppingApp.Models;

namespace ShoppingApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public IActionResult Index()
        {
            return View();
        }
        #region API Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            List<OrderHeader> objOrderHeaders = _unitOfWork.OrderHeader.GetAll(includeProperties: "ApplicationUser").ToList();
            return Json(new { data = objOrderHeaders });
        }

        //[HttpDelete]
        //public IActionResult Delete(int? Id)
        //{
        //    var productToBeDeleted = _unitOfWork.Product.Get(u => u.Id == Id);
        //    if (productToBeDeleted == null)
        //    {
        //        return Json(new { success = false, message = "Error while deleting" });
        //    }
        //    var oldImagePath = Path.Combine(IWebHostEnvironment.WebRootPath, productToBeDeleted.ImageUrl.TrimStart('\\'));
        //    if (System.IO.File.Exists(oldImagePath))
        //    {
        //        System.IO.File.Delete(oldImagePath);
        //    }
        //    _unitOfWork.Product.Remove(productToBeDeleted);
        //    _unitOfWork.Save();

        //    return Json(new { success = true, message = "deleted successful" });
        //}

        #endregion
    }
}
