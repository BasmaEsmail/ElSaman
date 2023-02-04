using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Linq.Expressions;
using ELSaman.ViewModels;
using Models;
using X.PagedList;

namespace ElSaman.Repository
{
    public class OrderItemRepository:GeneralRepository<OrderItem>
    {
        private readonly UnitOfWork unitOfWork;
        private readonly ProductRepository productRepository;

        public OrderItemRepository(DBContext _Context, UnitOfWork _unitOfWork, ProductRepository productRepository) : base(_Context)
        {
            unitOfWork = _unitOfWork;
            this.productRepository = productRepository;
        }
        public PaginingViewModel<List<OrderItemViewModel>> Get(int ID = 0, string orderBy = null
            , int OrderListQty = 0, bool isAscending = false
            , int pageIndex = 1, int pageSize = 20)

        {

            var filter = PredicateBuilder.New<OrderItem>();
            var oldFiler = filter;
            if (ID > 0)
                filter = filter.Or(o => o.ID == ID);
            if (OrderListQty > 0)
                filter = filter.Or(o => o.Qty == OrderListQty);



            if (filter == oldFiler)
                filter = null;
            var query = base.Get(filter, orderBy, isAscending, pageIndex, pageSize, "Product", "Order");
            var result =
            query.Select(i => new OrderItemViewModel
            {
                ID = i.ID,
                Qty = i.Qty,
                Price = i.Price,
                ProductID = i.ProductID,
                OrderID = i.OrderID,
            });

            PaginingViewModel<List<OrderItemViewModel>>
               finalResult = new PaginingViewModel<List<OrderItemViewModel>>()
               {
                   PageIndex = pageIndex,
                   PageSize = pageSize,
                   Count = base.GetList().Count(),
                   Data = result.ToList()
               };
            return finalResult;
        }
        public PaginingViewModel<List<OrderItemViewModel>> GetByOrderID(int OrderID = 0, bool isAscending = false,
           string orderBy = null, int pageIndex = 1, int pageSize = 20)

        {

            var filter = PredicateBuilder.New<OrderItem>();
            var oldFiler = filter;
            if (OrderID > 0)
                filter = filter.Or(o => o.OrderID == OrderID);
            if (filter == oldFiler)
                filter = null;
            var query = base.Get(filter, orderBy, isAscending, pageIndex, pageSize);


            var result =
            query.Select(i => new OrderItemViewModel
            {
                ID = i.ID,
                Qty = i.Qty,
                Price = i.Price,
                ProductID = i.ProductID,
                OrderID = i.OrderID,

            });
            PaginingViewModel<List<OrderItemViewModel>>
               finalResult = new PaginingViewModel<List<OrderItemViewModel>>()
               {
                   PageIndex = pageIndex,
                   PageSize = pageSize,
                   Count = base.GetList().Count(),
                   Data = result.ToList()
               };
            return finalResult;
        }
        public OrderItemViewModel Add(OrderItemEditViewModel model)
        {
            var cart = model.ToModel();
            var result = base.Add(cart);
            unitOfWork.Save();
            return result.Entity.ToViewModel();
        }

        public IPagedList<OrderItemViewModel> GetOrderDetails(int orderID, int pageIndex, int pageSize)
        {
            var orderItems = GetByOrderID(orderID).Data.ToList();
            foreach (var o in orderItems)
            {
                o.ProductName = productRepository.GetOne(o.ProductID).NameEN;
            }
            return orderItems.ToPagedList(pageIndex, pageSize);
        }
    }
}
