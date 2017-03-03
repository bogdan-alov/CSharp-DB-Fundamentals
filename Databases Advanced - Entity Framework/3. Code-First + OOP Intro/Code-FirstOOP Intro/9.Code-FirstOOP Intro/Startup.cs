namespace _9.Code_FirstOOP_Intro
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    class Startup
    {
        static void Main(string[] args)
        {
            var context = new HospitalContext();
            context.Database.Initialize(true);
        }
    }
}
