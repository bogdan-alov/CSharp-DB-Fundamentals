using System.Collections.Generic;

namespace _9.Code_FirstOOP_Intro.Models
{
    public class Medicament
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public HashSet<Patient> Medicaments { get; set; }
    }
}
