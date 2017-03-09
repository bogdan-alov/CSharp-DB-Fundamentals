
namespace BookShopSystem
{
    using BookShopSystem.Data;
    using System.Data.Entity;
    using System.Linq;
    using System;

    class Startup
    {
        static void Main(string[] args)
        {
            var ctx = new BookShopContext();
            //ctx.Database.Initialize(true);

            // AddSomeRelatedBooks(ctx);

            // QueryTheFirstThreeBooks(ctx);
        }

        private static void QueryTheFirstThreeBooks(BookShopContext ctx)
        {
            var books = ctx.Books.Take(3).ToList();
            foreach (var book in books)
            {
                Console.WriteLine($"--{book.Id}{book.Title}");
                foreach (var r in book.RelatedBooks)
                {
                    Console.WriteLine(r.Title);
                }
            }
        }

        private static void AddSomeRelatedBooks(BookShopContext ctx)
        {
            var books = ctx.Books.Take(3).ToList();

            books[0].RelatedBooks.Add(books[1]);
            books[1].RelatedBooks.Add(books[0]);
            books[2].RelatedBooks.Add(books[1]);
            books[0].RelatedBooks.Add(books[2]);

            ctx.SaveChanges();
        }
    }
}
