using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamBuilder.App.Utilities;
using TeamBuilder.Models;

namespace TeamBuilder.App.Core
{
    class AuthenticationManager
    {
        private static User currentUser;
        public static void Authorize()
        {
            if (currentUser == null)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LoginFirst);
            }
        }

        public static void Login(User user)
        {
            currentUser = user; 
        }

        public static  void Logout()
        {
            currentUser = null;
        }

        public static bool IsAuthenticated()
        {
            return false;
        }

        public static User GetCurrentUser()
        {
            return currentUser;
        }
    }
}
