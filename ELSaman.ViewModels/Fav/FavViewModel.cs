using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELSaman.ViewModels
{
    public static partial class FavExtentions
    {
        public static FavViewModel ToViewModel(this Fav model)
        {

            return new FavViewModel
            {
                ID = model.ID,
                UserID = model.UserID,
                ProductID = model.ProductID,



            };
        }
    }
        public class FavViewModel
    {
        public int ID { get; set; }
        public string? UserID { get; set; }
        public int ProductID { get; set; }
        public int Qty { get; set; }
        public string? ProductName { get; set; }
        public string? ProductImage { get; set; }
        public float ProductPrice { get; set; }

        public bool IsDeleted { get; set; }
    }
}
