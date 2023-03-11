using API.Models;
using BusinessLayer.Services;
using BusinessLayer.Services.EntityServices;
using BusinessLayer.ViewModels;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System;
using System.Security.Claims;

namespace API.Controllers
{
    public class EmployeeController : Controller
    {
        //implement viewmodels instead of using entities
        //private readonly IEntityService<Employee> _employeeService;
        //public EmployeeController(IEntityService<Employee> entityService)
        //{
        //    _employeeService = entityService;
        //}

        private readonly EmployeeService _employeeService;
        private readonly EmployerService _employerService;
        private readonly TaskService _taskService;
        public EmployeeController(EmployeeService employeeService,EmployerService employerService, TaskService taskService)
        {
            _employeeService = employeeService;
            _employerService = employerService;
            _taskService = taskService;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Delete(Guid id)
        {
            var employee = _employeeService.GetById(id);

            EmployeeViewModel employeeVM = _employeeService.ConvertEntityToViewModel(employee);
            return View(employeeVM);
        }
        public IActionResult DeleteEmployee(Guid id)
        {
            _employeeService.Delete(id);
            
            return View("Index");
        }


        public IActionResult Edit(Guid id)
        {
            var employee = _employeeService.GetById(id);

            EmployeeViewModel employeeVM = _employeeService.ConvertEntityToViewModel(employee);
            return View(employeeVM);

        }

        public IActionResult EditSubmit(Guid id,string email,string name,string phone,DateTime dob,decimal salary)
        {
            var employee = _employeeService.GetById(id);
            employee.Name=name;
            employee.Salary=salary;
            employee.Email = email;
            employee.DateOfBirth = dob;
            employee.PhoneNumber = phone;

            _employeeService.Update(employee);
            return View("Index");
        }
        

        public IActionResult CreateEmployee()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult Create(string name,string phone, string email,string dob,string salary)
        {
            string? id = User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
            _employeeService.CreateAndInsert(name,email,phone,dob,salary,id);

            return View("Index");
        }

        public IActionResult ListYourEmployees()
        {
            string? id = User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
            List<Employee> employees = _employeeService.GetAll().Where(x => x.EmployerId == Guid.Parse(id)).ToList();
            List<EmployeeViewModel> employeesVM = new List<EmployeeViewModel>();
            foreach (var item in employees)
            {
                EmployeeViewModel employeeVM = _employeeService.ConvertEntityToViewModel(item);//convert to viewmodel
                employeesVM.Add(employeeVM);
            }
            return View(employeesVM);
        }
        public IActionResult ListBestEmployees()
        {
            //FIX URGENT
            
            List<Employee> listEmployees = _employeeService.GetAll().OrderByDescending(x=>x.CompletedTaskCouner).ToList();
            List<EmployeeViewModel> employeesVM = new List<EmployeeViewModel>();
            for (int i = 0; i < 5 && i < listEmployees.Count; i++)            
            {
                EmployeeViewModel employeeVM = _employeeService.ConvertEntityToViewModel(listEmployees[i]);//convert to viewmodel

                Employer employer = _employerService.GetById(listEmployees[i].EmployerId);//get employer data with appropriate service
                employeeVM.EmployerInfo = employer.Name + " " + employer.Email;
                
                
                employeesVM.Add(employeeVM);
            }
            
            return View(employeesVM);
        }

        

    }
}
