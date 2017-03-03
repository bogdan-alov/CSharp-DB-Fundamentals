namespace _9.Code_FirstOOP_Intro
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public class Diagnose
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public HashSet<Patient> Patients { get; set; }
    }
}
