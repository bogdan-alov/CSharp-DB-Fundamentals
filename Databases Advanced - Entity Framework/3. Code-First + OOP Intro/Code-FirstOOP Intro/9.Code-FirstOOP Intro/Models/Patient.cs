namespace _9.Code_FirstOOP_Intro
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public class Patient
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public DateTime Birthdate { get; set; }

        public byte[] Picture { get; set; }

        public bool isMedicalInsured { get; set; }

        public HashSet<Diagnose> Diagnoses { get; set; }

        public HashSet<Visititation> Visitations { get; set; }
    }
}
