using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Abp.Linq.Expressions;
using ELSaman.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models;
using X.PagedList;

namespace ElSaman.Repository
{
    public class UserRepository:GeneralRepository<User>
    {
        UserManager<User> userManger;
        SignInManager<User> SignInManger;
        //EmailServices EmailServices;
        IConfiguration Configuration;
        public UserRepository(DBContext _Context,
            UserManager<User> _userManger, SignInManager<User> _SignInManger, IConfiguration _Configuration) : base(_Context)

        {
            userManger = _userManger;
            SignInManger = _SignInManger;
            // EmailServices = _EmailServices;
            Configuration = _Configuration;

        }
        public PaginingViewModel<List<UserViewModel>> Get(string id = "",
            string UserName = "", string Email = "", string phones = "",
            DateTime? registerDate = null, string NameEn = "",
            string NameAr = "", string orderby = "ID",
            bool isAscending = false, int pageIndex = 1,
                        int pageSize = 20)
        {

            var filter = PredicateBuilder.New<User>();
            var oldFiler = filter;
            if (!string.IsNullOrEmpty(id))
                filter = filter.Or(U => U.Id == id);
            if (!string.IsNullOrEmpty(UserName))
                filter = filter.Or(U => U.UserName.Contains(UserName));
            if (!string.IsNullOrEmpty(Email))
                filter = filter.Or(U => U.Email.Contains(Email));
            if (!string.IsNullOrEmpty(phones))
                filter = filter.Or(U => U.PhoneNumber == phones);
            if (!string.IsNullOrEmpty(NameEn))
                filter = filter.Or(NEn => NEn.NameEN.Contains(NameEn));
            if (!string.IsNullOrEmpty(NameAr))
                filter = filter.Or(NAR => NAR.NameAR.Contains(NameAr));
            if (registerDate != null)
                filter.Or(d => d.registerDate <= registerDate);

            if (filter == oldFiler)
                filter = filter = null;
            var query = base.Get(filter, orderby, isAscending, pageIndex, pageSize
                );
            var result = query.Select(i => new UserViewModel
            {
                ID = i.Id,
                Email = i.Email,
                phone = i.PhoneNumber,
                Image = i.ImageUrl,
                NameEN = i.NameEN,
                NameAR = i.NameAR,
                IsDelete = i.IsDeleted,
            });


            PaginingViewModel<List<UserViewModel>>
               finalResult = new PaginingViewModel<List<UserViewModel>>()
               {
                   PageIndex = pageIndex,
                   PageSize = pageSize,
                   Count = base.GetList().Count(),
                   Data = result.ToList()
               };

            return finalResult;
        }
        public async Task<AccountResultViewModel> SignUp(UserEditViewModel model)
        {
            User User = model.ToModel();
            var result = await userManger.CreateAsync(User, model.Password);
            if (result.Succeeded)
            {
                result = await userManger.AddToRoleAsync(User, model.Role);
                if (result.Succeeded)
                {
                    //string token = await userManger.GenerateEmailConfirmationTokenAsync(User);
                    //string PathOfRedirectOfConfirmation
                    //=String.Format(Configuration.GetSection("Application:AppDomin").Value
                    //+Configuration.GetSection("Application:ConfirmationEmail").Value
                    //,User.Id , token);
                    //SendEmailOptions options = new SendEmailOptions()
                    //{
                    //    Subject = "Confirmation Email",
                    //    FromEmail = "Info@SauceMang.com",
                    //    FromEmailDisplayName = "Sauce App",
                    //    IsBodyHTML = true,
                    //    Body = SendEmailOptions.GenerateBodyFromTemplate("ConfirmEmail",
                    //    new Dictionary<string, string>()
                    //    {
                    //        {"{{UserName}}",  User.NameEN},
                    //        {"{{Link}}" ,PathOfRedirectOfConfirmation  }
                    //    })
                    //};
                    //options.ToEmails.Add(User.Email);
                    //await EmailServices.SendEmail(options);
                    return new AccountResultViewModel { IsSuccess = result.Succeeded, UserId = User.Id, Errors = null };

                }
            }
            return new AccountResultViewModel { IsSuccess = result.Succeeded, UserId = User.Id, Errors = result.Errors };

        }
        public async Task<String> SignIn(UserLoginViewModel model)
        {
            var result = await SignInManger.PasswordSignInAsync(model.Email, model.Password,
                model.RemmeberMe, false);
            if (result.Succeeded)
            {
                User user = await userManger.FindByEmailAsync(model.Email);
                List<Claim> claims = new List<Claim>();
                IList<string> roles = await userManger.GetRolesAsync(user);
                roles.ToList().ForEach(i => claims.Add(new Claim(ClaimTypes.Role, i)));
                JwtSecurityToken token
                     = new JwtSecurityToken
                     (
                       signingCredentials: new SigningCredentials
                         (
                             new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["JWT:Key"]))
                             , SecurityAlgorithms.HmacSha256
                         ),
                       expires: DateTime.Now.AddDays(1),
                        claims: claims
                     );
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            return string.Empty;
        }

        public async Task SignOut() =>
            await SignInManger.SignOutAsync();

     
        public UserViewModel Add(UserEditViewModel model)
        {
            User Users = model.ToModel();
            return base.Add(Users).Entity.ToViewModel();
        }
        public Task<User> getbyEmail(string email)
        {
            return userManger.FindByNameAsync(email);
        }


        public IPagedList<UserViewModel> Search(int pageIndex = 1, int pageSize = 2)
                  =>
              GetList().Where(v => v.Vendor == null).Select(i => new UserViewModel
              {
                  ID = i.Id,
                  Email = i.Email,
                  Password = i.PasswordHash,
                  phone = i.PhoneNumber,
                  NameEN = i.NameEN,
                  Image = i.ImageUrl,
                  NameAR = i.NameAR,
                  IsDelete=i.IsDeleted,
              }).ToPagedList(pageIndex, pageSize);

        public async Task<IdentityResult> ChangePassward(ChangePasswardViewModel model)
        {
            User users = await userManger.FindByIdAsync(model.Id);
            var result = await userManger.ChangePasswordAsync(users, users.PasswordHash, model.NewPassword);
            return result;
        }


        public async Task<IdentityResult> ConfirmEmail(string Id, string token) =>
             await userManger.ConfirmEmailAsync(await userManger.FindByIdAsync(Id), token);



        public async Task<IdentityResult> Update(UserViewModel result)
        {
            var filter = PredicateBuilder.New<User>();
            filter = filter.Or(p => p.Id == result.ID);
            var last = GetByID(filter);
            last.NameAR = result.NameAR;
            last.UserName = result.phone;
            last.PasswordHash = result.Password;
            last.ImageUrl = result.Image;
            return await userManger.UpdateAsync(last);
        }

        public User Remove(string Id)
        {
            var filterd = PredicateBuilder.New<User>();
            var old = filterd;
            filterd = filterd.Or(c => c.Id == Id);
            var Result = base.GetByID(filterd);
            Result.IsDeleted = true;
            return Result;
        }
    }
}
