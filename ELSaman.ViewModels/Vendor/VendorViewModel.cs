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
        public static VendorViewModel ToViewModel(this Vendor model)
        {
            return new VendorViewModel
            {
                ID = model.ID,
                
            };
        }

        public static VendorEditViewModel ToEditViewModel(this VendorViewModel model)
        {
            return new VendorEditViewModel()
            {
                Id = model.ID,
                IsDeleted = true,
            };
        }
    }
    public class VendorViewModel
    {
        public string? ID { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? phones { get; set; }
        public string? NameEN { get; set; }
        public string? NameAR { get; set; }
        public string? SearchText { get; set; }
        public DateTime registerDate { get; set; }
        public bool IsDeleted { get; set; } = true;
        public List<string>? MemberShipsNames { get; set; }
        public List<MemberShipViewModel>? MemberShips { get; set; }
        public virtual List<VendorMemberShip>? Vendor_MemberShips { get; set; }
    }
}
