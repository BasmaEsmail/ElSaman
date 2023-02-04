using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace ElSaman.Repository
{
    public class Vendor_MembershipRepository:GeneralRepository<VendorMemberShip>
    {
        public Vendor_MembershipRepository(DBContext _Context) : base(_Context)
        {

        }
        public VendorMemberShip Add(string VendorID, int MembershipID)
        {
            VendorMemberShip VM = new VendorMemberShip()
            {
                VendorID = VendorID,
                MemberShipID = MembershipID
            };
            return base.Add(VM).Entity;

        }
        public List<VendorMemberShip> Get(string VendorID)
        {

            return base.GetList().Where(i => i.VendorID == VendorID).ToList();

        }
    }
}
