



namespace Migrations.Models
{

    using System.Collections.Generic;
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
