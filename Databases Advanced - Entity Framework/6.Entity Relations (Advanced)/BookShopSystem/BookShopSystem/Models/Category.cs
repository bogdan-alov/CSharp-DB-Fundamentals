using System.Collections.Generic;

namespace BookShopSystem.Models
{
    public class Category
    {
        public Category()
        {
            this.Books = new HashSet<Book>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public Author Author { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
