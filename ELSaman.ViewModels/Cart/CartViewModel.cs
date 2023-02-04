﻿using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELSaman.ViewModels
{
    public static partial class CartExtensions
    {
        public static CartViewModel ToViewModel(this Cart model)
        {
            return new CartViewModel
            {
                ProductID = model.ProductID,
                UserID = model.UserID,
                Qty = model.Qty,
            };
        }
    }
    public class CartViewModel
    {
        public int ID { get; set; }
        public string? UserID { get; set; }
        public int Qty { get; set; }
        public int ProductID { get; set; }
        public string? ProductName { get; set; }

        public string? ProductImage { get; set; }
        public float ProductPrice { get; set; }


        public bool IsDeleted { get; set; }
    }
}
