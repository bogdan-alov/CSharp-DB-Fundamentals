namespace Advanced_Mapping.DTOs
{
    using Advanced_Mapping.Models;
    using System.Collections.Generic;
    class ManagerDTO
    {
        public ManagerDTO()
        {
            this.Subordinates = new List<Employee>();
        }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual List<Employee> Subordinates { get; set; }

    }
}
