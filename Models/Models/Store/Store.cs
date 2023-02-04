using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Store
    {
        public int ID { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string? VendorID { get; set; }
        public string? StorePhone { get; set; }
        public string? StoreLocation { get; set; }
        public string? NameEN { get; set; }
        public string? NameAR { get; set; }
        public DateTime RegisterDate { get; set; }
        public bool IsDeleted { get; set; }
        public string? ImageUrl { get; set; }
        public virtual Vendor? Vendor { get; set; }
        public virtual List<Product>? Products { get; set; }
    }
}
