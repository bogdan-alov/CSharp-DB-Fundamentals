namespace BookShopSystem.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Data;
    using System.IO;
    using Models;
    using System.Globalization;

    internal sealed class Configuration : DbMigrationsConfiguration<BookShopSystem.Data.BookShopContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "BookShopSystem.Data.BookShopContext";
        }

        protected override void Seed(BookShopSystem.Data.BookShopContext context)
        {
            //SeedAuthors(context);

            //SeedBooks(context);

            //SeedCategories(context);
        }

        private void SeedAuthors(BookShopContext context)
        {
            string[] authors = File.ReadAllLines(@"c:\users\bogdan alov\documents\visual studio 2015\Projects\BookShopSystem\BookShopSystem\Import\authors.csv");

            for (int i = 1; i < authors.Length; i++)
            {
                var data = authors[i].Split(',');
                var firstName = data[0].Replace("\"", string.Empty);
                var lastName = data[1].Replace("\"", string.Empty);

                Author author = new Author()
                {
                    FirstName = firstName,
                    LastName = lastName
                };

                context.Authors.AddOrUpdate(a => new { a.FirstName, a.LastName }, author);
            }
        }

        private void SeedBooks(BookShopContext context)
        {
            int authorsCount = context.Authors.Local.Count;
            string[] books = File.ReadAllLines(@"c:\users\bogdan alov\documents\visual studio 2015\Projects\BookShopSystem\BookShopSystem\Import\books.csv");

            for (int i = 1; i < books.Length; i++)
            {
                string[] data = books[i].Split(',').Select(arg => arg.Replace("\"", string.Empty)).ToArray();
                int authorIndex = i % authorsCount;

                var author = context.Authors.Local[authorIndex];
                EditionType edition = (EditionType)int.Parse(data[0]);

                DateTime releaseDate = DateTime.ParseExact(data[1], "d/M/yyyy", CultureInfo.InvariantCulture);

                int copies = int.Parse(data[2]);

                decimal price = decimal.Parse(data[3]);

                AgeRestrictionType ageRestriction = (AgeRestrictionType)int.Parse(data[4]);

                string title = data[5];

                Book book = new Book
                {
                    Author = author,
                    AuthorId = author.Id,
                    Edition = edition,
                    ReleasedDate = releaseDate,
                    Copies = copies,
                    Price = price,
                    AgeRestriction = ageRestriction,
                    Title = title
                };

                context.Books.AddOrUpdate(b => new { b.Title, b.AuthorId }, book);
            }

        }

        private void SeedCategories(BookShopContext context)
        {
            int booksCount = context.Books.Local.Count;

            string[] categories = File.ReadAllLines(@"c:\users\bogdan alov\documents\visual studio 2015\Projects\BookShopSystem\BookShopSystem\Import\books.csv");

            for (int i = 1; i < categories.Length; i++)
            {
                var data = categories[i].Split(',').Select(arg=> arg.Replace("\"", string.Empty)).ToArray();

                string categoryName = data[0];
                var category = new Category() { Name = categoryName };

                int bookIndex = (i * 30) % booksCount;
                for (int j = 0; j < bookIndex; j++)
                {
                    Book book = context.Books.Local[j];
                    book.Categories.Add(category);
                }

                context.Categories.AddOrUpdate(c => c.Name, category);
            }
        }
    }
}
