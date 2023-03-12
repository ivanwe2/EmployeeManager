using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Initializer
{
    public class DbInitialiser
    {
        private readonly EmployeeManagerAssingmentDbContext _employeeManagerAssingmentDbContext;

        public DbInitialiser(EmployeeManagerAssingmentDbContext employeeManagerAssingmentDbContext)
        {
            _employeeManagerAssingmentDbContext = employeeManagerAssingmentDbContext;
        }
        public void Run()
        {
            _employeeManagerAssingmentDbContext.Database.Migrate();
        }
    }
}
