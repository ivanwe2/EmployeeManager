using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ViewModels
{
    public class EmployeeViewModel
    {
        public Guid Id { get;  set; }

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Email address is required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid email address!")]
        public string? Email { get;  set; }


        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password, ErrorMessage = "Invalid password!")]
        [MaxLength(15, ErrorMessage = "Password must be max 15 characters and min 5"), MinLength(5)]
        public string? Password { get;  set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Passwords dont match!")]
        [Compare("Password")]
        public string? ConfirmPassword { get;  set; }


        ///
        ///other properties

        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Name entry is required")]
        [MaxLength(20, ErrorMessage = "Name must be max 20 characters and min 2"), MinLength(2)]
        public string? Name { get;  set; }

        [Display(Name = "Phone number")]
        [Required(ErrorMessage = "Phone number entry is required")]
        [MaxLength(12, ErrorMessage = "Phone number must be max 12 characters and min 3"), MinLength(3)]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid pphone number entered!")]
        public string? PhoneNumber { get;  set; }


        [Display(Name = "Date of birth")]
        [Required(ErrorMessage = "Date entry is required")]
        [DataType(DataType.DateTime, ErrorMessage = "Invalid Date entered!")]
        public DateTime? DateOfBirth { get;  set; }



        [Display(Name = "Salary")]
        [Required(ErrorMessage = "Salary entry is required!")]
        [DataType(DataType.Currency, ErrorMessage = "Invalid entry!")]
        [Precision(18, 2)]
        public decimal Salary { get;  set; }
        

        [Display(Name = "Employer")]
        [Required(ErrorMessage = "Required")]
        [MaxLength(150, ErrorMessage = "Description must be max 150 characters and min 3"), MinLength(3)]
        public string? EmployerInfo { get; set; }

        public Guid EmployerId { get; set; }
        public IEnumerable<TaskViewModel>? Tasks { get; set; }

        public int CompletedTaskCouner { get; set; } //update when task is marked completed in services

    }
}
