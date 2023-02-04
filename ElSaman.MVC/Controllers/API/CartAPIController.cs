using ElSaman.Repository;
using ELSaman.ViewModels;
using ELSaman.ViewModels.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ElSaman.MVC.Controllers
{
    public class CartAPIController : ControllerBase
    {
        private readonly CartRepository cartRepository;
        private readonly UnitOfWork unitOfWork;
        public CartAPIController(CartRepository _cartRepository, UnitOfWork _unitOfWork
)
        {
            cartRepository = _cartRepository;
            unitOfWork = _unitOfWork;
        }
        public ResultViewModel Get()
        {
            var CartInfo = cartRepository.Get();
            return new ResultViewModel { Data = CartInfo, Success = true };
        }

        [HttpGet]
        public ResultViewModel GetByUser(string userId)
        {
            var CartInfo = cartRepository.GetByUser(userId);
            return new ResultViewModel { Data = CartInfo, Success = true };

        }
        [HttpGet]
        public ResultViewModel Details(int ID)
        {
            var CartInfo = cartRepository.Get(ID);
            return new ResultViewModel { Data = CartInfo, Success = true };
        }

        public ResultViewModel Add([FromBody] CartEditViewModel model)
        {
            //var UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string UserID = model.UserID;

            // var res = cartRepository.GetList().FirstOrDefault(i => i.ProductID == model.ProductID);
            var res = cartRepository.GetByUser(model.UserID).Data?.Where(i => i.ProductID == model.ProductID).FirstOrDefault();
            if (res == null)
            {
                cartRepository.Add(model);
                return new ResultViewModel
                { Data = true, Success = true };
            }
            else
            {
                return new ResultViewModel { Data = false, Success = true };
            }
        }
        [HttpPost]
        public ResultViewModel Remove([FromBody] CartEditViewModel model)
        {
            var result = cartRepository.Remove(model);

            return new ResultViewModel { Data = result, Success = true };
        }
        public ResultViewModel Update([FromBody] CartEditViewModel model)
        {

            var result = cartRepository.Update(model);
            unitOfWork.Save();

            return new ResultViewModel { Data = result, Success = true };
        }

    }
}
