
namespace BookShopSystem
{
    using BookShopSystem.Data;
    using System.Data.Entity;
    using System.Linq;
    using System;
    using Models;
    using EntityFramework.Extensions;
    using System.Data.SqlClient;

    class Startup
    {
        static void Main(string[] args)
        {
            var ctx = new BookShopContext();
            //ctx.Database.Initialize(true);

            //1. BooksTitlesByAgeResctriction(ctx);

            //2. GoldenBooks(ctx);

            //3. BooksByPrice(ctx);

            //4. NotRealeasedBooks(ctx);

            //5. BookTitlesByCategory(ctx);

            //6. BooksReleasedBeforeDate(ctx);

            //7. AuthorsSearch(ctx);

            //8. BooksTitleSearch(ctx); 

            //9. BooksTitlesSearch(ctx);

            //10. CountBooks(ctx);

            //11. TotalBookCopies(ctx);

            //12. FindProfit(ctx);

            //13. MostRecentBooks(ctx);

            //14. IncreaseBookCopies(ctx);

            //15. RemoveBooks(ctx);

            //16. StoredProcedure(ctx);

            //ctx.Database.Initialize(true);
        }


        private static void StoredProcedure(BookShopContext ctx)
        {
            var name = Console.ReadLine().Split(' ').ToArray();
            var query = "EXEC usp_Authors {0}, {1}";
            var sqlQuery = ctx.Database.SqlQuery<int>(query, name[0], name[1]).FirstOrDefault();

            Console.WriteLine($"{name[0]} {name[1]} has written {sqlQuery} books!");


        }

        private static void RemoveBooks(BookShopContext ctx)
        {
            var numberOFBooksDeleted = ctx.Books.Where(c => c.Copies < 4200).ToList().Count;
            ctx.Books.Where(c=> c.Copies < 4200).Delete();
            ctx.SaveChanges();
            Console.WriteLine($"{numberOFBooksDeleted} were deleted from database!");
        }

        private static void IncreaseBookCopies(BookShopContext ctx)
        {
            var date = Convert.ToDateTime("06-06-2013");
            int copies= 0 ;
            var books = ctx.Books.Where(c => c.ReleasedDate > date).ToList();
            foreach (var book in books)
            {
                book.Copies += 44;
                copies += 44;
            }
            Console.WriteLine(copies);
        }

        private static void MostRecentBooks(BookShopContext ctx)
        {
            var categories = ctx.Categories.Select(c=> new { c.Name, c.Books }).OrderByDescending(c=> c.Books.Count).ToList();
            foreach (var category in categories)
            {

                Console.WriteLine($"-- {category.Name}: {category.Books.Count} books ");
                var categoryBooks = category.Books.OrderByDescending(d => d.ReleasedDate).ThenBy(c=> c.Title).Select(c => new { c.Title, c.ReleasedDate.Year }).Take(3).ToList();
                foreach (var book in categoryBooks)
                {
                    Console.WriteLine($"{book.Title} ({book.Year})");
                }
            }
        }

        private static void FindProfit(BookShopContext ctx)
        {
            var categories = ctx.Categories.ToList();
            foreach (var category in categories)
            {
                var categoryBooks = category.Books.GroupBy(c => new { Name = category.Name }).Select(c => new { CategoryName = c.Key.Name, Amount = c.Sum(s => s.Copies * s.Price) }).OrderByDescending(c=> c.Amount).ToList();
                foreach (var book in categoryBooks)
                {
                    Console.WriteLine($"{book.CategoryName} - ${book.Amount}");
                }
            }
        }

        private static void BooksTitleSearch(BookShopContext ctx)
        {
            var word = Console.ReadLine().ToLower();
            var books = ctx.Books.ToList();
            foreach (var book in books)
            {
                var title = book.Title.ToLower();
                if (title.Contains(word))
                {
                    Console.WriteLine(book.Title);
                }
            }
        }

        private static void TotalBookCopies(BookShopContext ctx)
        {
            var books = ctx.Books.GroupBy(c=> new { AuthorName = c.Author.FirstName + " " + c.Author.LastName }).Select(c=> new { AuthorName = c.Key.AuthorName, Copies = c.Sum(s=> s.Copies)  }).ToList();


            foreach (var book in books)
            {
                Console.WriteLine($"{book.AuthorName} {book.Copies}");
            }
        }

        private static void CountBooks(BookShopContext ctx)
        {
            var number = int.Parse(Console.ReadLine());

            var books = ctx.Books.Where(c=> c.Title.Length > number).OrderByDescending(c => c.Copies).Select(c => c.Title.Length).ToList();
            Console.WriteLine(books.Count());
        }

        private static void BooksTitlesSearch(BookShopContext ctx)
        {
            var word = Console.ReadLine().ToLower();
            var books = ctx.Books.OrderBy(b => b.Id).Select(c => new { c.Title, c.Author.FirstName, c.Author.LastName }).ToList();
            foreach (var book in books)
            {
                var authorsLastName = book.LastName.ToLower();
                if (authorsLastName.StartsWith(word))
                {
                    Console.WriteLine($"{book.Title} ({book.FirstName} {book.LastName})");
                }
            }
        }

        private static void AuthorsSearch(BookShopContext ctx)
        {
            var word = Console.ReadLine();
            var authors = ctx.Authors.ToList();
            foreach (var author in authors)
            {
                if (author.FirstName.EndsWith(word))
                {
                    Console.WriteLine($"{author.FirstName} {author.LastName}");
                }
            }
        }

        private static void BooksReleasedBeforeDate(BookShopContext ctx)
        {
            var date = Console.ReadLine();
            var dateTime = Convert.ToDateTime(date);
            var books = ctx.Books.Where(c => c.ReleasedDate < dateTime).Select(c => new { c.Title, c.Edition, c.Price }).ToList();
            foreach (var book in books)
            {
                Console.WriteLine($"{book.Title} - {book.Edition.ToString()} {book.Price}");
            }
        }

        private static void BookTitlesByCategory(BookShopContext ctx)
        {
            var input = Console.ReadLine().ToLower();
            var categories = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            var books = ctx.Books.OrderBy(b => b.Id).Select(c=> new { c.Title, c.Categories }).ToList();
            foreach (var book in books)
            {
                if (book.Categories.Any(c=> categories.Contains(c.Name.ToLower())))
                {
                    Console.WriteLine(book.Title);
                }
            }
        }

        private static void NotRealeasedBooks(BookShopContext ctx)
        {
            var year = Console.ReadLine();
            var books = ctx.Books.OrderBy(b => b.Id).Where(c => c.ReleasedDate.Year.ToString() != year).Select(c=> c.Title).ToList();
            foreach (var book in books)
            {
                Console.WriteLine(book);
            }

        }

        private static void BooksByPrice(BookShopContext ctx)
        {
            var books = ctx.Books.Where(c => c.Price < 5 || c.Price > 40 ).OrderBy(c => c.Id).Select(c => new { c.Title, c.Price }).ToList();
            foreach (var book in books)
            {
                Console.WriteLine($"{book.Title} - ${book.Price}");
            }
        }

        private static void GoldenBooks(BookShopContext ctx)
        {
            var books = ctx.Books.Where(c => c.Copies < 5000).OrderBy(c => c.Id).Select(i => new {i.Title, i.Edition }).ToList();

            foreach (var book in books)
            {
                var bookEdition = book.Edition.ToString();
                if (bookEdition == "Gold")
                {
                    Console.WriteLine(book.Title);
                }
            }
        }

        private static void BooksTitlesByAgeResctriction(BookShopContext ctx)
        {
            var type = Console.ReadLine().ToLower();
            var books = ctx.Books.ToList();
            foreach (var book in books)
            {
                var bookType = book.AgeRestriction.ToString().ToLower();
                if (bookType == type)
                {
                    Console.WriteLine(book.Title);
                }
                
            }
        }
    }
}
