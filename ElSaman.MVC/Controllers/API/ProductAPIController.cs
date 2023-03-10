using ElSaman.Repository;
using ELSaman.ViewModels.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ElSaman.MVC.Controllers.API
{
    public class ProductAPIController:ControllerBase
    {
        private readonly ProductRepository RecipeRepo;
        public ProductAPIController(ProductRepository _RecipeRepo)
        {
            this.RecipeRepo = _RecipeRepo;
        }

        //[Authorize(Roles = "Admin,User,Vendor")]
        [HttpGet]
        public ResultViewModel GetAPI(int ID = 0, int ResturantID = 0, string NameAr = null, string NameEN = null,
            string orderBy = null, string ImageUrl = "", string VideoUrl = "",
            bool isAscending = false, float Price = 0, DateTime? rdate = null, string category = null,
            int pageIndex = 1, int pageSize = 20)
        {
            var data = RecipeRepo.GetAPI(ResturantID,
                NameAr, NameEN, orderBy, ImageUrl, VideoUrl,
                isAscending, Price, rdate, category, pageIndex, pageSize);
            return new ResultViewModel()
            {
                Success = true,
                Message = "",
                Data = data
            };
        }
        [HttpGet]
        public ResultViewModel GetAll()
        {
            var data = RecipeRepo.GetList();
            return new ResultViewModel()
            {
                Success = true,
                Message = "",
                Data = data
            };
        }
        public ResultViewModel GetDetails(int id)
        {
            var data = RecipeRepo.GetOne(
               id);
            return new ResultViewModel()
            {
                Success = true,
                Message = "",
                Data = data
            };
        }
        private IEnumerable<SelectListItem> GetCateogriesNames(List<TextValueViewModel> list)
        {
            return list.Select(i => new SelectListItem
            {
                Text = i.Text,
                Value = i.Value.ToString()
            });
        }

        private IEnumerable<SelectListItem> GetRestaurantNames(List<TextValueViewModel> list)
        {
            return list.Select(i => new SelectListItem
            {
                Text = i.Text,
                Value = i.Value.ToString()
            });
        }
    }
}
