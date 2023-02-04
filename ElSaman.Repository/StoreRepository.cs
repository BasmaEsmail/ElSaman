using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Linq.Expressions;
using ELSaman.ViewModels;
using ELSaman.ViewModels.Shared;
using Models;
using X.PagedList;

namespace ElSaman.Repository
{
    public class StoreRepository:GeneralRepository<Store>
    {
        RatingRepository rateRepo;
        public StoreRepository(DBContext _Context, RatingRepository _rateRepo) : base(_Context)
        {
            this.rateRepo = _rateRepo;

        }
        public IPagedList<StoreViewModel> Get(string Vendor_ID = "", int id = 0,
            DateTime? FromDate = null, DateTime? ToDate = null, string NameEn = "", string NameAr = "",
            DateTime? registerDate = null, bool isDeleted = false, string orderby = "ID",
            bool isAscending = false, int pageIndex = 1, int pageSize = 20)
        {

            var filter = PredicateBuilder.New<Store>();
            var oldFiler = filter;
            if (id > 0)
                filter = filter.Or(U => U.ID == id);
            if (!string.IsNullOrEmpty(Vendor_ID))
                filter = filter.Or(U => U.VendorID == Vendor_ID);
            if (FromDate != null)
                filter = filter.Or(d => d.FromDate <= FromDate);
            if (ToDate != null)
                filter = filter.Or(d => d.ToDate <= ToDate);
            if (!string.IsNullOrEmpty(NameEn))
                filter = filter.Or(NEn => NEn.NameEN.Contains(NameEn));
            if (!string.IsNullOrEmpty(NameAr))
                filter = filter.Or(NAR => NAR.NameAR.Contains(NameAr));
            if (registerDate != null)
                filter.Or(d => d.RegisterDate <= registerDate);
            if (isDeleted != false)
                filter = filter.Or(d => d.IsDeleted == isDeleted);
            if (filter == oldFiler)
                filter = null;
            var query = base.Get(filter, orderby, isAscending, pageIndex, pageSize
                );

            var result =
            query.Select(V => new StoreViewModel
            {
                ID = V.ID,
                FromDate = V.FromDate,
                ToDate = V.ToDate,
                NameEN = V.NameEN,
                NameAR = V.NameAR,
                RegisterDate = V.RegisterDate,
                IsDeleted = V.IsDeleted,
                ImageUrl = V.ImageUrl,
                Vendor_ID = V.VendorID
            }).ToPagedList(pageIndex, pageSize);
            return result;

            //PaginingViewModel<List<RestaurantViewModel>>

            //    finalResult = new PaginingViewModel<List<RestaurantViewModel>>()
            //    {
            //        PageIndex = pageIndex,
            //        PageSize = pageSize,
            //        Count = base.GetList().Count(),
            //        Data = result.ToList()
            //    };
            //return finalResult;

        }

        public PaginingViewModel<List<StoreViewModel>> GetAPI(string Vendor_ID = "", int id = 0,
            DateTime? FromDate = null, DateTime? ToDate = null, string NameEn = "", string NameAr = "",
            DateTime? registerDate = null, bool isDeleted = false, string orderby = "ID",
            bool isAscending = false, int pageIndex = 1, int pageSize = 20)
        {

            var filter = PredicateBuilder.New<Store>();
            var oldFiler = filter;
            if (id > 0)
                filter = filter.Or(U => U.ID == id);
            if (!string.IsNullOrEmpty(Vendor_ID))
                filter = filter.Or(U => U.VendorID == Vendor_ID);
            if (FromDate != null)
                filter = filter.Or(d => d.FromDate <= FromDate);
            if (ToDate != null)
                filter = filter.Or(d => d.ToDate <= ToDate);
            if (!string.IsNullOrEmpty(NameEn))
                filter = filter.Or(NEn => NEn.NameEN.Contains(NameEn));
            if (!string.IsNullOrEmpty(NameAr))
                filter = filter.Or(NAR => NAR.NameAR.Contains(NameAr));
            if (registerDate != null)
                filter.Or(d => d.RegisterDate <= registerDate);
            if (isDeleted != false)
                filter = filter.Or(d => d.IsDeleted == isDeleted);
            if (filter == oldFiler)
                filter = null;
            var query = base.Get(filter, orderby, isAscending, pageIndex, pageSize
                );

            var result =
            query.Select(V => new StoreViewModel
            {
                ID = V.ID,
                FromDate = V.FromDate,
                ToDate = V.ToDate,
                NameEN = V.NameEN,
                NameAR = V.NameAR,
                RegisterDate = V.RegisterDate,
                IsDeleted = V.IsDeleted,
                ImageUrl = V.ImageUrl,
                Vendor_ID = V.VendorID
            });
            PaginingViewModel<List<StoreViewModel>>

                finalResult = new PaginingViewModel<List<StoreViewModel>>()
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    Count = base.GetList().Count(),
                    Data = result.ToList()
                };
            return finalResult;

        }
        public StoreViewModel Add(StoreEditViewModel model)
        {
            Store store = model.ToModel();
            return base.Add(store).Entity.ToViewModel();
        }
        public List<TextValueViewModel> GetCRestaurantDropDown() =>
          GetList().Select(i => new TextValueViewModel
          {
              Value = i.ID,
              Text = i.NameEN
          }).ToList();
        public StoreViewModel Update(StoreEditViewModel model)
        {

            var filterd = PredicateBuilder.New<Store>();
            var old = filterd;
            filterd = filterd.Or(V => V.ID == model.ID);
            var Result = base.GetByID(filterd);
            Result.ID = model.ID;
            Result.FromDate = model.FromDate;
            Result.ToDate = model.ToDate;
            Result.NameEN = model.NameEN;
            Result.NameAR = model.NameAR;
            Result.RegisterDate = model.RegisterDate;
            Result.IsDeleted = model.IsDeleted;
            Result.ImageUrl = model.ImageUrl;
            Result.VendorID = model.Vendor_ID;
            return Result.ToViewModel();
        }
        public StoreViewModel GetOne(int _ID = 0)
        {
            var filterd = PredicateBuilder.New<Store>();
            var old = filterd;
            if (_ID > 0)
                filterd = filterd.Or(V => V.ID == _ID);
            if (old == filterd)
                filterd = null;
            var query = base.GetByID(filterd);
            return query.ToViewModel();

        }
        public StoreViewModel Remove(StoreEditViewModel model)
        {

            var filterd = PredicateBuilder.New<Store>();
            var old = filterd;

            filterd = filterd.Or(c => c.ID == model.ID);
            //if (old == filterd)
            //    filterd = null;


            var Result = base.GetByID(filterd);

            Result.IsDeleted = true;


            return Result.ToViewModel();


        }
        //public PaginingViewModel<List<RestaurantViewModel>> Search(int ID = 0, DateTime? WorkTime = null, string Vendor_ID = "",  string Name = "", string orderBy = null, 
        //    bool isAscending = false, int pageIndex = 1, int pageSize = 20)
        //{
        //    var filterd = PredicateBuilder.New<Restaurant>();
        //    var old = filterd;
        //    if (string.IsNullOrEmpty(Name))
        //        filterd = filterd.Or(b => b.NameEN.Contains(Name));
        //    filterd = filterd.Or(b => b.NameAR.Contains(Name));

        //    if (old == filterd)
        //        filterd = null;
        //    var query = base.Get(filterd, orderBy, isAscending, pageIndex, pageSize);
        //    var result =
        //    query.Select(i => new RestaurantViewModel
        //    {
        //        ID = i.ID,
        //        WorkTime = i.WorkTime,
        //        Vendor_ID = i.Vendor_ID,
        //        NameEN = i.NameEN,
        //        NameAR = i.NameAR,
        //        RegisterDate =i.RegisterDate,
        //        IsDeleted = i.IsDeleted,
        //        ImageUrl = i.ImageUrl,



        //    });

        //    PaginingViewModel<List<RestaurantViewModel>>
        //        finalResult = new PaginingViewModel<List<RestaurantViewModel>>()
        //        {
        //            PageIndex = pageIndex,
        //            PageSize = pageSize,
        //            Count = base.GetList().Count(),
        //            Data = result.ToList()
        //        };
        //    return finalResult;
        public StoreViewModel AcceptRestaurant(StoreEditViewModel model, int ID)
        {
            var filterd = PredicateBuilder.New<Store>();
            var old = filterd;
            if (ID > 0)
                filterd = filterd.Or(i => i.ID == ID);

            if (old == filterd)
                filterd = null;
            var Restaurant = base.GetByID(filterd);
            Restaurant.IsDeleted = false;
            return base.Update(Restaurant).Entity.ToViewModel();
        }
        double getRateByRecipeId(int id)
        {
            var res = rateRepo.Get(0, 0, id);
            double val = res.Average(r => r.RatingValue);
            return val;
        }
    }
}
