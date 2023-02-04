using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class OrderItem
    {
        public int ID { get; set; }
        public int Qty { get; set; }
        public float Price { get; set; }//subprice
        public int OrderID { get; set; }
        public bool IsDeleted { get; set; }
        public virtual Order? Order { get; set; }
        public int ProductID { get; set; }
        public virtual Product? Product { get; set; }
    }
}
