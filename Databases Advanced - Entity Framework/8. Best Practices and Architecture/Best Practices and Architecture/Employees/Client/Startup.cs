using Employees.Data;
using System;
using System.Diagnostics;
using System.Linq;

namespace Employees
{
    class Startup
    {
        static void Main(string[] args)
        {
            EmployeesContext context = new EmployeesContext();

            Stopwatch stopwatch = new Stopwatch();

            long timePassed = 0L;

            int testCount = 10; // Amount of tests to perform

            for (int i = 0; i < testCount; i++)

            {

                // Clear all query cache

                context.Database.ExecuteSqlCommand("CHECKPOINT; DBCC DROPCLEANBUFFERS; ");

                stopwatch.Start();

                //QueryWithEagerLoading(context);
                //QueryWithLazyLoading(context);

                //QueryWithEagerLoadingSelect(context);
                //QueryWithLazyLoadingSelect(context);

                //SalaryBelow3000Lazy(context);
                //SalaryBelow3000Eager(context);

                //EmployeesWithOneProjectLazy(context);
                //EmployeesWithOneProjectEager(context);

                //OrderBy1(context);
                //OrderBy2(context);

                OptimizeQuery(context);

                stopwatch.Stop();

                timePassed += stopwatch.ElapsedMilliseconds;

                stopwatch.Reset();

            }

            TimeSpan averageTimePassed = TimeSpan.FromMilliseconds(timePassed / (double)testCount);

            Console.WriteLine(averageTimePassed);
        }

        private static void OptimizeQuery(EmployeesContext context)
        {
            var employees = context.Employees.Where(c => c.Subordinates.Any(s => s.Address.Town.Name.StartsWith("B"))).Select(c => c.FirstName).Distinct().ToList();
            foreach (var emp in employees)
            {
                string result = $"{emp}";
            }
        }

        private static void OrderBy2(EmployeesContext context)
        {
            var employees = context.Employees.ToList().OrderBy(d => d.Department.Name).ThenBy(d => d.FirstName);
            foreach (var emp in employees)
            {
                var result = $"{emp.FirstName} - {emp.Department.Name}";
            }
        }

        private static void OrderBy1(EmployeesContext context)
        {
            var employees = context.Employees.OrderBy(d => d.Department.Name).ThenBy(d => d.FirstName).ToList();
            foreach (var emp in employees)
            {
                var result = $"{emp.FirstName} - {emp.Department.Name}";
            }
        }

        private static void EmployeesWithOneProjectEager(EmployeesContext context)
        {
            var employees = context.Employees.Include("Project").Include("Department").Include("EmployeesProject").Where(c => c.EmployeesProjects.Count == 1).Select(c => new { Name = c.FirstName + " " + c.LastName, DepartmentName = c.Department.Name }).ToList();
            foreach (var e in employees)
            {
                var result = $"{e.Name} - {e.DepartmentName}";
            }
        }

        private static void EmployeesWithOneProjectLazy(EmployeesContext context)
        {
            var employees = context.Employees.Where(c => c.EmployeesProjects.Count == 1).Select(c => new { Name = c.FirstName + " " + c.LastName, DepartmentName = c.Department.Name }).ToList();
            foreach (var e in employees)
            {
                var result = $"{e.Name} - {e.DepartmentName}";
            }
        }

        private static void SalaryBelow3000Eager(EmployeesContext context)
        {
            var employees = context.Employees.Include("Department").Include("Address").Where(c => c.Salary < 3000).Select(c => new { FirstName = c.FirstName, DepartmentName = c.Department.Name, Adress = c.Address.AddressText }).ToList();

            foreach (var emp in employees)
            {
                string result = $"{emp.FirstName} - {emp.DepartmentName} - {emp.Adress}";
            }
        }

        private static void SalaryBelow3000Lazy(EmployeesContext context)
        {
            var employees = context.Employees.Where(c => c.Salary < 3000).Select(c => new { FirstName = c.FirstName, DepartmentName = c.Department.Name, Adress = c.Address.AddressText }).ToList();

            foreach (var emp in employees)
            {
                string result = $"{emp.FirstName} - {emp.DepartmentName} - {emp.Adress}";
            }
        }

        private static void QueryWithLazyLoadingSelect(EmployeesContext context)
        {
            var employees = context.Employees.Select(c => new { FirstName = c.FirstName, DepartmentName = c.Department.Name, Adress = c.Address.AddressText }).ToList();

            foreach (var emp in employees)
            {
                string result = $"{emp.FirstName} - {emp.DepartmentName} - {emp.Adress}";
            }
        }

        private static void QueryWithEagerLoadingSelect(EmployeesContext context)
        {
            var employees = context.Employees.Include("Department").Include("Address").Select(c => new { FirstName = c.FirstName, DepartmentName = c.Department.Name, Adress = c.Address.AddressText }).ToList();

            foreach (var emp in employees)
            {
                string result = $"{emp.FirstName} - {emp.DepartmentName} - {emp.Adress}";
            }
        }

        private static void QueryWithEagerLoading(EmployeesContext context)
        {
            var employees = context.Employees.Include("Department").Include("Address").ToList();

            foreach (var emp in employees)
            {
                string result = $"{emp.FirstName} - {emp.Department.Name} - {emp.Address.AddressText}";
            }
        }

        private static void QueryWithLazyLoading(EmployeesContext context)
        {
            var employees = context.Employees.ToList();

            foreach (var emp in employees)
            {
                string result = $"{emp.FirstName} - {emp.Department.Name} - {emp.Address.AddressText}";
            }
        }
    }
}
