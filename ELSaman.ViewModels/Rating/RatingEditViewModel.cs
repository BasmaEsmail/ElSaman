using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace ELSaman.ViewModels
{
    public static partial class RatingExtentions
    {
        public static Rate ToModel(this RatingEditViewModel model)
        {
            return new Rate
            {
                ID = model.RatingID,
                Comment = model.Comment,
                RatingValue = model.RatingValue,
                ProductID = model.ProductID,
                UserID = model.UserID,
                IsDeleted = model.IsDeleted,
            };
        }
    }
    public class RatingEditViewModel
    {
        [Required]
        public int RatingID { get; set; }
        [Required]
        public string? Comment { get; set; }
        [Required]
        public int RatingValue { get; set; }
        public bool IsDeleted { get; set; }
        [Required]
        public int ProductID { get; set; }
        [Required]
        public string? UserID { get; set; }
    }
}
