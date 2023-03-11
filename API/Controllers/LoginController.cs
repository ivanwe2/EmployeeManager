using BusinessLayer.Services.EntityServices;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    public class LoginController : Controller
    {
        private readonly EmployerService _employerService;
        public LoginController(EmployeeService employeeService, EmployerService employerService)
        {           
            _employerService = employerService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("Denied")]
        public IActionResult Denied()
        {
            return View();
        }
        [HttpGet("Login")]
        public IActionResult Login(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Validate(string email, string password, string returnUrl)
        {
            if (returnUrl == "/Logout")
            {
                returnUrl = "Home";
            }

            ViewData["ReturnUrl"] = returnUrl;
            
            if (_employerService.TryValidateEmployer(email, password, out List<Claim> claims))
            {
                
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);


                var items = new Dictionary<string, string>();
                items.Add(".AuthScheme", CookieAuthenticationDefaults.AuthenticationScheme);
                var properties = new AuthenticationProperties(items);

                await HttpContext.SignInAsync(claimsPrincipal, properties);
                return Redirect("Home/Index");
            }
            else
            {
                TempData["Error"] = "Invalid email or password!";
                return View("Login");
            }

        }

        [HttpGet("Logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        { 
                await HttpContext.SignOutAsync();
                return RedirectToAction("Index", "Home");
            

        }
    }
}
