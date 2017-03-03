

namespace _6.Code_FirstOOP_Intro
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
            var input = Console.ReadLine();
            
            while (input != "End")
            {
                var array = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                var command = array[0];
                var firstNumber = decimal.Parse(array[1]);
                var secondNumber = decimal.Parse(array[2]);
                var mathUtilities = new MathUtil();
                switch (command)
                {
                    case "Sum":
                        var sum = mathUtilities.Sum(firstNumber, secondNumber);
                        Console.WriteLine($"{sum:f2}");
                        break;

                    case "Subtract":
                        var subtract = mathUtilities.Subtract(firstNumber, secondNumber);
                        Console.WriteLine($"{subtract:f2}");
                        break;

                    case "Multiply":
                        var multiply = mathUtilities.Multiply(firstNumber, secondNumber);
                        Console.WriteLine($"{multiply:f2}");
                        break;

                    case "Divide":
                        var divide = mathUtilities.Divide(firstNumber, secondNumber);
                        Console.WriteLine($"{divide:f2}");
                        break;

                    case "Percentage":
                        var percentage = mathUtilities.Percentage(firstNumber, secondNumber);
                        Console.WriteLine($"{percentage:f2}");
                        break;

                    default:
                        Console.WriteLine("No input.");
                        break;


                }

                input = Console.ReadLine();
            }
        }
    }

    class MathUtil
    {
        public static decimal First { get; set; }
        public static decimal Second { get; set; }

        public decimal Sum(decimal number1, decimal number2)
        {
            var sum = number1 + number2;
            return sum;
        }

        public decimal Subtract(decimal number1, decimal number2)
        {
            var subtract = number1 - number2;
            return subtract;
        }


        public decimal Multiply(decimal number1, decimal number2)
        {
            var multiply = number1 * number2;
            return multiply;
        }

        public decimal Divide(decimal divident, decimal divisor)
        {
            var divide = divident / divisor;
            return divide;
        }

        public decimal Percentage(decimal totalNumber, decimal percent)
        {
            var percentage = totalNumber * (percent / 100);
            return percentage;
        }
    }
}
