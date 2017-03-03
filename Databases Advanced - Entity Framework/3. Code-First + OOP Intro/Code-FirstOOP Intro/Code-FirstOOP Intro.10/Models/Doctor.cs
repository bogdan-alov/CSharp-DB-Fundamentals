using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_FirstOOP_Intro._10
{
    public class Doctor
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Speciality { get; set; }

        public HashSet<Visitation> Visitations { get; set; }

    }
}
