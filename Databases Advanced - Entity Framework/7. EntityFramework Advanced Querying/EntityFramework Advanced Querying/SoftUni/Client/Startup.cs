using SoftUni.Data;
using System;
using System.Linq;

namespace SoftUni.Client
{
    class Startup
    {
        static void Main(string[] args)
        {
            using (var ctx = new SoftUniEntities())
            {
                //17. CallAStoredProcedure(ctx);

                //18. EmployeesMaximumSalaries(ctx);

                
            }
            
        }

        private static void EmployeesMaximumSalaries(SoftUniEntities ctx)
        {
            var employees = ctx.Employees.GroupBy(c => c.Department.Name).Select(c => new { DepartmentName = c.Key, MaxSalary = c.Max(b => b.Salary) }).Where(c=> c.MaxSalary < 30000 || c.MaxSalary > 70000).ToList();
            foreach (var emp in employees)
            {
                Console.WriteLine($"{emp.DepartmentName} - ${emp.MaxSalary:f2}");
            }
        }

        private static void CallAStoredProcedure(SoftUniEntities ctx)
        {

            ctx.GetProjectsByName("Ruth", "Ellerbrock");
            
        }
    }
}
