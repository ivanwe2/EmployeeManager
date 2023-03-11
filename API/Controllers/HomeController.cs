using API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BusinessLayer;
using BusinessLayer.Services;
using DataAccessLayer.Entities;
using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;
using BusinessLayer.Services.EntityServices;

namespace API.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            
        }

        //public HomeController(IEntityService<Employee> service)
        //{
        //    _service = service;
        //}

        public IActionResult Index()
        {
            return View();
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}