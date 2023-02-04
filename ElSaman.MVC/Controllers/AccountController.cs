using Microsoft.AspNetCore.Mvc;

namespace ElSaman.MVC.Controllers
{
    public class AccountController:Controller
    {
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
