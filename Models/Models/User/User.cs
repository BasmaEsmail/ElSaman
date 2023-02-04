using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Models
{
    public class User : IdentityUser
    {
        public string? NameEN { get; set; }
        public string? NameAR { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime registerDate { get; set; }
        public bool IsDeleted { get; set; }
        public virtual List<Fav>? favs { get; set; }
        public virtual List<Cart>? Carts { get; set; }
        public virtual List<Order>? Orders { get; set; }
        public virtual List<Rate>? Rates { get; set; }
        public virtual Vendor? Vendor { get; set; }
    }
}
