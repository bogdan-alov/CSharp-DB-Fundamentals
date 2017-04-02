using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamBuilder.App.Utilities;
using TeamBuilder.Data;
using TeamBuilder.Models;

namespace TeamBuilder.App.Core.Commands
{
    public class LoginCommand
    {
        public string Execute(string[] inputArgs)
        {
            Check.CheckLenght(2, inputArgs);
            string username = inputArgs[0];
            string password = inputArgs[1];
            if (AuthenticationManager.IsAuthenticated())
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LogoutFirst);
            }

            User user = this.GetUserByCredentials(username, password);
            if (user == null)
            {
                throw new ArithmeticException(Constants.ErrorMessages.UserOrPAsswordIsInvalid);
            
            }

            AuthenticationManager.Login(user);

            return $"User {user.Username} successfully logged in!";
        }

        private User GetUserByCredentials(string username, string password)
        {
            using (var ctx = new TeamBuilderContext())
            {
                return ctx.Users.Where(c => c.Username == username && c.Password == password).FirstOrDefault();
            }
        }
    }
}
