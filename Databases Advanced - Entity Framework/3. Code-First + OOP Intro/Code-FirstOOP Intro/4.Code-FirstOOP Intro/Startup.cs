namespace _4.Code_FirstOOP_Intro
{
    public class Student
    {
        public string Name { get; set; }

        public int Count { get; protected set; } = 0;
        public Student(string name)
        {
                this.Name = name;
                this.Count ++;
        }
    }
}
