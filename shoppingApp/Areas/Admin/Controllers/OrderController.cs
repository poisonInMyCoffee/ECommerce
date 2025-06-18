using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using ShoppingApp.DataAccess.Repository.IRepository;
using ShoppingApp.Models;
using ShoppingApp.Models.ViewModels;
using ShoppingApp.Utilities;

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
        public IActionResult Details(int orderId)   
        {
            OrderVM orderVM = new()
            {
                OrderHeader = _unitOfWork.OrderHeader.Get(u => u.Id == orderId, includeProperties: "ApplicationUser"),
                OrderDetail = _unitOfWork.OrderDetail.GetAll(u => u.OrderHeaderId == orderId, includeProperties: "Product")
            };
            return View(orderVM);
        }

        #region API Calls
        [HttpGet]

        public IActionResult GetAll(string status)
        {
            IEnumerable<OrderHeader> objOrderHeaders = _unitOfWork.OrderHeader.GetAll(includeProperties: "ApplicationUser").ToList();
            switch (status) {
                case "pending":
                objOrderHeaders = objOrderHeaders.Where(u => u.PaymentStatus == SD.PaymentStatusDelayedPayment);
                break;
            case "inprocess":
                objOrderHeaders = objOrderHeaders.Where(u => u.OrderStatus == SD.StatusInProcess);
                break;
            case "completed":
                objOrderHeaders = objOrderHeaders.Where(u => u.OrderStatus == SD.StatusShipped);
                break;
            case "approved":
                objOrderHeaders = objOrderHeaders.Where(u => u.OrderStatus == SD.StatusApproved);
                break;
            default:
                break;

            }


            return Json(new { data = objOrderHeaders });
          
        }

        //[HttpPost]
        //[Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
        //public IActionResult CancelOrder()
        //{

        //    var orderHeader = _unitOfWork.OrderHeader.Get(u => u.Id == OrderVM.OrderHeader.Id);

        //    if (orderHeader.PaymentStatus == SD.PaymentStatusApproved)
        //    {
        //        var options = new RefundCreateOptions
        //        {
        //            Reason = RefundReasons.RequestedByCustomer,
        //            PaymentIntent = orderHeader.PaymentIntentId
        //        };

        //        var service = new RefundService();
        //        Refund refund = service.Create(options);

        //        _unitOfWork.OrderHeader.UpdateStatus(orderHeader.Id, SD.StatusCancelled, SD.StatusRefunded);
        //    }
        //    else
        //    {
        //        _unitOfWork.OrderHeader.UpdateStatus(orderHeader.Id, SD.StatusCancelled, SD.StatusCancelled);
        //    }
        //    _unitOfWork.Save();
        //    TempData["Success"] = "Order Cancelled Successfully.";
        //    return RedirectToAction(nameof(Details), new { orderId = OrderVM.OrderHeader.Id });

        //}



        #endregion
    }
}
