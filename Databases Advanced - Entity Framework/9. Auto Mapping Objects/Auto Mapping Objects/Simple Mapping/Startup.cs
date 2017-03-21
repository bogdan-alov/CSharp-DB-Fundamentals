namespace Simple_Mapping
{
    using AutoMapper;
    using DTOs;
    using Models;
    using System;

    class Startup
    {
        static void Main()
        {
            var employee = new Employee()
            {
                FirstName = "Pesho",
                LastName = "Peshev",
                Address = "Tintyava 15-17",
                BirthDate = Convert.ToDateTime("21/03/2017"),
                Salary = 4444
            };
            Mapper.Initialize(cfg => cfg.CreateMap<Employee, EmployeeDTO>());
            var employeeDTO = Mapper.Map<EmployeeDTO>(employee);
            Console.WriteLine($"{employeeDTO.FirstName} {employeeDTO.LastName} {employeeDTO.Salary}");
        }
    }
}
