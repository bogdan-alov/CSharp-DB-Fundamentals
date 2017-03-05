namespace Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class SalesContext : DbContext
    {
        // Your context has been configured to use a 'SalesContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Migrations.SalesContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'SalesContext' 
        // connection string in the application configuration file.
        public SalesContext()
           : base("name=SalesContext")
        {
            Database.SetInitializer(new SeedAndInitialize());
        }

        public class SeedAndInitialize : DropCreateDatabaseAlways<SalesContext>
        {
            protected override void Seed(SalesContext context)
            {
                // Products
                var product1 = new Product()
                {
                    Name = "Cheese",
                    Quantity = 150,
                    Price = 6.90m,

                };
                context.Products.Add(product1);

                var product2 = new Product()
                {
                    Name = "Milk",
                    Quantity = 50,
                    Price = 2.50m,

                };
                context.Products.Add(product2);

                var product3 = new Product()
                {
                    Name = "Bread",
                    Quantity = 60,
                    Price = 0.90m,

                };
                context.Products.Add(product3);

                var product4 = new Product()
                {
                    Name = "Chicken",
                    Quantity = 99,
                    Price = 9.99m,

                };
                context.Products.Add(product4);

                var product5 = new Product()
                {
                    Name = "Fur",
                    Quantity = 99,
                    Price = 9.99m,

                };
                context.Products.Add(product5);

                // Products 

                // Customers

                var customer1 = new Customer()
                {
                    Name = "Pesho",
                    Email = "pesho@abv.bg",
                    CreditCardNumber = "IBN231231675"
                };
                context.Customers.Add(customer1);

                var customer2 = new Customer()
                {
                    Name = "Goshko",
                    Email = "goshkoo@gmail.com",
                    CreditCardNumber = "IBN6967MVC75"
                };
                context.Customers.Add(customer2);

                var customer3 = new Customer()
                {
                    Name = "Sashko",
                    Email = "sasho@abv.bg",
                    CreditCardNumber = "IBN231231675"
                };
                context.Customers.Add(customer3);

                var customer4 = new Customer()
                {
                    Name = "Vlado",
                    Email = "vlado@hotmail.com",
                    CreditCardNumber = "IBN6997316757"
                };
                context.Customers.Add(customer4);

                var customer5 = new Customer()
                {
                    Name = "John Cena",
                    Email = "youcantseeme@hotmail.com",
                    CreditCardNumber = "IBN6997316757"
                };
                context.Customers.Add(customer5);

                // Customers

                // Stores

                var storeLocation1 = new StoreLocation()
                {
                    LocationName = "Barcelona"
                };
                context.StoreLocations.Add(storeLocation1);

                var storeLocation2 = new StoreLocation()
                {
                    LocationName = "Madrid"
                };
                context.StoreLocations.Add(storeLocation2);

                var storeLocation3 = new StoreLocation()
                {
                    LocationName = "London"
                };
                context.StoreLocations.Add(storeLocation3);

                var storeLocation4 = new StoreLocation()
                {
                    LocationName = "Sofia"
                };
                context.StoreLocations.Add(storeLocation4);

                var storeLocation5 = new StoreLocation()
                {
                    LocationName = "Plovdiv"
                };
                context.StoreLocations.Add(storeLocation5);

                // Locations

                // Sales

                var sale1 = new Sale()
                {
                    Customer = customer3,
                    Product = product1,
                    StoreLocation = storeLocation5,
                    Date = Convert.ToDateTime("01/08/2016"),


                };
                context.Sales.Add(sale1);

                var sale2 = new Sale()
                {
                    Customer = customer1,
                    Product = product2,
                    StoreLocation = storeLocation4,
                    Date = Convert.ToDateTime("05/08/2016")
                };
                context.Sales.Add(sale2);


                var sale3 = new Sale()
                {
                    Customer = customer5,
                    Product = product4,
                    StoreLocation = storeLocation2,
                    Date = Convert.ToDateTime("12/02/2017")
                };
                context.Sales.Add(sale3);

                var sale4 = new Sale()
                {
                    Customer = customer1,
                    Product = product2,
                    StoreLocation = storeLocation3,
                    Date = Convert.ToDateTime(DateTime.Now)
                };
                context.Sales.Add(sale4);

                var sale5 = new Sale()
                {
                    Customer = customer1,
                    Product = product5,
                    StoreLocation = storeLocation2,
                    Date = Convert.ToDateTime("1/03/2017")
                };
                context.Sales.Add(sale5);

                base.Seed(context);
            }
        }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<StoreLocation> StoreLocations { get; set; }

        public virtual DbSet<Sale> Sales { get; set; }


        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}