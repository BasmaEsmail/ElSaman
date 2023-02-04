using ElSaman.Repository;
using ELSaman.ViewModels;
using ELSaman.ViewModels.Shared;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace ElSaman.MVC.Controllers.API
{
    public class OrderAPIController:ControllerBase
    {
        OrderRepository ordRepo;
        private readonly UnitOfWork UnitOfWork;
        CartRepository cartRepository;
        ProductRepository productRepository;
        OrderItemRepository orderListRepository;

        public OrderAPIController(OrderRepository _ordRepo, CartRepository _cartRepository,
             ProductRepository _productRepository, OrderItemRepository _orderListRepository, UnitOfWork _unitOfWork)
        {
            //DBContext dBContext = new DBContext();
            ordRepo = _ordRepo;
            UnitOfWork = _unitOfWork;
            cartRepository = _cartRepository;
            productRepository = _productRepository;
            orderListRepository = _orderListRepository;

        }
        [HttpGet]
        public ResultViewModel Get(int ID = 0, string orderBy = null, bool isAscending = false,
            string UserId = "", DateTime? registerDate = null, int pageIndex = 1, int pageSize = 20)

        {
            var data =
                ordRepo.GetAPI(ID, orderBy, isAscending, UserId, registerDate, pageIndex, pageSize);
            return new ResultViewModel()
            {
                Success = true,
                Message = "",
                Data = data
            };
        }

        [HttpGet]
        public ResultViewModel GetLastOrder(int ID = 0, string orderBy = null, bool isAscending = false,
            string UserId = "", DateTime? registerDate = null, int pageIndex = 1, int pageSize = 20)

        {
            var data =
                ordRepo.GetAPI(ID, orderBy, isAscending, UserId, registerDate, pageIndex, pageSize);
            int OrderID = 0;
            foreach (var o in data.Data)
            {
                if (o.Date.ToString("MM-dd-yyyy") == DateTime.Now.ToString("MM-dd-yyyy"))
                {
                    OrderID = o.ID;
                }
            }
            return new ResultViewModel()
            {
                Success = true,
                Message = "",
                Data = OrderID
            };
        }



        public ResultViewModel GetById(int id)
        {
            var data =
           ordRepo.Get(id);
            return new ResultViewModel()
            {
                Success = true,
                Message = "",
                Data = data
            };
        }



        // [Authorize(Roles = "Admin")]
        [HttpGet]
        public ResultViewModel Add()
        {
            // List<TextValueViewModel> Values =  ordRepo.GetRecipeID();
            return new ResultViewModel()
            {
                Success = true,
                Message = "",
                Data = null
            };
        }
        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public ResultViewModel Add([FromBody] OrderEditViewModel model)
        {
            var userCarts = cartRepository.GetByUser(model.UserId);
           // int i = 0;
            foreach (var c in userCarts.Data)
            {
                var product = productRepository.GetOne(c.ProductID);
                //model.orderLists[i].OrderID = model.ID;
                //model.OrderItems[i].Price = product.Price;
                //model.OrderItems[i].Qty = c.Qty;
                //model.OrderItems[i].ProductID = product.ID;
                model.OrderItems.Add(new OrderItemEditViewModel
                {
                    OrderID = product.ID,
                    Qty = c.Qty,
                    ProductID = product.ID,
                    Price = product.Price,
                });

               // i++;
            }
            model.Date = DateTime.Now;
            ordRepo.Add(model);


            //model.Date = DateTime.Now;
            //var res = ordRepo.Add(model);
            //foreach (var c in userCarts.Data)
            //{
            //    var product = productRepository.GetOne(c.ProductID);
            //    orderListRepository.Add(new OrderItemEditViewModel
            //    {
            //        OrderID = res.ID,
            //        Qty = c.Qty,
            //        ProductID = c.ProductID,
            //        Price = product.Price,

            //    });
            //}
            //ordRepo.Add(model);


            foreach (var c in userCarts.Data)
            {
                cartRepository.Remove(c.ID);
            }

            UnitOfWork.Save();
            return new ResultViewModel()
            {
                Message = "Added Succesfully",
                Success = true,
                Data = null
            };

        }


        [HttpPost]
        public ResultViewModel Remove([FromBody]OrderEditViewModel model, int ID)
        {
            var res = ordRepo.Remove(model);
            UnitOfWork.Save();
            return new ResultViewModel()
            {
                Message = "Removed Succesfully",
                Success = true,
                Data = null
            };

        }
    }
}
