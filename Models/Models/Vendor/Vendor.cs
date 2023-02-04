using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Vendor
    {
        public List<string>? Phones;
        public string ID { get; set; }
        public DateTime registerDate { get; set; }
        public bool IsDeleted { get; set; }
        public virtual User? User { get; set; }
        public virtual List<VendorMemberShip>? VendorMemberShips { get; set; }
        public virtual List<Store>? Stores { get; set; }
    }
}
