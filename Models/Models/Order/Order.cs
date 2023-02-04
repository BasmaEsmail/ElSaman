using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Order
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public bool IsDeleted { get; set; }
        public virtual List<OrderItem>? OrderItems { get; set; }
        public string? UserId { get; set; }
        public virtual User? User { get; set; }
    }
}
