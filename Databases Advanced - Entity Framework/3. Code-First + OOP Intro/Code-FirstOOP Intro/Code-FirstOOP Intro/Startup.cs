
namespace Code_FirstOOP_Intro
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class Startup
    {
        static void Main()
        {
            var people = new List<Person>();
            var command = Console.ReadLine();
            while (command != "STOP")
            {
                Person newPerson = new Person();
                var data = command.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                var name = data[0];
                var age = Int32.Parse(data[1]);
                newPerson.Name = name;
                newPerson.Age = age;
                people.Add(newPerson);
                command = Console.ReadLine();
            }

            foreach (var p in people)
            {
                Console.WriteLine(p.Name + " " + p.Age);
            }
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
