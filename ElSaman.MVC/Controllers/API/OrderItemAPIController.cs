using ElSaman.Repository;
using ELSaman.ViewModels;
using ELSaman.ViewModels.Shared;
using Microsoft.AspNetCore.Mvc;

namespace ElSaman.MVC.Controllers.API
{
    public class OrderItemAPIController : ControllerBase
    {
        private readonly OrderItemRepository orderListRepository;
        public OrderItemAPIController(OrderItemRepository _orderListRepository)
        {
            orderListRepository = _orderListRepository;
        }
        [HttpGet]
        public ResultViewModel Get()
        {
            var OrderListInfo = orderListRepository.Get();
            return new ResultViewModel { Data = OrderListInfo, Success = true };

        }
        [HttpGet]
        public ResultViewModel GetByOrderID(int OrderID = 0, bool isAscending = false,
           string orderBy = null, int pageIndex = 1, int pageSize = 20)
        {
            var OrderListInfo = orderListRepository.GetByOrderID(OrderID, isAscending,
           orderBy, pageIndex, pageSize);

            return new ResultViewModel { Data = OrderListInfo, Success = true };

        }
        [HttpPost]
        public ResultViewModel Add([FromBody] OrderItemEditViewModel model)
        {
            var result = orderListRepository.Add(model);
            return new ResultViewModel { Data = result, Success = true };
        }
    }
}
