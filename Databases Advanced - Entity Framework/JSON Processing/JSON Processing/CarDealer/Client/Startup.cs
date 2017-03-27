namespace CarDealer
{
    using Data;
    using Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    class Startup
    {
        static void Main()
        {
            //InitiliazeDatabase();
            using (var ctx = new CarDealerContext())
            {
                //ImportData(ctx);
                //OrderedCustomers(ctx);
                //CarsFromMakeToyota(ctx);
                //LocalSuppliers(ctx);
                //CarsWithTheirListOfParts(ctx);
                //TotalSalesByCustomer(ctx);
                SalesWithAppliedDiscount(ctx);
            }
        }

        private static void SalesWithAppliedDiscount(CarDealerContext ctx)
        {
            var sales = ctx.Sales.Select(c => new
            {
                Car = new { c.Car.Make, c.Car.Model, c.Car.TravelledDistance },
                CustomerName = c.Customer.Name,
                Discount = c.Discount / 100,
            PriceWithoutDiscount = c.Car.Parts.Sum(s=> s.Price),
                PriceWithDiscount = c.Car.Parts.Sum(s=> s.Price)-(c.Car.Parts.Sum(s=> s.Price) * (c.Discount / 100))
            }).ToList();

            var json = JsonConvert.SerializeObject(sales, Formatting.Indented);
            Console.WriteLine(json);
        }

        private static void TotalSalesByCustomer(CarDealerContext ctx)
        {
            var customers = ctx.Customers.Where(c=> c.Sales.Count >= 1).ToList().Select(c => new
            {
                FullName = c.Name,
                BoughtCars = c.Sales.Count(),
                SpentMoney = c.Sales.Sum(s=> (s.Car.Parts.Sum(v=> v.Price)))
            }).OrderByDescending(c=> c.SpentMoney).ThenByDescending(c=> c.BoughtCars);

            var json = JsonConvert.SerializeObject(customers, Formatting.Indented);
            Console.WriteLine(json);
        }

        private static void CarsWithTheirListOfParts(CarDealerContext ctx)
        {
            var cars = ctx.Cars.Select(c => new
            {
                Make = c.Make,
                Model = c.Model,
                TravelledDistance = c.TravelledDistance,
                Parts = c.Parts.Select(s => new
                {
                    s.Name,
                    s.Price
                })
            }).ToList();

            var json = JsonConvert.SerializeObject(cars, Formatting.Indented);
            Console.WriteLine(json);
        }

        private static void LocalSuppliers(CarDealerContext ctx)
        {
            var suppliers = ctx.Suppliers.Select(c => new
            {
                Id = c.Id,
                Name = c.Name,
                PartsCount = c.Parts.Count()
            }).ToList();

            var json = JsonConvert.SerializeObject(suppliers, Formatting.Indented);
            Console.WriteLine(json);
        }

        private static void CarsFromMakeToyota(CarDealerContext ctx)
        {
            var cars = ctx.Cars.OrderBy(c => c.Model).ThenByDescending(c => c.TravelledDistance).Where(c=> c.Make == "Toyota").Select(c=> new
            {
                Id = c.Id,
                Make = c.Make,
                Model = c.Model,
                TravelledDistance = c.TravelledDistance
            }).ToList();

            var json = JsonConvert.SerializeObject(cars, Formatting.Indented);
            Console.WriteLine(json);
        }

        private static void OrderedCustomers(CarDealerContext ctx)
        {
            var customers = ctx.Customers.OrderBy(c => c.BirthDate).ThenBy(c=> c.IsYoungDriver).Select(c => new
            {
                Id = c.Id,
                Name = c.Name,
                Birthday = c.BirthDate,
                IsYoungDriver = c.IsYoungDriver,
                Sales = c.Sales.Select(s=> new
                {
                    CarName = s.Car.Make,
                    Customer = s.Customer.Name
                })
                
                
            }).ToList();

            var json = JsonConvert.SerializeObject(customers, Formatting.Indented);
            Console.WriteLine(json);
        }

        private static void ImportData(CarDealerContext ctx)
        {
            var suppliersJSON = File.ReadAllText(@"C:\Users\Bogdan Alov\documents\visual studio 2015\Projects\JSON Processing\CarDealer\JSON\suppliers.json");

            List<Supplier> suppliers = JsonConvert.DeserializeObject<List<Supplier>>(suppliersJSON);

            var partsJSON = File.ReadAllText(@"C:\Users\Bogdan Alov\documents\visual studio 2015\Projects\JSON Processing\CarDealer\JSON\parts.json");

            List<Part> parts = JsonConvert.DeserializeObject<List<Part>>(partsJSON);
            var rnd = new Random();
            foreach (var part in parts)
            {
                var r = rnd.Next(1, suppliers.Count);
                part.Supplier = suppliers[r];
            }

            var carsJSON = File.ReadAllText(@"C:\Users\Bogdan Alov\documents\visual studio 2015\Projects\JSON Processing\CarDealer\JSON\cars.json");

            List<Car> cars = JsonConvert.DeserializeObject<List<Car>>(carsJSON);

            foreach (var car in cars)
            {
                for (int i = 0; i < 15; i++)
                {
                    var r = rnd.Next(1, parts.Count);
                    car.Parts.Add(parts[r]);
                }
            }

            var customersJSON = File.ReadAllText(@"C:\Users\Bogdan Alov\documents\visual studio 2015\Projects\JSON Processing\CarDealer\JSON\customers.json");

            List<Customer> customers = JsonConvert.DeserializeObject<List<Customer>>(customersJSON);
            var discounts = new List<decimal> { 0, 5, 10, 15, 20, 30, 40, 50 };
            var sales = new List<Sale>();

            for (int i = 0; i < 50; i++)
            {
                sales.Add(new Sale());
            }

            foreach (var sale in sales)
            {
                var randomCar = rnd.Next(1, cars.Count);
                var randomCustomer = rnd.Next(1, customers.Count);
                var randomDiscount = rnd.Next(1, discounts.Count);

                sale.Car = cars[randomCar];
                sale.Customer = customers[randomCustomer];
                sale.Discount = discounts[randomDiscount];
                
            }
            ctx.Suppliers.AddRange(suppliers);
            ctx.Parts.AddRange(parts);
            ctx.Customers.AddRange(customers);
            ctx.Cars.AddRange(cars);
            ctx.Sales.AddRange(sales);
            ctx.SaveChanges();
        }

        private static void InitiliazeDatabase()
        {
            var ctx = new CarDealerContext();
            ctx.Database.Initialize(true);
        }
    }
}
