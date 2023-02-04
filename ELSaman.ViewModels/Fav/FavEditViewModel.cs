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
        public static Fav ToModel(this FavEditViewModel model)
        {
            return new Fav
            {

                ID = model.ID,
                UserID = model.UserID,
                ProductID = model.ProductID,
            };
        }
    }
    public class FavEditViewModel
    {
        public int ID { get; set; }
        public string? UserID { get; set; }
        public int ProductID { get; set; }
    }
}
