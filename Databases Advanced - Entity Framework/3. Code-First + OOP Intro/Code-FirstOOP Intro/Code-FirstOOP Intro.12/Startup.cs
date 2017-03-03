using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_FirstOOP_Intro._12
{
    class Startup
    {
        static void Main()
        {
            var input = Console.ReadLine();
            DateTime date = Convert.ToDateTime(input);
            var context = new UsersContext();
            var users = context.Users.ToList();
            int i = 0;
            foreach (var user in users)
            {
                if (user.LastTimeLoggedIn < date)
                {
                    user.isDeleted = true;
                    i++;
                }
            }

            if (i == 0)
            {
                Console.WriteLine("No users have been deleted.");
            }
            else
            {
                Console.WriteLine($"{i} users have been deleted.");
            }

            

        }

    }
}

