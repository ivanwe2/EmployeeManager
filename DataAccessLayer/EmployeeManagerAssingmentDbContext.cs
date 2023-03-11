using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class EmployeeManagerAssingmentDbContext : DbContext
    {
        public EmployeeManagerAssingmentDbContext() : base()
        {

        }

        public EmployeeManagerAssingmentDbContext(DbContextOptions<EmployeeManagerAssingmentDbContext> options)
            :base(options)
        {

        }

        //overriding to specify which db we will be using
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=EmployeeManagerDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        //overriding to insert data when model is created
        //alternative seeding
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employer>().HasData( new Employer
            {
                Id = new Guid("21f06ca6-348e-40b8-8fa1-ef5f2866ecb5"),
                Name = "Test Admin",
                PhoneNumber = "0879899248",
                DateOfBirth = DateTime.Now,
                Salary = 1234,

                Email = "admin@gmail.com",
                Password = "admin",
                ConfirmPassword = "admin"
            });

            modelBuilder.Entity<Employer>().HasData(new Employer
            {
                Id = new Guid("739f25f0-70a2-45ea-94a2-4ae4711669d4"),
                Name = "Test Admin 2",
                PhoneNumber = "0879899248",
                DateOfBirth = DateTime.Now,
                Salary = 12345,

                Email = "admin1@gmail.com",
                Password = "admin",
                ConfirmPassword = "admin"
            });

            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                Id = new Guid("dc4f1f74-2dc7-4f38-aac9-435036a86036"),
                EmployerId = new Guid("21f06ca6-348e-40b8-8fa1-ef5f2866ecb5"),
                Name = "Test Employee",
                PhoneNumber = "0879899248",
                DateOfBirth = DateTime.Parse("03.21.2020"),
                Salary = 123,

                Email = "employee@gmail.com",
                Password = "employee",
                ConfirmPassword = "employee"
            });

            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                Id = new Guid("b71ca60c-073b-4579-8068-3bd32a3b20ba"),
                EmployerId = new Guid("739f25f0-70a2-45ea-94a2-4ae4711669d4"),
                Name = "Test Employee 2",
                PhoneNumber = "0879899248",
                DateOfBirth = DateTime.Parse("03.21.2021"),
                Salary = 123,

                Email = "employee1@gmail.com",
                Password = "employee",
                ConfirmPassword = "employee"
            });

            modelBuilder.Entity<TaskWork>().HasData(new TaskWork
            {
                Id = new Guid("60c77ad4-db0b-4406-acde-9e0d0eac9921"),
                Title = "Example Test Task",
                Description = "LONG TEXT up to 150",
                DueDate = DateTime.Parse("08.21.2023"),
                AssigneeId = new Guid("dc4f1f74-2dc7-4f38-aac9-435036a86036")
            });


        }
        public  DbSet<Employer> Employers { get; set; }
        public   DbSet<Employee> Employees { get; set; }
        public  DbSet<TaskWork> Tasks { get; set; }

    }
}
