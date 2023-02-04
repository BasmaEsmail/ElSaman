using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Models;

namespace ELSaman.ViewModels
{
    public static partial class StoreExtentions
    {
        public static Store ToModel(this StoreEditViewModel model)
        {
            return new Store
            {
                ID = model.ID,
                FromDate = model.FromDate,
                ToDate = model.ToDate,
                VendorID = model.Vendor_ID,
                NameEN = model.NameEN,
                NameAR = model.NameAR,
                RegisterDate = model.RegisterDate,
                IsDeleted = true,
                ImageUrl = model.ImageUrl,
            };
        }
    }
    public class StoreEditViewModel
    {
        public int ID { get; set; }
        [Required]
        public DateTime FromDate { get; set; }
        [Required]
        public DateTime ToDate { get; set; }
        [Required]
        public string? Vendor_ID { get; set; }
        [Required]
        public string? NameEN { get; set; }
        [Required]
        public string? NameAR { get; set; }
        public DateTime RegisterDate { get; set; }
        public bool IsDeleted { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile? Image { get; set; }
    }
}
