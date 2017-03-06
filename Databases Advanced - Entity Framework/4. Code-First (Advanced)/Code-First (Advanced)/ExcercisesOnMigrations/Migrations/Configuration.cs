namespace ExcercisesOnMigrations.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ExcercisesOnMigrations.SalesContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "ExcercisesOnMigrations.SalesContext";
        }

        protected override void Seed(ExcercisesOnMigrations.SalesContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

             context.Customers.AddOrUpdate(x => x.Id,
                new Customer() { Id = 1,Email = "goshko@abv.bg" , CreditCardNumber = "123456789",FirstName = "Goshko", LastName = "Goshkov" },
                new Customer() { Id = 2, Email = "Peshko@abv.bg", CreditCardNumber = "123456789", FirstName = "Peshko", LastName = "Peshev" },
                new Customer() { Id = 3, Email = "john@abv.bg", CreditCardNumber = "123456789", FirstName = "John", LastName = "Cena" },
                new Customer() { Id = 4, Email = "morron@abv.bg", CreditCardNumber = "123456789", FirstName = "Morron", LastName = "Morronov" },
                new Customer() { Id = 5, Email = "dude@abv.bg", CreditCardNumber = "123456789", FirstName = "Dude", LastName = "Dudov" }
                );

            context.SaveChanges();
            base.Seed(context);
        }
    }
}
