using ElSaman.Repository;
using ELSaman.ViewModels;
using ELSaman.ViewModels.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ElSaman.MVC.Controllers
{
    public class ProductController:Controller
    {
        private readonly ProductRepository ProductRepo;
        private readonly CategoryRepository CatRepo;
        private readonly UnitOfWork UnitOfWork;
        private readonly VendorRepository VendorRepo;
        private readonly StoreRepository StoreRepo;

        public ProductController(ProductRepository _productRepo, UnitOfWork _unitOfWork,
            VendorRepository _VendorRepo, CategoryRepository _CatRepo, StoreRepository _StoreRepo)
        {
            ProductRepo = _productRepo;
            UnitOfWork = _unitOfWork;
            VendorRepo = _VendorRepo;
            CatRepo = _CatRepo;
            StoreRepo = _StoreRepo;
        }
        //[Authorize(Roles = "Admin,User,Vendor")]
        public IActionResult Get(int ID = 0, string NameAr = null, string NameEN = null,
            string orderBy = null, string ImageUrl = "", string VideoUrl = "",
            bool isAscending = false, float Price = 0, DateTime? rdate = null, string category = null,
            int pageIndex = 1, int pageSize = 20, int StoreID = 0, int CategoryID = 0)
        {
            var data = ProductRepo.Get( StoreID,
                ID, NameAr, NameEN, orderBy, ImageUrl, VideoUrl,
                isAscending, Price, rdate, category, pageIndex, pageSize, CategoryID);
            ViewBag.Store = StoreID;
            ViewBag.Category = CategoryID;
            return View(data);
        }
        //[Authorize(Roles = "Admin,Vendor")]
        //public IActionResult Search(int StoreID , int pageIndex = 1, int pageSize = 4)
        //{

        //    var result = ProductRepo.Get(StoreID);
        //    return View("Get", result);

        //}
        //[Authorize(Roles = "Admin,Vendor")]
        [HttpGet]
        public IActionResult Add(string? returnUrl, int? StoreId)
        {
            ViewBag.ReturnUrl = returnUrl;
            IEnumerable<SelectListItem> Categories =
                GetCateogriesNames(CatRepo.GetCategoriesDropDown());
            ViewBag.Categories = Categories;
            return View(new ProductEditViewModel { StoreID = StoreId });
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
       
        //[Authorize(Roles = "Admin,Vendor")]
        [HttpPost]
        public IActionResult Add(ProductEditViewModel model, int StoreID)
        {

            string? UploadUrl = "/Uploads/Product/";

            string newFileName = Guid.NewGuid().ToString() + model.Image.FileName;
            model.ImageUrl = UploadUrl + newFileName;

            FileStream fs = new FileStream(Path.Combine
                (
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                   "Uploads", "Product", newFileName
                ), FileMode.Create);

            model.Image.CopyTo(fs);
            fs.Position = 0;
            model.StoreID = StoreID;
            ProductRepo.Add(model);
            UnitOfWork.Save();
            if (StoreID > 0)
                return RedirectToAction("Get", new { StoreID = StoreID });
            else
                return RedirectToAction("Get");

        }
        [HttpGet]
        public IActionResult Update(int ID)
        {
            IEnumerable<SelectListItem> Categories = GetCateogriesNames(CatRepo.GetCategoriesDropDown());
            IEnumerable<SelectListItem> Restaurants = GetRestaurantNames(StoreRepo.GetCRestaurantDropDown());
            ViewBag.Categories = Categories;
            ViewBag.Restaurants = Restaurants;
            var product = ProductRepo.GetOne(ID);
            ViewBag.CurrentProduct = product;
            return View();
        }
        [HttpPost]
        public IActionResult Update(ProductEditViewModel model, int ID)
        {
            string? bookUploadUrl = "/Uploads/Product/";

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
            ProductRepo.Update(model, ID);
            UnitOfWork.Save();
            return RedirectToAction("Get");

        }
        public IActionResult Remove( int ID, int StoretID)
        {
            ProductRepo.Remove(ID);
            //UnitOfWork.Save();
            //return RedirectToAction("Get" , RestaurantID);
            if (StoretID > 0)
            {
                return RedirectToAction("Get", new { StoretID = StoretID });
            }
            else
            {
                return RedirectToAction("Get");
            }

        }
        public IActionResult AcceptProduct( int ID, int StoreID)
        {

            ProductRepo.AcceptProduct(ID);
            UnitOfWork.Save();
            //return RedirectToAction("Get", RestaurantID);
            if (StoreID > 0)
            {
                return RedirectToAction("Get", new { StoreID = StoreID });
            }
            else
            {
                return RedirectToAction("Get");
            }
        }

        public IActionResult RemoveSearch(int ID, int RestaurantID)
        {
            ProductRepo.Remove(ID);
            UnitOfWork.Save();
            //return RedirectToAction("Get" , RestaurantID);
            if (RestaurantID > 0)
            {
                return RedirectToAction("Get", new { RestaurantID = RestaurantID });
            }
            else
            {
                return RedirectToAction("Get");
            }

        }
       
    }
}
