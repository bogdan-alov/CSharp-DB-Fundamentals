namespace _2.Code_First__Advanced_
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Distributor { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }

        public decimal Weight { get; set; }

        public decimal Price { get; set; }
    }
}
