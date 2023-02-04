using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace ELSaman.ViewModels
{
    public static partial class UserExtentions
    {
        public static User ToModel(this UserEditViewModel model)
        {
            return new User
            {
                NameEN = model.NameEN,
                NameAR = model.NameAR,
                Email = model.phone,
                UserName = model.phone,
                PhoneNumber = model.phone,
                ImageUrl=model.Image,
            };
        }
    }
    public class UserEditViewModel
    {
        public string? NameEN { get; set; }
        [Required, StringLength(50, MinimumLength = 3)]
        public string? NameAR { get; set; }
        public string? Email { get; set; }
        [Required, StringLength(50, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Compare("ConfirnmPassword")]
        public string? Password { get; set; }
        [Required, StringLength(50, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string? ConfirnmPassword { get; set; }
        [Required, StringLength(50, MinimumLength = 6)]
        public string? phone { get; set; }
        public string? Image { get; set; }
        public string? Role { get; set; } 
    }
}
