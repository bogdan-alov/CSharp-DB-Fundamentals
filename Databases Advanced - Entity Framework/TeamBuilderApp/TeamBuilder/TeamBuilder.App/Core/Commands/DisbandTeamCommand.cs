using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamBuilder.App.Utilities;
using TeamBuilder.Data;

namespace TeamBuilder.App.Core.Commands
{
    public class DisbandTeamCommand
    {
        public string Execute(string[] inputArgs)
        {
            // TODO: Check input lenght.
            Check.CheckLenght(1, inputArgs);
            // TODO: Authorize for logged in users.
            AuthenticationManager.Authorize();

            string teamName = inputArgs[0];

            if (!CommandHelper.IsUserExisting(AuthenticationManager.GetCurrentUser().Username))
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.UserNotFound, AuthenticationManager.GetCurrentUser().Username)); 
            }

            var currentUser = AuthenticationManager.GetCurrentUser();

            if (!CommandHelper.IsUserCreatorOfTeam(teamName, currentUser.Username))
            {
                throw new InvalidOperationException(Constants.ErrorMessages.NotAllowed);
            }

            this.DisbandTeam(teamName);
            return $"{teamName} has been disbanded!";
        }

        private void DisbandTeam(string teamName)
        {
            using (var ctx = new TeamBuilderContext())
            {
                var team = ctx.Teams.FirstOrDefault(c => c.Name == teamName);

                ctx.Teams.Remove(team);
                ctx.SaveChanges();
            }
        }
    }
}
