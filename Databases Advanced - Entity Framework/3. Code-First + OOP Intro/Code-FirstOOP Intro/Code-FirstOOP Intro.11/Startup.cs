
namespace Code_FirstOOP_Intro._11
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
            var input = Console.ReadLine();
            GetEmails(input);
            
        }

        private static void GetEmails(string input)
        {
             using (var context = new UsersContext())
            {
                var users = context.Users.ToList();

                foreach (var user in users)
                {
                    if (user.Email.Contains(input))
                    {
                        Console.WriteLine($"{user.Username} {user.Email}");
                    }
                }

                
            }
        }
    }
}
