using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELSaman.ViewModels
{
    public static partial class OrderExtentions
    {
        public static OrderViewModel ToViewModel(this Order model)
        {

            return new OrderViewModel
            {
                ID = model.ID,
                UserId = model.UserId,
                Date = model.Date,
                IsDeleted = model.IsDeleted,
                OrderItems = model.OrderItems.Select(o=>o.ToViewModel()).ToList()

            };
        }
        public static OrderEditViewModel ToEditViewModel(this OrderViewModel model)
        {
            return new OrderEditViewModel()
            {
                ID = model.ID,
                UserId = model.UserId,
                Date = model.Date,
                IsDeleted = model.IsDeleted,
                OrderItems = model.OrderItems.Select(o => o.ToEditViewModel()).ToList()
            };
        }
    }
    public class OrderViewModel
    {
    public int ID { get; set; }
    public DateTime Date { get; set; }
    public bool IsDeleted { get; set; }
    public string? UserId { get; set; }
    public List<OrderItemViewModel>? OrderItems { get; set; }
    public string? CustomerName { get; set; }
    public string? Phone { get; set; }
    public float? TotalPrice { get; set; }


    }
}
