using AutoMapper.QueryableExtensions;
using AutoMapper;
using Projection.Data;
using Projection.DTOs;
using Projection.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projection
{
    class Startup
    {
        static void Main()
        {
            // Initialize database
            var context = new EmployeeContext();
            context.Database.Initialize(true);

            Mapper.Initialize(cfg => cfg.CreateMap<Employee, EmployeeDTO>().ForMember(dto => dto.Name, dest => dest.MapFrom(src => src.FirstName + " " + src.LastName)).ForMember(dto => dto.ManagerLastName, dest => dest.MapFrom(src => src.Manager.LastName)));
            var employees = context.Employees.Where(c=> c.BirthDate.Year < 1990).ToList();
            List<EmployeeDTO> employeesDTO = Mapper.Map<List<Employee>, List<EmployeeDTO>>(employees);
            foreach (var employee in employeesDTO)
            {
                if (employee.ManagerLastName == null)
                {
                    Console.WriteLine($"{employee.Name} ${employee.Salary} Manager: [no manager]");
                }
                else
                {
                    Console.WriteLine($"{employee.Name} ${employee.Salary} Manager: {employee.ManagerLastName}");
                }
            }
        }

    }
}
