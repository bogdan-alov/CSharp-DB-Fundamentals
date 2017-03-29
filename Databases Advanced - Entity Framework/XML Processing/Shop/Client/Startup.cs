namespace Shop
{
    using AutoMapper;
    using Data;
    using Models;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;
    using System.Xml.Serialization;

    class Startup
    {
        static void Main()
        {

            //InitializeDatabase();
            using (var ctx = new ShopContext())
            {
                //ImportUsers(ctx);
                //ImportProducts(ctx);
                //ImportCategories(ctx);
                //ProductsInRange(ctx);
                //SoldProducts(ctx);
                //CategoriesByProductsCount(ctx);
                //UsersAndProducts(ctx);


            }

        }

        private static void UsersAndProducts(ShopContext ctx)
        {
            var users = ctx.Users.Where(c => c.ProductsSold.Count > 1).OrderByDescending(c => c.ProductsSold.Count).ThenBy(c => c.LastName).Select(c => new
            {
                FirstName = c.FirstName,
                LastName = c.LastName,
                Products = c.ProductsSold.Select(s => new
                {
                    Name = s.Name,
                    Price = s.Price
                })
            }).ToList();

            var xml = new XElement("users", new XAttribute("count", users.Count),

                users.Select(s => new XElement("user",
                s.FirstName == null ? null : new XAttribute("first-name", s.FirstName),
                new XAttribute("last-name", s.LastName),
                new XElement("sold-products", s.Products.Select(c =>
                new XElement("product", new XAttribute("name", c.Name),
                new XAttribute("price", c.Price)
                )))))).ToString();
            Console.WriteLine(xml);
        }

        private static void CategoriesByProductsCount(ShopContext ctx)
        {
            var categories = ctx.Categories.Select(c => new
            {
                Name = c.Name,
                Products = c.Products,
                AveragePrice = c.Products.Average(s => s.Price),
                TotalPrice = c.Products.Sum(s => s.Price)
            }).OrderBy(d => d.Products.Count).ToList();

            var xml = new XElement("categories", categories.Select(x => new XElement("category",
                new XAttribute("name", x.Name),
                new XElement("products-count", x.Products.Count),
                new XElement("aveerage-price", x.AveragePrice),
                new XElement("total-price", x.TotalPrice)
                ))).ToString();
            Console.WriteLine(xml);

        }

        private static void SoldProducts(ShopContext ctx)
        {
            var users = ctx.Users.OrderBy(c => c.FirstName).ThenBy(c => c.LastName).Select(c => new
            {
                FirstName = c.FirstName ?? string.Empty,
                LastName = c.LastName,
                SoldProducts = c.ProductsSold.Select(s => new
                {
                    Name = s.Name,
                    Price = s.Price
                })
            }).ToList();
            var xml = new XElement("users", users.Select(x => new XElement("product",
                                               x.FirstName == null ? null : new XAttribute("first-name", x.FirstName),
                                               new XAttribute("last-name", x.LastName),
                                               new XElement("sold-products", x.SoldProducts.Select(s => new XElement("product",
                                               new XElement("name", s.Name), new XElement("price", s.Price)
                                                )))))).ToString();
            Console.WriteLine(xml);

        }

        private static void ProductsInRange(ShopContext ctx)
        {
            // No products below < 2000, this is why I made it between 10000 and 20000 
            var products = ctx.Products.Where(p => p.Price >= 10000 && p.Price <= 20000 && p.BuyerId != null).OrderBy(d => d.Price).Select(c => new
            {
                Name = c.Name,
                Price = c.Price,
                Buyer = c.Buyer.FirstName + " " + c.Buyer.LastName
            }).ToList();

            var xml = new XElement("products", products.Select(x => new XElement("product",
                                               new XAttribute("name", x.Name),
                                               new XAttribute("price", x.Price),
                                               new XAttribute("buyer", x.Buyer)))).ToString();


            Console.WriteLine(xml);

        }

        private static void ImportCategories(ShopContext ctx)
        {
            var xDoc = XDocument.Load(@"C:\Users\Bogdan Alov\documents\visual studio 2015\Projects\XML Processing\Shop\XML\categories.xml");
            var categories = xDoc.Root.Elements();
            var products = ctx.Products.ToList();
            var rnd = new Random();
            foreach (var category in categories)
            {
                var newCategory = new Category()
                {
                    Name = category.Element("name").Value
                };

                for (int i = 0; i < 5; i++)
                {
                    int r = rnd.Next(1, products.Count);
                    newCategory.Products.Add(products[r]);
                }
                ctx.Categories.Add(newCategory);
                ctx.SaveChanges();

            }

        }

        private static void ImportProducts(ShopContext ctx)
        {
            var xDoc = XDocument.Load(@"C:\Users\Bogdan Alov\documents\visual studio 2015\Projects\XML Processing\Shop\XML\products.xml");
            var products = xDoc.Root.Elements();
            var rnd = new Random();
            foreach (var product in products)
            {
                var buyer = rnd.Next(1, ctx.Users.Count());
                var seller = rnd.Next(1, ctx.Users.Count());
                var newProduct = new Product()
                {
                    Name = product.Element("name").Value,
                    Price = decimal.Parse(product.Element("price").Value),
                    Buyer = ctx.Users.Where(c => c.Id == buyer).FirstOrDefault(),
                    Seller = ctx.Users.Where(c => c.Id == seller).FirstOrDefault()
                };
                ctx.Products.Add(newProduct);
                ctx.SaveChanges();

            }
        }

        private static void ImportUsers(ShopContext ctx)
        {
            XDocument xDocument = XDocument.Load(@"C:\Users\Bogdan Alov\documents\visual studio 2015\Projects\XML Processing\Shop\XML\users.xml");
            var users = xDocument.Root.Elements();
            foreach (var user in users)
            {
                int age;
                var number = int.TryParse(user.Attribute("age")?.Value, out age);

                var newUser = new User()
                {
                    FirstName = user.Attribute("first-name")?.Value,
                    LastName = user.Attribute("last-name").Value,
                    Age = number ? (int?)age : null
                };
                ctx.Users.Add(newUser);
                ctx.SaveChanges();
            }

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
