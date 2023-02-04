using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Rate
    {
        public int ID { get; set; }
        public string? Comment { get; set; }
        public int RatingValue { get; set; }
        public int ProductID { get; set; }
        public string? UserID { get; set; }
        public bool IsDeleted { get; set; }
        public virtual Product? Product { get; set; }
        public virtual User? User { get; set; }
    }
}
