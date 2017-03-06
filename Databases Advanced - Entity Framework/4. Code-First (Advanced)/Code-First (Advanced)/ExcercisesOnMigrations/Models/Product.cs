using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcercisesOnMigrations.Models
{
    public class Product
    {
        public Product()
        {
            this.SalesByProduct = new HashSet<Sale>();
            this.Description = "No description";
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public ICollection<Sale> SalesByProduct { get; set; }
    }
}
