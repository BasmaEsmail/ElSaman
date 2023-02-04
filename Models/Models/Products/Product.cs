using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Product
    {
        public int ID { get; set; }
        public string? ImageUrl { get; set; }
        public int CategoryID { get; set; }
        public string? Details { get; set; }
        public int Qty { get; set; }
        public float Price { get; set; }
        public string? NameEN { get; set; }
        public string? NameAR { get; set; }
        public DateTime RegisterDate { get; set; }
        public bool IsDeleted { get; set; }
        public virtual Category? Category { get; set; }
        public virtual List<Fav>? Favs { get; set; }
        public virtual List<Cart>? Carts { get; set; }
        public virtual List<Rate>? Rates { get; set; }
        public virtual List<OrderItem>? OrderItems { get; set; }
        public virtual int? StoreID { get; set; }
        public virtual Store? Store { get; set; }
    }
}
