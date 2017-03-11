using Entity_Relations_Advanced_.Models;
using System;
using System.Linq;

namespace Entity_Relations_Advanced_
{
    class Startup
    {
        static void Main()
        {
            var ctx = new StudentsContext();
            //ctx.Database.Initialize(true);


            Task3(ctx);
        }

        static void Task3(StudentsContext ctx)
        {
            Console.WriteLine("Hello, this is SoftUni 4.0! Welcome.");
            Console.WriteLine("----------------------------------");
            Console.WriteLine("Type 'commands' to see the available commands ");
            Console.WriteLine("Type 'Finished' when you are ready.");

            var input = Console.ReadLine();

            while (input != "Finished")
            {
                if (input == "commands")
                {
                    Console.WriteLine("1. List All Students - Lists all students with their homeworks");
                    Console.WriteLine("2. List All Courses - Lists all Courses with their corresponding resources");
                    Console.WriteLine("3. List All Courses > 5 resources - Lists all courses with more than 5 resources");
                    Console.WriteLine("4. List All Courses Date - Lists All courses ACTIVE by given date.");
                    Console.WriteLine("5. List All Students courses - Gets the number of courses he/she has enrolled in, the total price of these courses and the average price per course for the student");
                }

                if (input == "List All Students")
                {
                    var students = ctx.Students.ToList();

                    foreach (var student in students)
                    {
                        Console.WriteLine($"- {student.Name}");
                        Console.WriteLine("Homeworks: ");
                        foreach (var hw in student.Homeworks)
                        {
                            Console.WriteLine($"-- {hw.Content}.{hw.ContenType}");
                            Console.WriteLine("----------------------------------");
                        }
                    }
                }
                else if (input == "List All Courses")
                {
                    var courses = ctx.Courses.OrderBy(c => c.StartDate).ThenByDescending(c => c.EndDate).ToList();
                    foreach (var c in courses)
                    {
                        Console.WriteLine($"- {c.Name}");
                        if (c.Description == null)
                        {
                            Console.WriteLine("(no description)");
                        }
                        else
                        {
                            Console.WriteLine($"- {c.Description}");
                        }
                        Console.WriteLine("Resources:");
                        if (c.Resources.Count == 0)
                        {
                            Console.WriteLine("No resources for this course!");
                        }
                        else
                        {
                            foreach (var r in c.Resources)
                            {
                                Console.WriteLine($"-- {r.Name}, {r.ResourceType}, {r.URL}");
                            }
                        }
                    }
                }
                else if (input == "List All Courses > 5 resources")
                {
                    var courses = ctx.Courses.Where(c => c.Resources.Count > 5).OrderByDescending(c => c.Resources.Count).ThenByDescending(c => c.StartDate).ToList();
                    if (courses.Count == 0)
                    {
                        Console.WriteLine("No course with more than 5 resources.");
                    }
                    else
                    {
                        foreach (var c in courses)
                        {
                            Console.WriteLine($"Course name: {c.Name} Resources count: {c.Resources.Count}");
                        }
                    }
                }
                else if (input == "List All Courses Date")
                {
                    Console.Write("Date:");
                    var date = Console.ReadLine();
                    var datetime = Convert.ToDateTime(date);
                    var courses = ctx.Courses.Where(c => datetime >= c.StartDate && datetime < c.EndDate).ToList();
                    if (courses.Count == 0)
                    {
                        Console.WriteLine("No active courses on this date!");
                    }
                    else
                    {
                        foreach (var c in courses)
                        {
                            Console.WriteLine($"Course name: {c.Name}");
                            Console.WriteLine($"Start date: {c.StartDate}");
                            Console.WriteLine($"End date: {c.EndDate}");
                            Console.WriteLine($"Course duration(days): {(c.EndDate - c.StartDate).TotalDays} ");
                            Console.WriteLine($"Course students: {c.Students.Count}");
                            Console.WriteLine("----------------------------------");
                        }

                    }
                }
                else if (input == "List All Students courses")
                {
                    var students = ctx.Students.OrderByDescending(c=> c.Courses.Count).ThenBy(s=> s.Name).ToList();

                    foreach (var s in students)
                    {
                        Console.WriteLine("----------------------------------");
                        Console.WriteLine($"Student Name: {s.Name}");
                        Console.WriteLine($"Courses: {s.Courses.Count}");
                        foreach (var p in s.Courses)
                        {
                            Console.WriteLine($"- Course name: {p.Name}");
                            Console.WriteLine($"- Course price: {p.Price}");
                            Console.WriteLine($"- Course students: {p.Students.Count}"); 
                        }
                    }
                }
                input = Console.ReadLine();
            }
            Console.WriteLine("Thank you for using our services! :P");
        }
    }
}
