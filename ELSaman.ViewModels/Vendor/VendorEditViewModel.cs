using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace ELSaman.ViewModels
{
    public static partial class VendorExtentions
    {
        public static Vendor ToModel(this VendorEditViewModel model)
        {
            return new Vendor
            {
                ID = model.Id,
                registerDate = model.registerDate,
                IsDeleted = true,

            };
        }
    }
    public class VendorEditViewModel
    {
        public List<string>? Phones;
        public string? Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ConfirnmPassword { get; set; }
        public string? NameEN { get; set; }
        public string? NameAR { get; set; }
        public string? Role { get; set; }
        public DateTime registerDate { get; set; }
        public bool IsDeleted { get; set; } = true;
        public string? Search { get; set; }
        public virtual List<VendorMemberShip>? Vendor_MemberShips { get; set; }
        public virtual List<Store>? Stores { get; set; }
    }
}
