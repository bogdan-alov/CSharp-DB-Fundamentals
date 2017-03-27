using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Models
{
    [Table("CategoryProducts")]
    public class CategoryProduct
    {
        [Key, Column(Order = 0)]
        public int CategoryId { get; set; }

        [Key, Column(Order = 1)]
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public virtual Category Category { get; set; }
    }
}
