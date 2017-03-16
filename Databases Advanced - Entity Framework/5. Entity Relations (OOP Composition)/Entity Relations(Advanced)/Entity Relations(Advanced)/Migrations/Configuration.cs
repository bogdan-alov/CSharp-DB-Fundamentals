namespace Entity_Relations_Advanced_.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Globalization;
    using System.Linq;

    public class Configuration : DbMigrationsConfiguration<Entity_Relations_Advanced_.StudentsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "Entity_Relations_Advanced_.StudentsContext";
        }

        protected override void Seed(Entity_Relations_Advanced_.StudentsContext context)
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


            context.Students.AddOrUpdate(p => p.Name,
                new Student { Name = "Gosho", PhoneNumber = "+359 6213 09962", RegistrationDate = Convert.ToDateTime("16/02/2017"), BirthDay = Convert.ToDateTime("16/03/2016") },
                new Student { Name = "Pesho", PhoneNumber = "+359 3613 09962", RegistrationDate = Convert.ToDateTime("16/02/2017"), BirthDay = Convert.ToDateTime("16/03/2016") },
                new Student
                {
                    Name = "Sasho",
                    PhoneNumber = "+359 6213 05562",
                    RegistrationDate = Convert.ToDateTime("16/02/2017"),
                    BirthDay = Convert.ToDateTime("16/03/2016")
                }
                );

            context.Courses.AddOrUpdate(p => p.Name,
                new Course
                {
                    Name = "DB Fundamentals",
                    StartDate = Convert.ToDateTime("16/01/2017"),
                    EndDate = Convert.ToDateTime("20/04/2017"),
                    Price = 390m
                },
                new Course
                {
                    Name = "C# Advanced",
                    StartDate = Convert.ToDateTime("16/05/2017"),
                    EndDate = Convert.ToDateTime("20/08/2017"),
                    Price = 390m
                },
                new Course
                {
                    Name = "Javascript (JS) Core",
                    StartDate = Convert.ToDateTime("16/05/2017"),
                    EndDate = Convert.ToDateTime("20/08/2017"),
                    Price = 390m
                }
                );

            context.Resources.AddOrUpdate(p => p.Name,
                new Resource
                {
                    Name = "EnTITIES-Excercise",
                    ResourceType = ResourceType.Document,
                    CourseId = context.Courses.Where(p => p.Id == 1).FirstOrDefault(),
                    URL = "softuni.bg/DB-Fundamentals"
                },
                new Resource
                {
                    Name = "W",
                    ResourceType = ResourceType.Document,
                    CourseId = context.Courses.Where(p => p.Id == 3).FirstOrDefault(),
                    URL = "softuni.bg/Javascript-(JS)-Core"
                },
                new Resource
                {
                    Name = "Stacks&Queues",
                    ResourceType = ResourceType.Document,
                    CourseId = context.Courses.Where(p => p.Id == 2).FirstOrDefault(),
                    URL = "softuni.bg/C#-Advanced"
                }
                );



            context.Homeworks.AddOrUpdate(h => h.Content,
                new Homework
                {
                    Content = "HomeWorkForEntities",
                    ContenType = Homework.ContentType.zip,
                    CourseId = context.Courses.Where(p => p.Id == 1).FirstOrDefault(),
                    StudentId = context.Students.Where(p => p.Id == 1).FirstOrDefault(),
                    SumbissionDate = Convert.ToDateTime("06/03/2017")
                },
                new Homework
                {
                    Content = "HomeWorkForStacks&Queues",
                    ContenType = Homework.ContentType.zip,
                    CourseId = context.Courses.Where(p => p.Id == 2).FirstOrDefault(),
                    StudentId = context.Students.Where(p => p.Id == 2).FirstOrDefault(),
                    SumbissionDate = Convert.ToDateTime("06/03/2017")
                },
                new Homework
                {
                    Content = "WTFISTHISSHIT",
                    ContenType = Homework.ContentType.zip,
                    CourseId = context.Courses.Where(p => p.Id == 3).FirstOrDefault(),
                    StudentId = context.Students.Where(p => p.Id == 3).FirstOrDefault(),
                    SumbissionDate = Convert.ToDateTime("06/03/2017")
                }
                );

            context.Licenses.AddOrUpdate(p => p.Name,
                new License { Name = "License01", Resource = context.Resources.Where(p => p.Id == 1).FirstOrDefault() },
                new License { Name = "License02", Resource = context.Resources.Where(p => p.Id == 2).FirstOrDefault() },
                new License { Name = "License03", Resource = context.Resources.Where(p => p.Id == 3).FirstOrDefault() }
                );

            context.SaveChanges();
            base.Seed(context);
        }
    }
}
