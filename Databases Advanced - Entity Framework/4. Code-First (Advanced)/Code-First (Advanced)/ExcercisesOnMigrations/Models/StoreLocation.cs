using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcercisesOnMigrations.Models
{
    public class StoreLocation
    {

        public StoreLocation()
        {
            this.SalesByStoreLocation = new HashSet<Sale>();
        }
        public int Id { get; set; }

        public string LocationName { get; set; }

        public ICollection<Sale> SalesByStoreLocation { get; set; }
    }
}
