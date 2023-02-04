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
    public class RatingRepository:GeneralRepository<Rate>
    {
        public RatingRepository(DBContext _Context) : base(_Context)
        {

        }
        public IPagedList<Rate> Get(int id = 0,
                    int RatingValue = 0, int? RecipeId = null,
                string orderby = "", bool isAscending = false, int pageIndex = 1,
                        int pageSize = 20, int RestaurantID = 0)
        {

            var filter = PredicateBuilder.New<Rate>();
            var oldFiler = filter;
            if (id > 0)
                filter = filter.Or(V => V.ID == id);
            if (RecipeId != null)
                filter = filter.Or(V => V.ID == RecipeId);

            if (filter == oldFiler)
                filter = null;
            var query = base.Get(filter, orderby, isAscending, pageIndex, pageSize
                );

            var result =
            query.Select(V => new Rate
            {
                RatingValue = V.RatingValue,
                Comment=V.Comment,
                ProductID=V.ProductID,
                IsDeleted=V.IsDeleted,
                UserID=V.UserID,
            }).ToPagedList(pageIndex, pageSize); ;

            //PaginingViewModel<List<RatingViewModel>>
            //    finalResult = new PaginingViewModel<List<RatingViewModel>>()
            //    {
            //        PageIndex = pageIndex,
            //        PageSize = pageSize,
            //        Count = base.GetList().Count(),
            //        Data = result.ToList()
            //    };


            return result;
        }
        public IPagedList<RatingViewModel> Search(int pageIndex = 1, int pageSize = 2)
                    =>
    GetList().Select(V => new RatingViewModel
    {
        RatingID = V.ID,
        Comment = V.Comment,
        RatingValue = V.RatingValue,

    }).ToPagedList(pageIndex, pageSize);

        public RatingViewModel Add(RatingEditViewModel model)
        {
            Rate Rating = model.ToModel();
            return base.Add(Rating).Entity.ToViewModel();
        }



        public List<TextValueViewModel> GetRecipeID() =>
            GetList().Select(i => new TextValueViewModel
            {
                Value = i.ProductID
            }).ToList();






        public RatingViewModel Update(RatingEditViewModel model)
        {

            var filterd = PredicateBuilder.New<Rate>();
            var old = filterd;

            filterd = filterd.Or(c => c.ID == model.RatingID);

            var Result = base.GetByID(filterd);
            Result.Comment = model.Comment;
            Result.RatingValue = model.RatingValue;


            return Result.ToViewModel();


        }
        public RatingViewModel GetOne(int _ID = 0)
        {
            var filterd = PredicateBuilder.New<Rate>();
            var old = filterd;
            if (_ID > 0)
                filterd = filterd.Or(c => c.ID == _ID);

            if (old == filterd)
                filterd = null;

            var query = base.GetByID(filterd);
            return query.ToViewModel();

        }

        public RatingViewModel Remove(RatingEditViewModel model)
        {
            var filterd = PredicateBuilder.New<Rate>();
            var old = filterd;
            filterd = filterd.Or(c => c.ID == model.RatingID);
            var Result = base.GetByID(filterd);
            Result.IsDeleted = true;
            return Result.ToViewModel();
        }
    }
}
