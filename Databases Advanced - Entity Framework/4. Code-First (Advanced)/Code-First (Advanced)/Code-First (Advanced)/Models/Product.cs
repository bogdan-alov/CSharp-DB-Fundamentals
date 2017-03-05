using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_First__Advanced_.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Distributor { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
    }
}
