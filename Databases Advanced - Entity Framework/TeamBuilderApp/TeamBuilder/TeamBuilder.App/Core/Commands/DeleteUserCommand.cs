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
    class DeleteUserCommand
    {
        public string Execute(string[] inputArgs)
        {
            Check.CheckLenght(0, inputArgs);
            AuthenticationManager.Authorize();

            User currentUser = AuthenticationManager.GetCurrentUser();
            using (var ctx = new TeamBuilderContext())
            {
                currentUser.IsDeleted = true;
                ctx.SaveChanges();

                AuthenticationManager.Logout();
            }
            return $"User {currentUser.Username} was deleted succesfully!";
        }
    }
}
