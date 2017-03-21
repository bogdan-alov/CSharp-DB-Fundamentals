namespace Projection.Data
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using static EmployeeContext;

    public class EmployeeContext : DbContext
    {
        public EmployeeContext()
            : base("name=EmployeeContext")
        {
           Database.SetInitializer(new Initliazer());
        }

        public class Initliazer : DropCreateDatabaseAlways<EmployeeContext>
        {
            protected override void Seed(EmployeeContext context)
            {
                
                var employee1 = new Employee()
                {
                    FirstName = "Gosho",
                    LastName = "Goshev",
                    Address = "Maritza 15-16",
                    BirthDate = Convert.ToDateTime("16/06/2016"),
                    Salary = 15000
                };
                

                var employee2 = new Employee()
                {
                    FirstName = "Petko",
                    LastName = "Peshev",
                    BirthDate = Convert.ToDateTime("16/06/2015"),
                    Manager = employee1,
                    Salary = 1000.33m
                };
                var employee3 = new Employee()
                {
                    FirstName = "John",
                    LastName = "Cena",
                    Address = "Kolumbiano 16",
                    BirthDate = Convert.ToDateTime("16/05/2000"),
                    Manager = employee1,
                    Salary = 500
                };
                var employee4 = new Employee()
                {
                    FirstName = "Malinka",
                    LastName = "Malinkova",
                    Address = "Maritza 15-16",
                    BirthDate = Convert.ToDateTime("16/06/2016"),
                    Salary = 300000
                };
                var employee5 = new Employee()
                {
                    FirstName = "Boncho",
                    LastName = "Georgiev",
                    Address = "Maritza 15-16",
                    BirthDate = Convert.ToDateTime("16/06/2016"),
                    Salary = 15000
                };

                var employee6 = new Employee()
                {
                    FirstName = "Gosho",
                    LastName = "Chernev",
                    Address = "Maritza 15-16",
                    BirthDate = Convert.ToDateTime("16/06/2016"),
                    Manager = employee2,
                    Salary = 15000
                };
                var employee7 = new Employee()
                {
                    FirstName = "Gosho",
                    LastName = "Batkov",
                    Address = "Maritza 15-16",
                    BirthDate = Convert.ToDateTime("16/06/1980"),
                    Salary = 300000000
                };
                var employee8 =    new Employee()
                {
                    FirstName = "Gosho",
                    LastName = "Geshov",
                    BirthDate = Convert.ToDateTime("16/06/1987"),
                    Manager = employee2,
                    Salary = 20000
                };

                context.Employees.Add(employee1);
                context.Employees.Add(employee2);
                context.Employees.Add(employee3);
                context.Employees.Add(employee4);
                context.Employees.Add(employee5);
                context.Employees.Add(employee6);
                context.Employees.Add(employee7);
                context.Employees.Add(employee8);
                base.Seed(context);
            }
        }
        public virtual DbSet<Employee> Employees { get; set; }
    }
}