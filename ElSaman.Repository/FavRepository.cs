using Abp.Linq.Expressions;
using ELSaman.ViewModels;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElSaman.Repository
{
    public class FavRepository : GeneralRepository<Fav>
    {
        private readonly UnitOfWork unitOfWork;
        public FavRepository(DBContext _Context, UnitOfWork _unitOfWork) : base(_Context)
        {
            unitOfWork = _unitOfWork;
        }
        public PaginingViewModel<List<FavViewModel>> Get(int ID = 0,
                string orderby = "", bool isAscending = false, int pageIndex = 1,
                        int pageSize = 20)
        {

            var filter = PredicateBuilder.New<Fav>();
            var oldFilter = filter;
            if (ID > 0)
                filter = filter.Or(V => V.ID == ID);


            if (filter == oldFilter)
                filter = null;
            var query = base.Get(filter, orderby, isAscending, pageIndex, pageSize, "Product"
                );

            var result =
            query.Select(V => new FavViewModel
            {
                ID = V.ID,
                ProductID = V.ProductID

            });

            PaginingViewModel<List<FavViewModel>>
                finalResult = new PaginingViewModel<List<FavViewModel>>()
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    Count = base.GetList().Count(),
                    Data = result.ToList()
                };


            return finalResult;
        }

        public FavViewModel Add(FavEditViewModel model)
        {
            Fav Fav = model.ToModel();
            var res = base.Add(Fav).Entity.ToViewModel();
            unitOfWork.Save();
            return res;
        }

        public PaginingViewModel<List<FavViewModel>> GetByUser(string userId = null,
           int pageIndex = 1, int pageSize = 20)
        {
            var filter = PredicateBuilder.New<Fav>();
            var oldFilter = filter;


            if (!string.IsNullOrEmpty(userId))
            {
                filter = filter.Or(c => c.UserID == userId);
            }

            if (filter == oldFilter)
            {
                filter = null;
            }

            var query = base.Get(filter);
            var result =
            query.Select(i => new FavViewModel
            {
                ID = i.ID,
                ProductID = i.ProductID,
                UserID = i.UserID,
            });

            PaginingViewModel<List<FavViewModel>>
                finalResult = new PaginingViewModel<List<FavViewModel>>()
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    Count = base.GetList().Count(),
                    Data = result.ToList()
                };
            return finalResult;

        }

        public FavViewModel Remove(FavEditViewModel model)
        {
            var filterd = PredicateBuilder.New<Fav>();
            var old = filterd;
            if (model.ID > 0)
                filterd = filterd.Or(i => i.ID == model.ID);

            if (old == filterd)
                filterd = null;

            var cart = base.GetByID(filterd);
            var res = base.Remove(cart);
            unitOfWork.Save();

            return res.Entity.ToViewModel();
        }

    }
}
