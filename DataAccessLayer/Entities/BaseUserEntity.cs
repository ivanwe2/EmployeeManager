using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    
    public class BaseUserEntity
    {
        /// <summary>
        /// Login details
        /// </summary>
        [Key]
        public Guid Id { get; internal set; }

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Email address is required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid email address!")]
        public string? Email { get; internal set; }


        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password, ErrorMessage = "Invalid password!")]
        [MaxLength(15, ErrorMessage = "Password must be max 15 characters and min 5"), MinLength(5)]
        public string? Password { get; internal set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Passwords dont match!")]
        [Compare("Password")]
        public string? ConfirmPassword { get; internal set; }


        ///
        ///other properties

        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Name entry is required")]
        [MaxLength(20, ErrorMessage = "Name must be max 20 characters and min 2"), MinLength(2)]
        public string? Name { get; internal set; }

        [Display(Name = "Phone number")]
        [Required(ErrorMessage = "Phone number entry is required")]
        [MaxLength(12, ErrorMessage = "Phone number must be max 12 characters and min 3"), MinLength(3)]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid pphone number entered!")]
        public string? PhoneNumber { get; internal set; }


        [Display(Name = "Date of birth")]
        [Required(ErrorMessage = "Date of birth entry is required")]
        [DataType(DataType.DateTime, ErrorMessage = "Invalid Date entered!")]
        public DateTime? DateOfBirth { get; internal set; }



        [Display(Name = "Salary")]
        [Required(ErrorMessage = "Salary entry is required!")]
        [DataType(DataType.Currency, ErrorMessage = "Invalid entry!")]
        [Precision(18, 2)]
        public decimal Salary { get; internal set; }

    }
}
