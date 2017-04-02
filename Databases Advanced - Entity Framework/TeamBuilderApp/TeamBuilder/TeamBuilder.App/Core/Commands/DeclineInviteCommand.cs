using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamBuilder.App.Utilities;
using TeamBuilder.Data;

namespace TeamBuilder.App.Core.Commands
{
    public class DeclineInviteCommand
    {
        public string Execute(string[] inputArgs)
        {
            // TODO: Check input lenght.
            Check.CheckLenght(1, inputArgs);
            // TODO: Authorize for logged in users.
            AuthenticationManager.Authorize();

            string teamName = inputArgs[0];

            if (!CommandHelper.IsTeamExists(teamName))
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.TeamNotFound, teamName));
            }

            if (!CommandHelper.IsInviteExisting(teamName, AuthenticationManager.GetCurrentUser()))
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.InviteNotFound, teamName));
            }

            this.DeclineInvite(teamName);

            return $"Declined invitation for team {teamName}!";
        }

        private void DeclineInvite(string teamName)
        {
            using (var ctx = new TeamBuilderContext())
            {
                var currentUser = AuthenticationManager.GetCurrentUser();
                var team = ctx.Teams.FirstOrDefault(c => c.Name == teamName);
                ctx.Users.Attach(currentUser);
                var invitation = ctx.Invitations.FirstOrDefault(c => c.TeamId == team.Id && c.InvitedUserId == currentUser.Id && c.IsActive);
                invitation.IsActive = false;

                ctx.SaveChanges();
            }
        }
    }
}
