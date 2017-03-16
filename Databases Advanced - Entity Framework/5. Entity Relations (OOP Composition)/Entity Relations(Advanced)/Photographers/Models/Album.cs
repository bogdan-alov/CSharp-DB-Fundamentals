using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photographers.Models
{
    public class Album
    {
        public Album()
        {
            this.Pictures = new HashSet<Picture>();
            this.Tags = new HashSet<Tag>();
            this.Photographers = new HashSet<Photographer>();
        }
        public enum AlbumType
        {
            Public,
            Private
        }
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string BackgroundColor { get; set; }
        [Required]
        public AlbumType AlbumsType { get; set; }

        [Required]
        public Photographer Owner { get; set; }

        public virtual ICollection<Picture> Pictures { get; set; }


        public virtual ICollection<Photographer> Photographers { get; set; }

       public virtual ICollection<Tag> Tags { get; set; }
    }
}
