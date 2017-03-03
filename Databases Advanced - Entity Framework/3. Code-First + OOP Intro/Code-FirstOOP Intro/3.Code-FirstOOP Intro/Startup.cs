

namespace _3.Code_FirstOOP_Intro
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    class Startup
    {
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            var family = new Family(new List<Person>());
            for (int i = 0; i < n; i++)
            {
                var input = Console.ReadLine();
                var array = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                var person = new Person(array[0], int.Parse(array[1]));
                family.AddMember(person);
            }

            var oldestPerson = family.GetOldestMember();
            Console.WriteLine(oldestPerson);
        }

        //private static void GetOldestMember(Family family)
        //{
        //    var person = family.Members.Where(c => c.Age == family.Members.Select(a => a.Age).Max()).FirstOrDefault();
        //    Console.WriteLine(person);
        //}
    }

    public class Person
    {
        
        public Person(string name, int age)
        {
            this.Name = name;
            this.Age = age;
        }
        public string Name { get; set; }
        public int Age { get; set; }
        public override string ToString()
        {
            return this.Name + ' ' + this.Age;
        }
    }

    public class Family
    {
        public Family(List<Person> members)
        {
            this.Members = members;
        }
        public List<Person> Members { get; set; }

        public void AddMember(Person member )
        {
            Members.Add(member);
        }

        public Person GetOldestMember()
        {

            return Members.Where(c => c.Age == Members.Select(a => a.Age).Max()).FirstOrDefault();

        }

        

        
    }
}
