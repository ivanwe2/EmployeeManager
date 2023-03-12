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
using System.Linq;

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

            List<TaskWork> tasksToRemove = _taskService.GetAll().Where(x => x.AssigneeId == id).ToList();
            if(tasksToRemove.Any())
            {
                for (int i = 0; i < tasksToRemove.Count; i++)
                {
                    _taskService.Delete(tasksToRemove[i].Id);
                }
            }

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
            //decided not to remodel database and use what i have at hand
            //once a task is completed its due date property now stores the time of completion
            //any task marked as completed will have its due date changet to datetime.now
            var latestCompletedTasks=_taskService.GetAll().Where(x => x.IsDone == true).Where(x=>x.DueDate.Value.Month == DateTime.Now.Month).ToList();
            Dictionary<Guid,int> employeeTaskCompletedCounted = new Dictionary<Guid,int>();
            foreach (var item in latestCompletedTasks)
            {
                if(!employeeTaskCompletedCounted.ContainsKey(item.AssigneeId))
                {
                    employeeTaskCompletedCounted.Add(item.AssigneeId, 1);
                }
                else
                {
                    employeeTaskCompletedCounted[item.AssigneeId]++;
                }
            }

        
            //get all employees ids who have completed a task this month
            List<Guid> bestEmployeeGuids = new List<Guid>();

            foreach (KeyValuePair<Guid,int> item in employeeTaskCompletedCounted.OrderBy(key=> key.Value))
            {
                bestEmployeeGuids.Add(item.Key);
            }

            

            //List<Employee> listEmployees = _employeeService.GetAll().OrderByDescending(x=>x.CompletedTaskCouner).ToList();
            
            List<EmployeeViewModel> employeesVM = new List<EmployeeViewModel>();

            for (int i = 0; i < 5 && i < bestEmployeeGuids.Count; i++)            
            {
                Employee employee = _employeeService.GetById(bestEmployeeGuids[i]);
                EmployeeViewModel employeeVM = _employeeService.ConvertEntityToViewModel(employee);//convert to viewmodel

                Employer employer = _employerService.GetById(employee.EmployerId);//get employer data with appropriate service
                employeeVM.EmployerInfo = employer.Name + " " + employer.Email;
                
                
                employeesVM.Add(employeeVM);
            }
            employeesVM.Reverse();//items are ordered but from bottom to top 
            return View(employeesVM);
        }

        

    }
}
