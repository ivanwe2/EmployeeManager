using BusinessLayer.ViewModels;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.EntityServices
{
    public class EmployerService : EntityService<Employer>
    {
        public EmployerService(IRepository<Employer> repository) : base(repository)
        {
        }
       
        public bool TryValidateEmployer(string email, string password, out List<Claim> claims)
        {
            claims = new List<Claim>();
            var currentEmployer = _repository.GetAll().Where(x => x.Email == email).Where(x => x.Password == password).FirstOrDefault();
            
            if (currentEmployer == null)
            {
                return false;
            }
            else
            {
                claims.Add(new Claim("Id", currentEmployer.Id.ToString()));
                claims.Add(new Claim("Name", currentEmployer.Name));
                claims.Add(new Claim("Phone", currentEmployer.PhoneNumber));
                claims.Add(new Claim("Salary", currentEmployer.Salary.ToString()));
                claims.Add(new Claim("DateOfBirth", currentEmployer.DateOfBirth.ToString()));
                claims.Add(new Claim("Password",  currentEmployer.Password));

                claims.Add(new Claim(ClaimTypes.Email, currentEmployer.Email));
                return true;

            }
        }

        //obratnotot syshto za creating

        //used to fetch information from services to viewmodels to actual views
        public EmployerViewModel ConvertEntityToViewModel(Employer employer)
        {
            EmployerViewModel employerViewModel = new EmployerViewModel();

            employerViewModel.Id=employer.Id;
            employerViewModel.Name=employer.Name;
            employerViewModel.PhoneNumber = employer.PhoneNumber;
            employerViewModel.Email = employer.Email;
            employerViewModel.Password = employer.Password;
            employerViewModel.ConfirmPassword=employer.ConfirmPassword;
            employerViewModel.DateOfBirth = employer.DateOfBirth;
            employerViewModel.Salary=employer.Salary;
            employerViewModel.Employees = employer.Employees;

            return employerViewModel;
        }

        public Employer ConvertViewModelToEntity(EmployerViewModel employerVM)
        {
            Employer employer = new Employer();

            employer.Id = employerVM.Id;
            employer.Name = employerVM.Name;
            employer.PhoneNumber = employerVM.PhoneNumber;
            employer.Email = employerVM.Email;
            employer.Password = employerVM.Password;
            employer.ConfirmPassword = employerVM.ConfirmPassword;
            employer.DateOfBirth = employerVM.DateOfBirth;
            employer.Salary = employerVM.Salary;          

            return employer;
        }
    }
}
