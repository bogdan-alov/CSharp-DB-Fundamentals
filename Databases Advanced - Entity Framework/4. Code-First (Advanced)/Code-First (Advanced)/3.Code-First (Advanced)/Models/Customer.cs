

namespace _3.Code_First__Advanced_.Models
{
    using System.Collections.Generic;
    public class Customer
    {
        public Customer()
        {
            this.SalesByCustomer = new HashSet<Sale>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string CreditCardNumber { get; set; }

        public HashSet<Sale> SalesByCustomer { get; set; }

        
    }
}
