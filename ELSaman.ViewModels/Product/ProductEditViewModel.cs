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
    public static partial class RecipeExtentions
    {
        public static Product ToModel(this ProductEditViewModel model)
        {
            return new Product
            {
                ID = model.ID,
                CategoryID = model.CategoryID,
                Details = model.Details,
                IsDeleted = model.IsDeleted,
                NameAR = model.NameAR,
                NameEN = model.NameEN,
                Price = model.Price,
                RegisterDate =DateTime.Now,
                ImageUrl = model.ImageUrl,
                Qty = model.Qty,
                StoreID= model.StoreID==0?null:model.StoreID,
            };
        }
    }
    public class ProductEditViewModel
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string? ImageUrl { get; set; }
        [Required]
        public int CategoryID { get; set; }
        [Required]
        public int Qty { get; set; }
        [Required]
        public string? Details { get; set; }
        [Required]
        public float Price { get; set; }
        [Required]
        [Display(Name = "Name In English")]
        public string? NameEN { get; set; }
        [Required]
        [Display(Name = "Name In Arabic")]
        public string? NameAR { get; set; }
        [Required]
        public DateTime RegisterDate { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
        [Required]
        public string CategoryName { get; set; }

        [Required]
        public double RateValue { get; set; }
        public IFormFile? Image { get; set; }
        public Store? Store { get; set; }
        public int? StoreID { get; set; }
    }
}
