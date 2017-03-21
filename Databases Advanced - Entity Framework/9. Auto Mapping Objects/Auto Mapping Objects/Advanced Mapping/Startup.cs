
namespace Advanced_Mapping
{
    using DTOs;
    using Models;
    using AutoMapper;
    using System;
    using System.Collections.Generic;

    class Startup
    {
        static void Main()
        {
            var boss = new Employee();
            Mapper.Initialize(cfg => 
            {
                cfg.CreateMap<Employee, EmployeeDTO>();
                cfg.CreateMap<Employee, ManagerDTO>();
            });
            var employee1 = new Employee()
            {
                FirstName = "Gosho",
                LastName = "Goshev",
                Address = "Maritza 18",
                Salary = 3000,
                IsOnHoliday = true,
                BirthDate = Convert.ToDateTime("16/08/2016"),
                Manager = boss
                
            };
            var employee2 = new Employee()
            {
                FirstName = "Petko",
                LastName = "Goshev",
                Address = "Maritza 18",
                Salary = 5000,
                IsOnHoliday = true,
                BirthDate = Convert.ToDateTime("16/08/2016"),
                Manager = boss

            };
            var employee3 = new Employee()
            {
                FirstName = "Gosho",
                LastName = "Goshev",
                Address = "Maritza 18",
                Salary = 4600,
                IsOnHoliday = true,
                BirthDate = Convert.ToDateTime("16/08/2016"),
                
            };
            var employee4 = new Employee()
            {
                FirstName = "John",
                LastName = "Cena",
                Address = "No address",
                IsOnHoliday = true,
                BirthDate = Convert.ToDateTime("16/08/2016"),
                Manager = boss,
                Salary = 3333.33m

            };
            boss = new Employee()
            {
                FirstName = "Gosho",
                LastName = "Goshev",
                Address = "Maritza 18",
                IsOnHoliday = true,
                BirthDate = Convert.ToDateTime("16/08/2016"),
                Subordinates = new List<Employee> { employee1, employee2, employee4 }
            };
            var managers = new List<Employee>() { boss };

            var employees = boss.Subordinates;
            
            List<EmployeeDTO> employeeDTOs = Mapper.Map<List<Employee>, List<EmployeeDTO>>(employees);
            List<ManagerDTO> managerDTOs = Mapper.Map<List<Employee>, List<ManagerDTO>>(managers);

            foreach (var manager in managerDTOs)
            {
                Console.WriteLine($"{manager.FirstName} {manager.LastName} | Employees: {manager.Subordinates.Count}");
                foreach (var employee in employeeDTOs)
                {
                    Console.WriteLine($"- {employee.FirstName} {employee.LastName} ${employee.Salary}");
                }
            }
        }
    }
}
