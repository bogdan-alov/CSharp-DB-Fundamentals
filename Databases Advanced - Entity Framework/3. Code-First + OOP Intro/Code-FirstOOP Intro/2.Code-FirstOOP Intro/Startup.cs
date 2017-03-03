

namespace _2.Code_FirstOOP_Intro
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
            var dataForPerson = Console.ReadLine();

            var data = dataForPerson.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            
            int age;
            var person = new Person();
            if (data.Length == 0)
            {
                Console.WriteLine(person);
            }
            if (data.Length == 1)
            {
                if (int.TryParse(data[0], out age))
                {
                    person.Age = age;
                }
                else
                {
                    person.Name = data[0];
                }
            }
            else
            {
                person.Name = data[0];
                person.Age = Int32.Parse(data[1]);
            }
            
            Console.WriteLine(person);



        }
    }

    public class Person
    {
        public Person()
        {
            this.Name = "No name";
            this.Age = 1;
        }

        public Person(int age)
        {
            this.Name = "No name";
            this.Age = age;
        }

        public Person(string name)
        {
            this.Name = name;
            this.Age = 1;
        }

        public Person(string name, int age)
        {
            this.Name = name;
            this.Age = age;
        }

        //public Person(string name, int age)
        //{
        //    this.Name = name;
        //    this.Age = age;
        //}

        //public Person()
        //    : this ("No name", 0) { }
        public string Name { get; set; }
        public int Age { get; set; }
        public override string ToString()
        {
            return this.Name + ' ' + this.Age;
        }
    }
}
