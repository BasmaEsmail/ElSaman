using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace ELSaman.ViewModels
{
    public static partial class RestaurantExtentions
    {
        public static StoreViewModel ToViewModel(this Store model)
        {
            return new StoreViewModel
            {
                ID = model.ID,
                FromDate = model.FromDate,
                ToDate = model.ToDate,
                Vendor_ID = model.VendorID,
                NameEN = model.NameEN,
                NameAR = model.NameAR,
                RegisterDate = model.RegisterDate,
                IsDeleted = model.IsDeleted,
                ImageUrl = model.ImageUrl,
            };
        }

        public static StoreEditViewModel ToEditViewModel(this StoreViewModel model)
        {
            return new StoreEditViewModel()
            {
                FromDate = model.FromDate,
                ToDate = model.ToDate,
                Vendor_ID = model.Vendor_ID,
                NameEN = model.NameEN,
                NameAR = model.NameAR,
                RegisterDate = model.RegisterDate,
                IsDeleted = true,
                ImageUrl = model.ImageUrl,
            };
        }
    }
    public class StoreViewModel
    {
        public int ID { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string? Vendor_ID { get; set; }
        public string? NameEN { get; set; }
        public string? NameAR { get; set; }
        public DateTime RegisterDate { get; set; }
        public bool IsDeleted { get; set; }
        public string? ImageUrl { get; set; }
    }
}
