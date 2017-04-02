using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamBuilder.App.Core;
using TeamBuilder.Data;
using TeamBuilder.Models;

namespace TeamBuilder.App
{
    class Startup
    {
        static void Main(string[] args)
        {
            var engine = new Engine(new CommandDispatcher());
            engine.Run();
        }
    }
    
}
