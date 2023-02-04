using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Extensions;
using Abp.Linq.Expressions;
using ELSaman.ViewModels;
using Models;
using X.PagedList;

namespace ElSaman.Repository
{
    public class VendorRepository:GeneralRepository<Vendor>
    {
        private readonly Vendor_MembershipRepository vendorMemberRepo;
        private readonly MemberShipRepository memberShipRepository;
        private readonly StoreRepository storeRepository;
        private readonly OrderItemRepository orderListRepository;
        private readonly ProductRepository ProductRepository;
        private readonly OrderRepository orderRepository;
        private readonly UserRepository userRepository;

        public VendorRepository(DBContext _Context, Vendor_MembershipRepository _vendorMemberRepo,
            MemberShipRepository _memberShipRepository, StoreRepository storeRepository,
            OrderItemRepository orderListRepository, ProductRepository ProductRepository,
            UserRepository userRepository, OrderRepository orderRepository

            )
            : base(_Context)
        {
            vendorMemberRepo = _vendorMemberRepo;
            memberShipRepository = _memberShipRepository;
            this.ProductRepository = ProductRepository;
            this.orderListRepository = orderListRepository;
            this.storeRepository = storeRepository;

            this.userRepository = userRepository;
            this.orderRepository = orderRepository;

        }
        public PaginingViewModel<List<VendorViewModel>> Get(string id = "",
            string nameEN = "", string nameAR = "", string Email = "", string phone = "",
                string orderby = "ID", bool isAscending = false, int pageIndex = 1,
                        int pageSize = 5)
        {

            var filter = PredicateBuilder.New<Vendor>();
            var oldFiler = filter;

            if (!string.IsNullOrEmpty(id))
                filter = filter.Or(V => V.ID == id);
            if (!string.IsNullOrEmpty(nameEN))
                filter = filter.Or(V => V.User.NameEN.Contains(nameEN));

            if (!string.IsNullOrEmpty(nameAR))
                filter = filter.Or(V => V.User.NameAR.Contains(nameAR));
            if (!string.IsNullOrEmpty(Email))
                filter = filter.Or(V => V.User.Email.Contains(Email));
            if (!string.IsNullOrEmpty(phone))
                filter = filter.Or(V => V.User.PhoneNumber.Any(i => i.Equals(phone)));

            if (filter == oldFiler)
                filter = null;
            var query = base.Get(filter, orderby, isAscending, pageIndex, pageSize, "VendorMemberShips");



            var result =
            query.Select(V => new VendorViewModel
            {
                ID = V.ID,
                UserName = V.User.UserName,

                NameEN = V.User.NameEN,
                NameAR = V.User.NameAR,
                Email = V.User.Email,
                IsDeleted = V.IsDeleted,
                phones = V.User.PhoneNumber,
                MemberShips = getMemberShipName(V.VendorMemberShips),

            }).ToList();

            PaginingViewModel<List<VendorViewModel>>
                finalResult = new PaginingViewModel<List<VendorViewModel>>()
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    Count = base.GetList().Count(),
                    Data = result,
                };
            return finalResult;
        }
        public IPagedList<VendorViewModel> Search(int pageIndex = 1, int pageSize = 2)
                    =>
                GetList().Select(V => new VendorViewModel
                {
                    ID = V.ID,
                    UserName = V.User.UserName,

                    NameEN = V.User.NameEN,
                    NameAR = V.User.NameAR,
                    Email = V.User.Email,
                    IsDeleted = V.IsDeleted,
                    phones = V.User.PhoneNumber,
                }).ToPagedList(pageIndex, pageSize);
        public VendorViewModel Add(VendorEditViewModel model)
        {
            /////
            Vendor Vendor = model.ToModel();
            return base.Add(Vendor).Entity.ToViewModel();
        }
        public VendorViewModel Update(VendorEditViewModel model)
        {
            var filterd = PredicateBuilder.New<Vendor>();
            var old = filterd;
            filterd = filterd.Or(i => i.ID == model.Id);
            var Result = base.GetByID(filterd);
            Result.User.UserName = model.UserName;
            Result.User.NameEN = model.NameEN;
            Result.User.NameAR = model.NameAR;
            Result.User.Email = model.Email;
            return Result.ToViewModel();
        }
        public VendorViewModel GetOne(string _ID = "")
        {
            var filterd = PredicateBuilder.New<Vendor>();
            var old = filterd;
            if (!string.IsNullOrEmpty(_ID))
                filterd = filterd.Or(i => i.ID == _ID);
            if (old == filterd)
                filterd = null;

            var query = base.GetByID(filterd);
            var ms = vendorMemberRepo.Get(query.ID);
            return new VendorViewModel
            {
                ID = query.ID,
                
                MemberShips = getMemberShipName(ms),
            };
        }
        public VendorViewModel Remove(string Id)
        {
            var filterd = PredicateBuilder.New<Vendor>();
            var old = filterd;
            filterd = filterd.Or(c => c.ID == Id);
            var Result = base.GetByID(filterd);
            Result.IsDeleted = true;
            return Result.ToViewModel();
        }
        public void AcceptVendor(string ID)
        {
            var filterd = PredicateBuilder.New<Vendor>();
            var old = filterd;
            if (!string.IsNullOrEmpty(ID))
                filterd = filterd.Or(i => i.ID == ID);

            if (old == filterd)
                filterd = null;
            var vendor = base.GetByID(filterd);
            vendor.IsDeleted = false;
            var res = base.Update(vendor);
        }
        List<MemberShipViewModel> getMemberShipName(List<VendorMemberShip> vendorMemberShips)
        {
            var res = new List<MemberShipViewModel>();
            foreach (var item in vendorMemberShips)
            {
                res.Add(memberShipRepository.GetOne(item.MemberShipID));
            }
            return res;
        }

        public IPagedList<OrderViewModel> GetOrders(string Vendor_ID, int pageIndex = 1, int pageSize = 20)
        {
            var rest = storeRepository.GetAPI(Vendor_ID);
            var productIDs = GetVendorProducts(rest.Data);
            List<OrderItemViewModel> VendorOrderItems = new List<OrderItemViewModel>();
            var AllOrderItems = orderListRepository.Get().Data;
            var VendorOrders = new List<OrderViewModel>();

            foreach (var o in AllOrderItems)
            {
                foreach (var rID in productIDs)
                {
                    if (o.ProductID == rID)
                        VendorOrderItems.Add(o);
                }
            }

            if (VendorOrderItems.Count != 0)
            {
                foreach(var o in VendorOrderItems)
                {
                    var ord = orderRepository.Get().Where(ord => ord.ID == o.OrderID).FirstOrDefault();
                   
                        VendorOrders.Add(ord);

                }

                foreach (var order in VendorOrders)
                {
                    
                    order.CustomerName = userRepository.Get(order.UserId).Data.FirstOrDefault().NameEN;
                    order.Phone = userRepository.Get(order.UserId).Data.FirstOrDefault().phone;

                }
            }
            VendorOrderItems.GroupBy(x => x.ID).Select(group => group.First());
            var distinctOrders = VendorOrders.GroupBy(x => x.ID).Select(o=> o.First());


            return distinctOrders.ToPagedList(pageIndex, pageSize);


        }
        
        private List<int> GetVendorProducts(List<StoreViewModel> VendorStore)
        {
            List<int> productIDs = new List<int>();
            foreach (var r in VendorStore)
            {
                foreach (var rec in ProductRepository.GetList())
                {
                    if (rec.StoreID == r.ID)
                        productIDs.Add(rec.ID);
                }
            }
            return productIDs;
        }
    }
}
