using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ViewModels
{
    public class TaskViewModel
    {
        public Guid Id { get;  set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "Title is required")]
        [MaxLength(20, ErrorMessage = "Title must be max 20 characters and min 2"), MinLength(2)]
        public string? Title { get;  set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description entry is required")]
        [MaxLength(150, ErrorMessage = "Description must be max 150 characters and min 3"), MinLength(3)]
        public string? Description { get;  set; }

        [Display(Name = "Assignee")]
        [Required(ErrorMessage = "Assignee is required")]
        public EmployeeViewModel? Assignee { get;  set; }


        [Display(Name = "Due date")]
        [Required(ErrorMessage = "Due date entry is required")]
        [DataType(DataType.DateTime, ErrorMessage = "Invalid Date entered!")]
        public DateTime? DueDate { get;  set; }

        private bool isDone;//def false
        public bool IsDone { get { return isDone; } set { isDone = value; } }//default false


    }
}
