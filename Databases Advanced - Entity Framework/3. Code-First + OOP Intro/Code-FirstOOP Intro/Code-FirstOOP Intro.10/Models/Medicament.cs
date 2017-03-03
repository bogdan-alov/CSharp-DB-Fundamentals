

namespace Code_FirstOOP_Intro._10.Models
{
    using System.Collections.Generic;

    public class Medicament
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public HashSet<Patient> Patients { get; set; }
    }
}
