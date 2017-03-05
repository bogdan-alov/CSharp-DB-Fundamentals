

namespace _3.Code_First__Advanced_.Models
{
    using System.Collections.Generic;
    public class Product
    {
        public Product()
        {
            this.SalesByProduct = new HashSet<Sale>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public ICollection<Sale> SalesByProduct { get; set; }
    }
}
