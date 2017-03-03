using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5.Code_FirstOOP_Intro
{
    class Startup
    {
        static void Main(string[] args)
        {
            var calculation = new Calculation();
            
            Console.WriteLine(calculation.Planck());
        }
    }
    class Calculation
    {
        public static double PlankConstant = 6.62606896e-34d;
        public static double PiConstant = 3.14159d;

        public double Planck()
        {
            var result = PlankConstant / (2 * PiConstant);
            return result;
        }
    }
}
