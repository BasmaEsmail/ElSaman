using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace ELSaman.ViewModels
{
    public static partial class ProductExtentions
    {
        public static ProductViewModel ToViewModel(this Product model)
        {
            return new ProductViewModel
            {
                ID = model.ID,
                CategoryID = model.CategoryID,
                Details = model.Details,
                IsDeleted = model.IsDeleted,
                NameAR = model.NameAR,
                NameEN = model.NameEN,
                Price = model.Price,
                RegisterDate = model.RegisterDate,
                ImageUrl = model.ImageUrl,
                StoreID = model.StoreID==0?null:model.StoreID,
                Qty=model.Qty
                
            };
        }
    }
    public class ProductViewModel
    {
        public int ID { get; set; }
        public string? ImageUrl { get; set; }
        public int CategoryID { get; set; }
        public int Qty { get; set; }

        public string? Details { get; set; }
        public float Price { get; set; }
        public string? NameEN { get; set; }
        public double RateValue { get; set; }
        public string? CategoryName { get; set; }
        public string? NameAR { get; set; }
        public DateTime RegisterDate { get; set; }
        public bool IsDeleted { get; set; }
        public string? StoreName { get; set; }
        public int? StoreID { get; set; }
    }
}
