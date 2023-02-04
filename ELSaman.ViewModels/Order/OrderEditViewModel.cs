using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELSaman.ViewModels
{
    public static partial class OrderExtentions
    {
        public static Order ToModel(this OrderEditViewModel model)
        {
            return new Order
            {
                ID = model.ID,
                Date = model.Date,
                IsDeleted = model.IsDeleted,
                UserId = model.UserId,
                OrderItems = model.OrderItems.Select(e => e.ToModel()).ToList()
            };
        }
    }
    public class OrderEditViewModel
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
        [Required]
        public string? UserId { get; set; }
        public List<OrderItemEditViewModel>? OrderItems { get; set; }

    }
}
