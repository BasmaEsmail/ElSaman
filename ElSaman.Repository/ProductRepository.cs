using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Linq.Expressions;
using ELSaman.ViewModels;
using Models;
using X.PagedList;

namespace ElSaman.Repository
{
    public class ProductRepository:GeneralRepository<Product>
    {
        RatingRepository rateRepo;
        private readonly UnitOfWork UnitOfWork;

        public ProductRepository(DBContext _Context, RatingRepository _rateRepo, UnitOfWork _UnitOfWork) : base(_Context)
        {
            this.rateRepo = _rateRepo;
            this.UnitOfWork = _UnitOfWork;

            //recipe_IngredientRepository = _recipe_IngredientRepository;
        }
        public IPagedList<ProductViewModel> Get(int StoreID = 0,
            int ID = 0, string? NameAr = null, string? NameEN = null, string? orderBy = null, string ImageUrl = "", string VideoUrl = "",
            bool isAscending = false, float Price = 0, DateTime? rdate = null, string? category = null,
            int pageIndex = 1, int pageSize = 5, int CategoryID = 0)
        {
            var filter = PredicateBuilder.New<Product>();
            var oldFilter = filter;
            if (ID > 0)
                filter = filter.Or(r => r.ID == ID);
            if (!string.IsNullOrEmpty(NameAr))
                filter = filter.Or(r => r.NameAR.Contains(NameAr));
            if (!string.IsNullOrEmpty(NameEN))
                filter = filter.Or(r => r.NameEN.Contains(NameEN));
            if (rdate != null)
                filter = filter.Or(r => r.RegisterDate <= rdate);
            if (Price > 0)
                filter = filter.Or(r => r.Price <= Price);
            if (category != null)
                filter = filter.Or(r => r.Category.NameEN == category);
            if (!string.IsNullOrEmpty(ImageUrl))
                filter = filter.Or(I => I.ImageUrl.Contains(ImageUrl));
            if (!string.IsNullOrEmpty(VideoUrl))
                filter = filter.Or(I => I.ImageUrl.Contains(VideoUrl));
            if (StoreID > 0)
                filter = filter.Or(r => r.StoreID == StoreID);
            if (CategoryID > 0)
                filter = filter.Or(r => r.CategoryID == CategoryID);
            if (filter == oldFilter)
                filter = null;
            var query = base.Get(filter, orderBy, isAscending, pageIndex, pageSize);
            var result =
            query.Select(i => new ProductViewModel
            {
                ID = i.ID,
                CategoryID = i.CategoryID,
                Details = i.Details,

                IsDeleted = i.IsDeleted,
                NameAR = i.NameAR,
                NameEN = i.NameEN,
                Price = i.Price,
                RegisterDate = i.RegisterDate,
                ImageUrl = i.ImageUrl,
                StoreName = i.Store.NameEN,
                CategoryName = i.Category.NameEN,
                StoreID = i.StoreID,
                Qty=i.Qty
            }).ToPagedList(pageIndex, pageSize);
            return result;


            //PaginingViewModel<List<RecipeViewModel>>
            //    finalResult = new PaginingViewModel<List<RecipeViewModel>>()
            //    {
            //        PageIndex = pageIndex,
            //        PageSize = pageSize,
            //        Count = base.GetList().Count(),
            //        Data = result.ToList()
            //    };
            //return finalResult;

        }

        public PaginingViewModel<List<ProductViewModel>> GetAPI(int ResturantID = 0,
            string? NameAr = null, string? NameEN = null, string? orderBy = null, string ImageUrl = "", string VideoUrl = "",
            bool isAscending = false, float Price = 0, DateTime? rdate = null, string? category = null,
            int pageIndex = 1, int pageSize = 20)
        {
            var filter = PredicateBuilder.New<Product>();
            var oldFilter = filter;
            if (!string.IsNullOrEmpty(NameAr))
                filter = filter.Or(r => r.NameAR.Contains(NameAr));
            if (!string.IsNullOrEmpty(NameEN))
                filter = filter.Or(r => r.NameEN.Contains(NameEN));
            if (rdate != null)
                filter = filter.Or(r => r.RegisterDate <= rdate);
            if (Price > 0)
                filter = filter.Or(r => r.Price <= Price);
            if (ResturantID > 0)
                filter = filter.Or(r => r.StoreID == ResturantID);
            if (category != null)
                filter = filter.Or(r => r.Category.NameAR == category);
            if (!string.IsNullOrEmpty(ImageUrl))
                filter = filter.Or(I => I.ImageUrl.Contains(ImageUrl));
            if (!string.IsNullOrEmpty(VideoUrl))
                filter = filter.Or(I => I.ImageUrl.Contains(VideoUrl));
            if (filter == oldFilter)
                filter = null;


            var query = base.Get(filter, orderBy, isAscending, pageIndex, pageSize);
            var result =
            query.Select(i => new ProductViewModel
            {
                ID = i.ID,
                CategoryID = i.CategoryID,
                Details = i.Details,
                IsDeleted = i.IsDeleted,
                NameAR = i.NameAR,
                NameEN = i.NameEN,
                Price = i.Price,
                RegisterDate = i.RegisterDate,
                ImageUrl = i.ImageUrl,
                StoreName = i.Store.NameEN,
                CategoryName = i.Category.NameEN,
                StoreID = i.StoreID,
                Qty=i.Qty

            }).OrderByDescending(v=>v.RegisterDate);



            PaginingViewModel<List<ProductViewModel>>
                finalResult = new PaginingViewModel<List<ProductViewModel>>()
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    Count = base.GetList().Count(),
                    Data = result.ToList(),
                };
            return finalResult;

        }


        //public IPagedList<ProductViewModel> Search(int StoreID,int pageIndex = 1, int pageSize = 2)
        //         =>
        //    GetList().Where(i=>i.StoreID==StoreID)
        //    .Select(i => new ProductViewModel
        //    {
        //        ID = i.ID,
        //        CategoryID = i.CategoryID,
        //        Details = i.Details,
        //        IsDeleted = i.IsDeleted,
        //        NameAR = i.NameAR,
        //        NameEN = i.NameEN,
        //        Price = i.Price,
        //        RegisterDate = i.RegisterDate,
        //        ImageUrl = i.ImageUrl,
        //        StoreName = i.Store.NameEN,
        //        CategoryName = i.Category.NameEN,
        //        StoreID = i.StoreID,
        //        Qty=i.Qty

        //    }).ToPagedList(pageIndex, pageSize);

        public ProductViewModel Add(ProductEditViewModel model)
        {

            Product Product = model.ToModel();
            var result = base.Add(Product).Entity.ToViewModel();
            //foreach (var item in model.Ingredients)
            //{
            //    recipe_IngredientRepository.Add(result.ID, item);
            //}
            return result;
        }




        public ProductViewModel GetOne(int _ID = 0)
        {
            var filterd = PredicateBuilder.New<Product>();
            var old = filterd;
            if (_ID > 0)
                filterd = filterd.Or(i => i.ID == _ID);

            if (old == filterd)
                filterd = null;

            var query = base.GetByID(filterd);
            //  return query.ToViewModel();
            return new ProductViewModel
            {
                ID = query.ID,
                CategoryID = query.CategoryID,
                Details = query.Details,
                IsDeleted = query.IsDeleted,
                NameAR = query.NameAR,
                NameEN = query.NameEN,
                Price = query.Price,
                RegisterDate = query.RegisterDate,
                ImageUrl = query.ImageUrl,
                //StoreName = (query.Store != null) ? query.Store.NameEN : "",
                //CategoryName = query.Category.NameEN,
                StoreID = query.StoreID,
                Qty=query.Qty
            };
        }

        public ProductViewModel Update(ProductEditViewModel model, int ID)
        {

            var filterd = PredicateBuilder.New<Product>();
            var old = filterd;
            if (ID > 0)
                filterd = filterd.Or(i => i.ID == ID);

            if (old == filterd)
                filterd = null;

            var product = base.GetByID(filterd);

            product.NameAR = model.NameAR;
            product.NameEN = model.NameEN;
            product.Price = model.Price;
            product.ImageUrl = model.ImageUrl;
            product.StoreID = model.StoreID;
            product.CategoryID = model.CategoryID;
            product.Details = model.Details;
            product.Qty = model.Qty;

            //recipe = model.ToModel();

            return base.Update(product).Entity.ToViewModel();

        }

        public ProductViewModel Remove( int ID)
        {
            var filterd = PredicateBuilder.New<Product>();
            var old = filterd;
            if (ID > 0)
                filterd = filterd.Or(i => i.ID == ID);

            if (old == filterd)
                filterd = null;

            var product = base.GetByID(filterd);
            product.IsDeleted = true;
            var res= base.Update(product).Entity.ToViewModel();
            UnitOfWork.Save();
            return res;

        }

        public ProductViewModel AcceptProduct( int ID)
        {
            var filterd = PredicateBuilder.New<Product>();
            var old = filterd;
            if (ID > 0)
                filterd = filterd.Or(i => i.ID == ID);

            if (old == filterd)
                filterd = null;

            var product = base.GetByID(filterd);
            product.IsDeleted = false;
            var res= base.Update(product).Entity.ToViewModel();
            UnitOfWork.Save();
            return res;

        }

        double getRateByRecipeId(int id)
        {
            var res = rateRepo.Get(0, 0, id);
            double val = res.Average(r => r.RatingValue);
            return val;
        }
    }
}
