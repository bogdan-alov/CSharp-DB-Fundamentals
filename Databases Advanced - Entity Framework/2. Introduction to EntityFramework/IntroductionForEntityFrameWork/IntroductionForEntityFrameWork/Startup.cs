

namespace IntroductionForEntityFrameWork
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    class Startup
    {
       
        static void Main()
        {
            using (var context = new SoftUniContext())
            {
                FindEmployeesInPeriod(context);
            }
        }

        static void FindEmployeesInPeriod(SoftUniContext context)
        {
            var employyes = context.Employees
                .Where(e => e.Projects.Count(p => p.StartDate.Year >= 2001 && p.StartDate.Year <= 2003) > 0)
                .Take(30);
            foreach (var e in employyes)
            {
                Console.WriteLine($"{e.FirstName} {e.LastName} {e.Manager.FirstName}");
                foreach (var p in e.Projects)
                {
                    Console.WriteLine($"--{p.Name} {p.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture)} {p.EndDate:M'/'d'/'yyyy h:mm:ss tt}");
                }
            }
        }
        static void AllEmployees(SoftUniContext context)
        {
            var employees = context.Employees.ToList();
            foreach (var employee in employees)
            {
                Console.WriteLine($"{employee.FirstName} {employee.LastName} {employee.MiddleName} {employee.JobTitle} {employee.Salary}");
            }
        }
        static void EmployeesWithSalaryOver5000(SoftUniContext context)
        {
            var employees = context.Employees.Where(c => c.Salary >= 50000).ToList();
            foreach (var employee in employees)
            {
                Console.WriteLine($"{employee.FirstName}");
            }
        }
        static void EmployeesFromSeattle(SoftUniContext context)
        {
            var employees = context.Employees.Where(c => c.Department.Name == "Research and Development").ToList().OrderBy(c => c.Salary).ThenByDescending(c => c.FirstName);
            foreach (var emp in employees)
            {
                Console.WriteLine($"{emp.FirstName} {emp.LastName} from {emp.Department.Name} - ${emp.Salary:f2}");
            }
        }
        static void AddingNewAddresAndUpdatingEmployee(SoftUniContext context)
        {
            var newAddress = new Address();
            newAddress.TownID = 4;
            newAddress.AddressText = "Vitoshka 15";

            var nakov = context.Employees.Where(c => c.LastName == "Nakov").FirstOrDefault();
            nakov.Address = newAddress;
            var employees = context.Employees.OrderByDescending(c => c.AddressID).Take(10).ToList();
            foreach (var item in employees)
            {
                Console.WriteLine(item.Address.AddressText);
            }
        }
        static void EmployeeWithId147(SoftUniContext context)
        {
            var employee = context.Employees.Where(e => e.EmployeeID == 147).FirstOrDefault();
            

            Console.WriteLine($"{employee.FirstName} {employee.LastName} {employee.JobTitle}");

            foreach (var projects in employee.Projects.OrderBy(p=> p.Name))
            {
                Console.WriteLine(projects.Name);
            }

        }

        static void AddressesByTownName(SoftUniContext context)
        {
            var addresses = context.Addresses.OrderByDescending(c=> c.Employees.Count).ThenBy(c=> c.Town.Name).Take(10).ToList();

            foreach (var address in addresses)
            {
                
                Console.WriteLine($"{address.AddressText}, {address.Town.Name} - {address.Employees.Count} employees");
            }
        }

        static void DepartmentsWithMoreThan5Employees(SoftUniContext context)
        {
            var departments = context.Departments.Where(d => d.Employees.Count > 5).OrderBy(d => d.Employees.Count).ToList();

            foreach (var d in departments)
            {
                Console.WriteLine($"{d.Name} {d.ManagerID}");

                foreach (var emp in d.Employees)
                {
                    Console.WriteLine($"{emp.FirstName} {emp.LastName} {emp.JobTitle}");
                }
            }
        }
        static void Find10LatestProjects(SoftUniContext context)
        {
            var projects = context.Projects.OrderByDescending(d => d.StartDate).ThenBy(d => d.Name).Skip(2).Take(10).ToList();
         
            foreach (var p in projects)
            {
                Console.WriteLine($"{p.Name} {p.Description} {p.StartDate} {p.EndDate}");
                Console.WriteLine();
            }
        }
        static void IncreaseSalaries(SoftUniContext context)
        {
            var employees = context.Employees.Where(e => e.Department.Name == "Engineering" || e.Department.Name == "Tool Design" || e.Department.Name == "Marketing" || e.Department.Name == "Information Services").ToList();

            foreach (var emp in employees)
            {
                emp.Salary += emp.Salary * 0.12m;
                Console.WriteLine($"{emp.FirstName} {emp.LastName} (${emp.Salary})");
            }
        }
        static void FindEmployeesByFirstNameStartingWithSa(SoftUniContext context)
        {
            var employees = context.Employees.Where(e => e.FirstName.StartsWith("Sa")).ToList();

            foreach (var emp in employees)
            {
                Console.WriteLine($"{emp.FirstName} {emp.LastName} - {emp.JobTitle} - (${emp.Salary})");
            }
        }

        static void DeleteProjectById(SoftUniContext context)
        {
            var project = context.Projects.Where(p => p.ProjectID == 2).FirstOrDefault();
            var projects = context.Projects.ToList();
            projects.Remove(project);
            foreach (var p in projects.Take(10))
            {
                Console.WriteLine(p.Name);
            }


        }

        static void NativeSQLQuery(SoftUniContext context)
        {
            var timer = new Stopwatch();
            timer.Start();
            PrintNamesWithLinq(context);
            timer.Stop();
            Console.WriteLine($"Linq: {timer.Elapsed}");

            timer.Restart();
            PrintNamesWithNativeSQL(context);
            Console.WriteLine($"Native SQL: {timer.Elapsed}");

        }

        private static void PrintNamesWithNativeSQL(SoftUniContext context)
        {
            

            var projects = context.Projects.SqlQuery("SELECT * FROM Projects WHERE YEAR(StartDate) = 2002").ToList();

            foreach (var p in projects)
            {
                foreach (var emp in p.Employees)
                {
                    Console.WriteLine(emp.FirstName);
                }
            }
        }

        private static void PrintNamesWithLinq(SoftUniContext context)
        {
            
            var projects = context.Projects.Where(c => c.StartDate.Year == 2002).ToList();

            foreach (var p in projects)
            {
                foreach (var emp in p.Employees)
                {
                    Console.WriteLine(emp.FirstName);
                }
            }
        }
    }
}
