using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcercisesOnMigrations.Models
{
    public class Customer
    {
        public Customer()
        {
            this.SalesByCustomer = new HashSet<Sale>();
        }
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string CreditCardNumber { get; set; }

        public HashSet<Sale> SalesByCustomer { get; set; }


    }
}
