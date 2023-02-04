using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace ELSaman.ViewModels
{
    public static partial class UserExtentions
    {
        public static UserViewModel ToViewModel(this User model)
        {

            return new UserViewModel
            {
                NameEN = model.NameEN,
                NameAR = model.NameAR,
                Email = model.Email,

            };
        }
    }
    public class UserViewModel
    {
        public string? ID { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? phone { get; set; }
        public string? NameEN { get; set; }
        public string? NameAR { get; set; }
        public string? Image { get; set; }
        public string? Role { get; set; }
        public DateTime registerDate { get; set; }
        public bool IsDelete { get; set; }
    }
}
