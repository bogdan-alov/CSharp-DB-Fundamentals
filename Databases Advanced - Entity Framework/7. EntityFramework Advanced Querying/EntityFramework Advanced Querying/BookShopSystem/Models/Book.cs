using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopSystem.Models
{
    public enum EditionType
    {
        Normal = 0 ,
        Promo = 1,
        Gold = 2
    }

    public enum AgeRestrictionType
    {
        Minor = 0,
        Teen = 1,
        Adult = 2
    }
    public class Book
    {
        public Book()
        {
            this.Categories = new HashSet<Category>();
            this.RelatedBooks = new HashSet<Book>();
        }
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        public EditionType Edition { get; set; }

        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Copies { get; set; }

        [Required]
        public Author Author { get; set; }

        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        [Required]
        public AgeRestrictionType AgeRestriction { get; set; }
        public DateTime ReleasedDate { get; set; }

        public virtual ICollection<Book> RelatedBooks { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
    }
}
