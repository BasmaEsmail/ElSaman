using ElSaman.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ElSaman.MVC.Controllers
{
    public class OrderItemController:Controller
    {
        OrderItemRepository ordRepo;

        public OrderItemController(OrderItemRepository _ordRepo)
        {
            ordRepo = _ordRepo;
        }
        public ViewResult Get(int ID = 0, string orderBy = null
            , int OrderListQty = 0, bool isAscending = false
            , int pageIndex = 1, int pageSize = 20)

        {
            var data =
                ordRepo.Get(ID, orderBy, OrderListQty, isAscending, pageIndex, pageSize);
            return View(data);
        }

        public IActionResult GetOrderDetails(int orderID, int pageIndex = 1, int pageSize = 20)
        {
            var res = ordRepo.GetOrderDetails(orderID, pageIndex, pageSize);
            return View("Get",res);
        }
    }
}
