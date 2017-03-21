
namespace Advanced_Mapping.Models
{
    using System;
    using System.Collections.Generic;

    class Employee
    {
        public Employee()
        {
            this.Subordinates = new List<Employee>();
        }
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? BirthDate { get; set; }

        public decimal Salary { get; set; }

        public bool IsOnHoliday { get; set; }

        public string Address { get; set; }

        public virtual List<Employee> Subordinates { get; set; }

        public virtual Employee Manager { get; set; }



    }
}
