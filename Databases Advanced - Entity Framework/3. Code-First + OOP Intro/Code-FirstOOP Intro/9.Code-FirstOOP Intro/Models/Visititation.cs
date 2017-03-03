
namespace _9.Code_FirstOOP_Intro
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Visititation
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }
        public HashSet<Patient> Patient { get; set; }
    }
}
