using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace ELSaman.ViewModels
{
    public static partial class OrderListExtensions
    {
        public static OrderItemViewModel ToViewModel(this OrderItem model)
        {
            return new OrderItemViewModel
            {
                ID = model.ID,
                Qty = model.Qty,
                Price = model.Price,
                ProductID = model.ProductID,
                OrderID = model.OrderID,
            };
        }

        public static OrderItemEditViewModel ToEditViewModel(this OrderItemViewModel model)
        {
            return new OrderItemEditViewModel()
            {
                ID = model.ID,
                Qty = model.Qty,
                Price = model.Price,
                ProductID = model.ProductID,
                OrderID = model.OrderID,

            };
        }
    }
    public class OrderItemViewModel
    {
        public int ID { get; set; }
        public int Qty { get; set; }
        public float Price { get; set; }
        public int ProductID { get; set; }
        public int OrderID { get; set; }
        public string? ProductName { get; set; }

    }
}
