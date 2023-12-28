using HR.LeaveManagment.MVC.Contracts;
using HR.LeaveManagment.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagment.MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IAuthenticationService _authService;

        public UserController(IAuthenticationService authService)
        {
            _authService = authService;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task< IActionResult> Login(LoginVm login, string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl))
            {
                returnUrl = Url.Content("~/");
            } 
            var isLoggedIn = await _authService.Authenticate(login.Email, login.Password);
            if (isLoggedIn) return LocalRedirect(returnUrl);
            ModelState.AddModelError("", "Log in attemp Failed. Please try again");
            return View(login);
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registration)
        {
            if(ModelState.IsValid)
            {
                var returnUrl = Url.Content("!/");
                var isCreated = await _authService.Register( registration);
                if(isCreated) return LocalRedirect(returnUrl);
            }
            ModelState.AddModelError("", "Registration Attempt Failed Please try again");
            return View(registration);
        }

        public async Task<IActionResult> Lougout(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl))
            {
                returnUrl = Url.Content("~/");
            }
            await _authService.logout();
            return LocalRedirect(returnUrl);
        }
    }
}
