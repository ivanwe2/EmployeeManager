using BusinessLayer.Services.EntityServices;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace API.Controllers
{
    public class RegisterController : Controller
    {
        private readonly EmployerService _employerService;
        
        public RegisterController( EmployerService employerService)
        {
            _employerService = employerService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult RegisterEmployerAsync(string email,string pass,string cpass,string name,string phone,DateTime dob,decimal salary)
        {
            if(_employerService.GetAll().Any(i => i.Email==email))
            {
                TempData["Error"] = "Email already in use";
                return View("Register");
            }
            _employerService.Create(new Employer
            {
                Email = email,
                Password = pass,
                ConfirmPassword = cpass,
                Name = name,
                PhoneNumber = phone,
                DateOfBirth = dob,
                Salary = salary
            });
            return View("ProceedLogin");
        }
    }
}
