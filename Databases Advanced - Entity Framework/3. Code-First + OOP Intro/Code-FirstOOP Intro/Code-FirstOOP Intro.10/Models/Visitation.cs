using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_FirstOOP_Intro._10
{
    public class Visitation
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }


        public int DoctorID { get; set; }

        public HashSet<Doctor> Doctors { get; set; }

        public HashSet<Patient> Patients { get; set; }
    }
}
