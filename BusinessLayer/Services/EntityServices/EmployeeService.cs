
using BusinessLayer.ViewModels;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLayer.Services.EntityServices
{
    public class EmployeeService : EntityService<Employee>
    {
        public EmployeeService(IRepository<Employee> repository) : base(repository)
        {
        }

        //random extention method
        

        public EmployeeViewModel ConvertEntityToViewModel(Employee employee)
        {
            EmployeeViewModel employeeViewModel = new EmployeeViewModel();

            employeeViewModel.Id = employee.Id;
            employeeViewModel.Name = employee.Name;
            employeeViewModel.PhoneNumber = employee.PhoneNumber;
            employeeViewModel.Email = employee.Email;
            employeeViewModel.Password = employee.Password;
            employeeViewModel.ConfirmPassword = employee.ConfirmPassword;
            employeeViewModel.DateOfBirth = employee.DateOfBirth;
            employeeViewModel.Salary = employee.Salary;
            employeeViewModel.EmployerId= employee.EmployerId;
            employeeViewModel.EmployerInfo= "";//get later in controller bc a context is needed
            employeeViewModel.CompletedTaskCouner = employee.CompletedTaskCouner;
            employeeViewModel.Tasks = new List<TaskViewModel>();

            return employeeViewModel;
        }

        public Employee ConvertViewModelToEntity(EmployeeViewModel employeeVM)
        {
            Employee employee = new Employee();

            employee.Id = employeeVM.Id;
            employee.Name = employeeVM.Name;
            employee.PhoneNumber = employeeVM.PhoneNumber;
            employee.Email = employeeVM.Email;
            employee.Password = employeeVM.Password;
            employee.ConfirmPassword = employeeVM.ConfirmPassword;
            employee.DateOfBirth = employeeVM.DateOfBirth;
            employee.Salary = employeeVM.Salary;
            employee.EmployerId = employeeVM.EmployerId;//get later in controller bc a context is needed
            employee.CompletedTaskCouner = employeeVM.CompletedTaskCouner;
            employee.Tasks = new List<TaskWork>();

            return employee;
        }

        public void CreateAndInsert(string name, string email, string phone, string dob, string salary, string employerId)
        {
            EmployeeViewModel employeeViewModel = new EmployeeViewModel();

            employeeViewModel.Id = new Guid();
            employeeViewModel.Name = name;
            employeeViewModel.PhoneNumber = phone;
            employeeViewModel.Email = email;
            employeeViewModel.Password = "default";
            employeeViewModel.ConfirmPassword = "default";
            employeeViewModel.DateOfBirth = DateTime.Parse(dob);
            employeeViewModel.Salary = decimal.Parse(salary);
            employeeViewModel.EmployerId = Guid.Parse(employerId);
            employeeViewModel.EmployerInfo = "";//get later in controller bc a context is needed
            employeeViewModel.CompletedTaskCouner = 0;
            employeeViewModel.Tasks = new List<TaskViewModel>();

            Employee employee = ConvertViewModelToEntity(employeeViewModel);
            _repository.Insert(employee);

        }
    }
}
