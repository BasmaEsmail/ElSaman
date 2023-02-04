using ElSaman.Repository;
using ELSaman.ViewModels;
using ELSaman.ViewModels.Shared;
using Microsoft.AspNetCore.Mvc;

namespace ElSaman.MVC.Controllers.API
{
    public class FavAPIController : ControllerBase
    {
        private readonly FavRepository FavRepo;

        public FavAPIController(FavRepository _FavRepo)
        {
            this.FavRepo = _FavRepo;
        }
        public ResultViewModel Get(int ID = 0,
                string orderBy = "ID", bool isAscending = false,
                int pageIndex = 1, int pageSize = 20)
        {
            PaginingViewModel<List<FavViewModel>> result = FavRepo.Get(ID, orderBy, isAscending, pageIndex, pageSize);

            return new ResultViewModel()
            {
                Success = true,
                Message = "",
                Data = result
            };

        }

        [HttpGet]
        public ResultViewModel GetByUser(string userId)
        {
            var CartInfo = FavRepo.GetByUser(userId);
            return new ResultViewModel { Data = CartInfo, Success = true };

        }

        [HttpPost]
        public ResultViewModel Add([FromBody] FavEditViewModel model)
        {
            var res = FavRepo.GetList().FirstOrDefault(i => i.ProductID == model.ProductID);
            if (res == null)
            {
                if (ModelState.IsValid == true)
                {
                    FavRepo.Add(model);
                    return new ResultViewModel()
                    {
                        Message = "Added Succesfully",
                        Success = true,
                        Data = true
                    };
                }
                else
                {
                    List<string> errors = new List<string>();
                    foreach (var i in ModelState.Values)
                        foreach (var error in i.Errors)
                            errors.Add(error.ErrorMessage);
                    return new ResultViewModel()
                    {
                        Message = "Oops! Something went wrong. ",
                        Success = false,
                        Data = false
                    };
                }
            }
            else
            {
                return new ResultViewModel { Data = false, Success = true };
            }
        }

        [HttpPost]
        public ResultViewModel Remove([FromBody] FavEditViewModel model)
        {
            var res = FavRepo.Remove(model);
            return new ResultViewModel { Data = res, Success = true };


        }
    }
}
