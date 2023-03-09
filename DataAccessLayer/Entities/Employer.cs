using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Employer : BaseUserEntity
    {
        [NotMapped]
        public IEnumerable<Employee> Employees { get; set; }

        public Employer()
        {
            Employees = new List<Employee>();
        }
    }
}
