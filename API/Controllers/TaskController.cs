using BusinessLayer.Services.EntityServices;
using BusinessLayer.ViewModels;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace API.Controllers
{
    public class TaskController : Controller
    {
        private readonly TaskService _taskService;
        private readonly EmployeeService _employeeService;

        public TaskController(TaskService taskService, EmployeeService employeeService)
        {
            _taskService = taskService;
            _employeeService = employeeService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("pak");
            }
            TaskViewModel task = new TaskViewModel();
            task.Assignee = _employeeService.ConvertEntityToViewModel(_employeeService.GetById(id));
            

            return View("Create",task);
        }
        
        public IActionResult CreateTask(Guid id,string title,string description,DateTime due)
        {        

            _taskService.Create(new TaskWork
            {
                Id = new Guid(),
                Title = title,
                Description = description,  
                DueDate = due,
                IsDone = false,
                AssigneeId = id,
            });

            return View("Index");
        }
        public IActionResult SeeTasks(Guid id)
        {
            var tasks = _taskService.GetAll().Where(task => task.AssigneeId == id);

            List<TaskViewModel> tasksVM= new List<TaskViewModel>();
            foreach (var item in tasks)
            {
                tasksVM.Add(new TaskViewModel
                {
                    Id = item.Id,
                    Title = item.Title,
                    Description = item.Description,
                    DueDate = item.DueDate,
                    Assignee = _employeeService.ConvertEntityToViewModel(_employeeService.GetById(id)),//not needed in this case
                    IsDone = item.IsDone
                });
            }
            

            return View(tasksVM);
        }
        public IActionResult MarkAsDone(Guid id)
        {
            var task = _taskService.GetById(id);
            task.IsDone = true;
            task.DueDate= DateTime.Now;
           
            var employee = _employeeService.GetById(task.AssigneeId);
            employee.CompletedTaskCouner += 1;

            _taskService.Update(task);
            _employeeService.Update(employee);
            return View("Index");
        }

        public IActionResult Edit(Guid id)
        {
            TaskWork a =_taskService.GetById(id);
            TaskViewModel b = new TaskViewModel()
            {
                Id = id,
                Title = a.Title,
                Description = a.Description,
                DueDate = DateTime.Now,
                IsDone = a.IsDone,
                Assignee = new EmployeeViewModel()
            };
            return View(b);
        }
        public IActionResult EditTask(Guid id,string title,string description,DateTime due)
        {
            TaskWork task = _taskService.GetById(id);
            task.Title = title;
            task.Description = description;
            task.DueDate = due;
            _taskService.Update(task);
            return View("Index");
        }
    }
}
