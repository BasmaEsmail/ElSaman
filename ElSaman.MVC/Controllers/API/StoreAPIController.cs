using ElSaman.Repository;
using ELSaman.ViewModels.Shared;
using Microsoft.AspNetCore.Mvc;

namespace ElSaman.MVC.Controllers.API
{
    public class StoreAPIController:ControllerBase
    {
        private readonly StoreRepository ResRepo;
        private readonly UnitOfWork UnitOfWork;
        public StoreAPIController(StoreRepository _RepoRes, UnitOfWork _unitOfWork)
        {
            //DBContext dBContext = new DBContext();
            this.ResRepo = _RepoRes;
            UnitOfWork = _unitOfWork;
        }
        //[Authorize(Roles = "Admin,User,Vendor")]
        public ResultViewModel Get(string vendorID = "", int id = 0, DateTime? FromTime = null, DateTime? ToTime = null, string NameEn = "", string NameAr = "", DateTime? registerDate = null, bool isDeleted = false, string orderby = "ID", bool isAscending = false, int pageIndex = 1, int pageSize = 20)
        {
            var Resultdata =
                ResRepo.GetAPI(vendorID, id, FromTime, ToTime, NameEn, NameAr, registerDate, isDeleted, orderby, isAscending, pageIndex, pageSize);
            return new ResultViewModel()
            {
                Success = true,
                Message = "",
                Data = Resultdata
            };
        }
    }
}
