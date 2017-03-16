using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photographers.Models
{
    public class Picture
    {
        public Picture()
        {
            this.Albums = new HashSet<Album>();
        }
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Caption { get; set; }

        public byte[] Path { get; set; }

        public virtual ICollection<Album> Albums { get; set; }
    }
}
