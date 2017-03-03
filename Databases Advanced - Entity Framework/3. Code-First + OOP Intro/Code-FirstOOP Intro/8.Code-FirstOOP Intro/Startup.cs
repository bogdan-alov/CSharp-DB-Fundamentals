

namespace _8.Code_FirstOOP_Intro
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    class Startup
    {
        static void Main()
        {
            var context = new UsersContext();
            context.Database.Initialize(true);
        }
    }
}
