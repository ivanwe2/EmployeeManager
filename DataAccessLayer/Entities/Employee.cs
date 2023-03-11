using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Employee : BaseHumanEntity
    {
        [Display(Name = "Employer")]
        [Required(ErrorMessage = "Employer is required")]
        [ForeignKey("Employer")]
        public Guid EmployerId { get;  set; }

        [NotMapped]
        public IEnumerable<TaskWork>? Tasks { get; set; }
        public int CompletedTaskCouner { get;  set; } //update when task is marked completed in services

    }
}
