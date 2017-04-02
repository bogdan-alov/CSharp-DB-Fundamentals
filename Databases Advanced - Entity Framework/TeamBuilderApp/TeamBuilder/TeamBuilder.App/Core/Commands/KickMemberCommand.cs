using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamBuilder.App.Utilities;
using TeamBuilder.Data;

namespace TeamBuilder.App.Core.Commands
{
    public class KickMemberCommand
    {
        public string Execute(string[] inputArgs)
        {
            // TODO: Check input lenght.
            Check.CheckLenght(2, inputArgs);
            // TODO: Authorize for logged in users.
            AuthenticationManager.Authorize();

            var teamName = inputArgs[0];

            if (!CommandHelper.IsTeamExists(teamName))
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.TeamNotFound, teamName));
            }

            string username = inputArgs[1];

            if (!CommandHelper.IsUserExisting(username))
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.UserNotFound, username));
            }

            if (!CommandHelper.IsMemberOfTeam(teamName, username))
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.NotPartOfTeam, username, teamName));
            }

            if (!CommandHelper.IsUserCreatorOfTeam(teamName, AuthenticationManager.GetCurrentUser().Username))
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.NotAllowed));
            }

            if (AuthenticationManager.GetCurrentUser().Username == username)
            {
                throw new InvalidOperationException(string.Format(Constants.ErrorMessages.CommandNotAllowed, "DisbandTeam"));
            }

            this.KickMemberFromTeam(teamName, username);

            return $"User {username} was kicked from {teamName}!";
        }

        private void KickMemberFromTeam(string teamName, string username)
        {
            using (var ctx = new TeamBuilderContext())
            {
                var user = ctx.Users.FirstOrDefault(c => c.Username == username);

                var team = ctx.Teams.FirstOrDefault(c => c.Name == teamName);

                team.Members.Remove(user);
                ctx.SaveChanges();
            }
        }
    }
}
