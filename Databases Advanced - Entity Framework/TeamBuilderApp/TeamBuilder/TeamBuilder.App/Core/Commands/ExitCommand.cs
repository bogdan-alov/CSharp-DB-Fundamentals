using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamBuilder.App.Utilities;

namespace TeamBuilder.App.Core.Commands
{
    class ExitCommand
    {
        public string Execute(string[] inputArgs)
        {
            Check.CheckLenght(0, inputArgs);
            Environment.Exit(0);
            return "Goodbye! Cya again!";
            
        }
    }
}
