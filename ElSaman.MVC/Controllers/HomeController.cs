using System.Diagnostics;
using Abp.Linq.Expressions;
using ElSaman.MVC.Models;
using ElSaman.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace ElSaman.MVC.Controllers
{
    public class HomeController : Controller
    {
        public ProductRepository ProductRepo;
        public UserRepository UserRepo;
        public VendorRepository VendorRepo;
        public StoreRepository StoreRepo;
        public OrderRepository ordRepo;
        public MemberShipRepository MemberShipRepo;
        private readonly Vendor_MembershipRepository vendorMemberRepo;


        public HomeController(UserRepository _UserRepository, ProductRepository _ProductRepo, VendorRepository _VendorRepo
            , StoreRepository _StoreRepo, MemberShipRepository _memberShipRepository
            , Vendor_MembershipRepository _vendorMemberRepo, OrderRepository _OrderRepo)
        {
            ProductRepo = _ProductRepo;
            UserRepo = _UserRepository;
            VendorRepo = _VendorRepo;
            StoreRepo = _StoreRepo;
            MemberShipRepo = _memberShipRepository;
            vendorMemberRepo = _vendorMemberRepo;
            ordRepo = _OrderRepo;
        }
        [Authorize(Roles = "Admin,Vendor")]
        public IActionResult Index()
        {
            var claimsIdentity = (System.Security.Claims.ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var userId = claim.Value;

            ViewBag.ProductCount = ProductRepo.GetList().Count();
            ViewBag.UserCount = UserRepo.GetList().Count();
            ViewBag.VendorCount = VendorRepo.GetList().Count();
            ViewBag.StoreCount = StoreRepo.GetList().Count();
            ViewBag.Bronze = MemberShipRepo.GetList().FirstOrDefault(i => i.TypeEn == "Bronze");
            ViewBag.Silver = MemberShipRepo.GetList().FirstOrDefault(i => i.TypeEn == "Silver");
            ViewBag.Golden = MemberShipRepo.GetList().FirstOrDefault(i => i.TypeEn == "Golden");
            ViewBag.Free = MemberShipRepo.GetList().FirstOrDefault(i => i.TypeEn == "Free");

            var data = ordRepo.GetList();
            List<Order> list = new List<Order>();
            foreach (var i in data)
            {
                if (i.Date.ToString("MM-dd-yyyy") == DateTime.Now.ToString("MM-dd-yyyy"))
                {
                    list.Add(i);
                }
            }
            if (list.Count() == 0)
                ViewBag.New = false;

            else
                ViewBag.New = true;



            if (this.User.HasClaim(c => c.Value == "Vendor"))
            {
                var Vendor = VendorRepo.GetList().FirstOrDefault(i => i.ID == userId);
                if (Vendor != null)
                    ViewBag.Flag = Vendor.IsDeleted;
            }
            else
                ViewBag.Flag = false;
            ///////
            var vm = vendorMemberRepo.GetList().FirstOrDefault(i => i.VendorID == userId);
            var filterd = PredicateBuilder.New<Vendor>();
            var old = filterd;
            filterd = filterd.Or(i => i.ID == userId);
            if (old == filterd)
                filterd = null;
            var v = VendorRepo.GetByID(filterd);
            if (vm != null && v.IsDeleted)
                ViewBag.Waiting = true;
            else
                ViewBag.Waiting = false;



            if (this.User.HasClaim(c => c.Value == "Admin"))
            {
                ViewBag.RecipeCount = ProductRepo.GetList().Count();
                ViewBag.UserCount = UserRepo.GetList().Count();
                ViewBag.VendorCount = VendorRepo.GetList().Count();
                ViewBag.StoreCount = StoreRepo.GetList().Count();
                ViewBag.OrderCount = ordRepo.GetList().Count();


            }
            else
            {
                // var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                ViewBag.ReataurantCount = StoreRepo.GetList().Where(v => v.VendorID == userId).Count();
                ViewBag.OrderCount = VendorRepo.GetOrders(userId).Count();





            }

            // return new ObjectResult(ViewBag);
            return View();
        }
    }
}