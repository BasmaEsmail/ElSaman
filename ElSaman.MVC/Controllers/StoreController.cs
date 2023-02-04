using System.Security.Claims;
using ElSaman.Repository;
using ELSaman.ViewModels;
using ELSaman.ViewModels.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ElSaman.MVC.Controllers
{
    public class StoreController:Controller
    {
        private readonly StoreRepository StoreRepo;
        private readonly UnitOfWork UnitOfWork;
        public StoreController(StoreRepository _Repo, UnitOfWork _unitOfWork)
        {
            StoreRepo = _Repo;
            UnitOfWork = _unitOfWork;
        }
        //[Authorize(Roles = "Admin,User,Vendor")]
        public IActionResult Get(string Vendor_ID = "", int id = 0, DateTime? FromTime = null, DateTime? ToTime = null,
            string NameEn = "", string NameAr = "", DateTime? registerDate = null,
            bool isDeleted = false, string orderby = "ID", bool isAscending = false,
            int pageIndex = 1, int pageSize = 20)
        {
            var Resultdata =
                StoreRepo.Get(Vendor_ID, id, FromTime, ToTime, NameEn, NameAr, registerDate, isDeleted, orderby, isAscending, pageIndex, pageSize);
            return View("Get", Resultdata);
        }
        [Authorize(Roles = "Vendor")]
        [HttpGet]
        public IActionResult Add()
        {
            var claimsIdentity = (System.Security.Claims.ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var userId = claim.Value;
            return View(new StoreEditViewModel { Vendor_ID = userId });
        }
        private IEnumerable<SelectListItem> GetRestaurantNames(List<TextValueViewModel> list)
        {
            return list.Select(i => new SelectListItem
            {
                Text = i.Text,
                Value = i.Value.ToString()
            });
        }

        [Authorize(Roles = "Vendor")]
        [HttpPost]
        public IActionResult Add(StoreEditViewModel model)
        {
            //model.
            string? UploadUrl = "/Uploads/Store/";

            string newFileName = Guid.NewGuid().ToString() + model.Image.FileName;
            model.ImageUrl = UploadUrl + newFileName;

            FileStream fs = new FileStream(Path.Combine
                (
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                   "Uploads", "Store", newFileName
                ), FileMode.Create);

            model.Image.CopyTo(fs);
            fs.Position = 0;
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            StoreRepo.Add(model);
            UnitOfWork.Save();

            return RedirectToAction("Get", new { Vendor_ID = userId });
        }
        [HttpGet]
        public IActionResult Update(int Id)
        {
            IEnumerable<SelectListItem> Restaurants =
                GetRestaurantNames(StoreRepo.GetCRestaurantDropDown());
            ViewBag.Restaurants = Restaurants;
            var Results = StoreRepo.GetOne(Id);
            return View(Results.ToEditViewModel());
        }

        [HttpPost]
        public IActionResult Update(StoreEditViewModel model, int ID = 0)
        {
            string? bookUploadUrl = "/Uploads/Store/";

            string newFileName = Guid.NewGuid().ToString() + model.Image.FileName;
            model.ImageUrl = bookUploadUrl + newFileName;

            FileStream fs = new FileStream(Path.Combine
                (
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                   "Uploads", "Product", newFileName
                ), FileMode.Create);

            model.Image.CopyTo(fs);
            fs.Position = 0;
            StoreRepo.Update(model);
            UnitOfWork.Save();
            return RedirectToAction("Get");
        }

        [HttpGet]
        public IActionResult Remove(StoreEditViewModel model, int ID)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var res = StoreRepo.Remove(model);
            UnitOfWork.Save();

            //if (!this.User.HasClaim(c => c.Value == "Vendor"))
            //{
            //    return RedirectToAction("Get", new { Vendor_ID = userId });
            //}
            //else
            return RedirectToAction("Get");

        }
        public IActionResult AcceptRestaurant(StoreEditViewModel model, int ID)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            StoreRepo.AcceptRestaurant(model, ID);
            UnitOfWork.Save();
            return RedirectToAction("Get");


        }
    }
}
