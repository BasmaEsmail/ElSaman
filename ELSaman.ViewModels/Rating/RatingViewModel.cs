using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace ELSaman.ViewModels
{
    public static partial class RatingExtentions
    {
        public static RatingViewModel ToViewModel(this Rate model)
        {

            return new RatingViewModel
            {
                RatingID = model.ID,
                Comment = model.Comment,
                RatingValue = model.RatingValue,
                ProductID = model.ProductID,
                UserID = model.UserID,
                IsDeleted = model.IsDeleted,
            };
        }
        public static RatingEditViewModel ToEditViewModel(this RatingViewModel model)
        {
            return new RatingEditViewModel()
            {
                RatingID = model.RatingID,
                Comment = model.Comment,
                RatingValue = model.RatingValue,
                ProductID = model.ProductID,
                UserID = model.UserID,
                IsDeleted = model.IsDeleted,
            };
        }
    }
    public class RatingViewModel
    {
        public int RatingID { get; set; }
        public string? Comment { get; set; }
        public int RatingValue { get; set; }
        public bool IsDeleted { get; set; }
        public int ProductID { get; set; }
        public string? UserID { get; set; }
    }
}
