using Projection.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projection.DTOs
{
    public class EmployeeDTO
    {
        public string Name { get; set; }

        public decimal Salary { get; set; }

        public string  ManagerLastName { get; set; }
    }
}
