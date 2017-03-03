using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_FirstOOP_Intro._10
{
    public class Patient
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string  Email { get; set; }

        public DateTime BirthDate { get; set; }

        public byte[] Picture { get; set; }

        public bool isMedicalInsured { get; set; }

        public HashSet<Diagnose> Diagnoses { get; set; }

        public HashSet<Visitation> Visitations { get; set; }


    }
}
