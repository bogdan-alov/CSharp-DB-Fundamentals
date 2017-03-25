namespace Shop
{
    using Data;
    using Models;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    class Startup
    {
        static void Main()
        {

            //InitializeDatabase();
            using (var ctx = new ShopContext())
            {
                //ImportData(ctx);
                //ProductsInRange(ctx);
                //SuccesfullySoldProducts(ctx);
                //CategoriesByProductsCount(ctx);
                UsersAndProducts(ctx);
            }

        }

        private static void UsersAndProducts(ShopContext ctx)
        {
            var users = ctx.Users.Where(c => c.ProductsSold.Count >= 1).OrderByDescending(c => c.ProductsSold.Count).ToList().Select(c => new
            {
                FirstName = c.FirstName,
                LastName = c.LastName,
                Age = c.Age,

                SoldProducts = c.ProductsSold.Select(s=> new
                {
                    Count = c.ProductsSold.Count,
                    Products = c.ProductsSold.Select(d => new
                    {
                        Name = s.Name,
                        Price = s.Price
                    })
                })
            });
            var json = JsonConvert.SerializeObject(new { UsersCount = users.Count(), users }, Formatting.Indented);
            Console.WriteLine(json);
        }

        private static void CategoriesByProductsCount(ShopContext ctx)
        {
            var categories = ctx.Categories.OrderBy(c => c.Name).ToList().Select(s => new
            {
                Category = s.Name,
                Products = s.Products.Count,
                AveragePrice = s.Products.Average(c => c.Price),
                TotalPrice = s.Products.Sum(c => c.Price)
            });

            var json = JsonConvert.SerializeObject(categories, Formatting.Indented);
            Console.WriteLine(json);
        }

        private static void SuccesfullySoldProducts(ShopContext ctx)
        {
            var users = ctx.Users.Where(c => c.ProductsSold.Count >= 1).ToList().OrderBy(c => c.FirstName).ThenBy(c => c.LastName).Select(c => new
            {
                FirstName = c.FirstName,
                LastName = c.LastName,
                SoldProducts = c.ProductsSold.Select(s=> new
                {
                    Name = s.Name,
                    Price = s.Price,
                    BuyerFirstName = s.Buyer.FirstName,
                    BuyerLastName = s.Buyer.LastName
                })
            });
            var json = JsonConvert.SerializeObject(users, Formatting.Indented);
            Console.WriteLine(json);
        }

        private static void ProductsInRange(ShopContext ctx)
        {
            var products = ctx.Products.Where(c => c.Price >= 500 && c.Price <= 1000).ToList().OrderBy(c=> c.Price).Select(c=> new
            {
                Name = c.Name,
                Price = c.Price,
                Seller = c.Seller.FirstName + " " + c.Seller.LastName
            });

            string json = JsonConvert.SerializeObject(products, Formatting.Indented);
            Console.WriteLine(json);
        }

        private static void ImportData(ShopContext ctx)
        {
            string usersJSON = File.ReadAllText(@"c:\users\bogdan alov\documents\visual studio 2015\Projects\JSON Processing\Shop\JSON\users.json");
            List<User> users = JsonConvert.DeserializeObject<List<User>>(usersJSON);
            ctx.Users.AddRange(users);
            string productsJSON = File.ReadAllText(@"c:\users\bogdan alov\documents\visual studio 2015\Projects\JSON Processing\Shop\JSON\products.json");
            List<Product> products = JsonConvert.DeserializeObject<List<Product>>(productsJSON);

            Random rnd = new Random();
            int counter = 1;

            foreach (var product in products)
            {
                int r = rnd.Next(1, users.Count);
                product.Seller = users[counter];
                if (counter % 3 == 0)
                {
                    product.Buyer = users[r];
                }
                if (counter == 55)
                {
                    counter = 1;
                }
                else
                {
                    counter++;
                }

            }


            string categoriesJSON = File.ReadAllText(@"C:\Users\Bogdan Alov\documents\visual studio 2015\Projects\JSON Processing\Shop\JSON\categories.json");
            List<Category> categories = JsonConvert.DeserializeObject<List<Category>>(categoriesJSON);

            foreach (var category in categories)
            {


                for (int i = 0; i < 5; i++)
                {
                    int r = rnd.Next(1, products.Count);
                    category.Products.Add(products[r]);
                }
            }

            ctx.Users.AddRange(users);
            ctx.Products.AddRange(products);
            ctx.Categories.AddRange(categories);
            ctx.SaveChanges();

        }

        private static void InitializeDatabase()
        {
            try
            {
                var ctx = new ShopContext();
                ctx.Database.Initialize(true);
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
        }
    }
}
