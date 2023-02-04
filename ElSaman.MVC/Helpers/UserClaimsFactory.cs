using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Models;

namespace ElSaman.MVC.Helpers
{
    public class UserClaimsFactory : UserClaimsPrincipalFactory<User, IdentityRole>
    {
        UserManager<User> UserManager;
        public UserClaimsFactory(UserManager<User> _userManager, RoleManager<IdentityRole> _roleManager,
            IOptions<IdentityOptions> optionsAccessor) : base(_userManager, _roleManager, optionsAccessor)
        {
            UserManager = _userManager;
        }
        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User User)
        {
            var claims = await base.GenerateClaimsAsync(User);
            var result = await UserManager.GetRolesAsync(User);
            List<string> roles = result.ToList();

            foreach (string role in roles)
                claims.AddClaim(new Claim(role, role));

            return claims;
        }
    }
}
