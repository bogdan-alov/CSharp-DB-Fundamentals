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
    public class InviteToTeamCommand
    {
        public string Execute(string[] inputArgs)
        {
            // TODO: Check input lenght.
            Check.CheckLenght(2, inputArgs);

            // TODO: Authorize for logged in users.
            AuthenticationManager.Authorize();

            string teamName = inputArgs[0];
            string username = inputArgs[1];

            if (!CommandHelper.IsTeamExists(teamName) && !CommandHelper.IsUserExisting(username))
            {
                throw new ArgumentException(Constants.ErrorMessages.TeamOrUserNotExist);
            }

            if (!this.IsInvitePending(teamName, username))
            {
                throw new InvalidOperationException(Constants.ErrorMessages.InviteIsAlreadySent);
            }
            if (!this.IsCreatorOrPartOfTeam(teamName))
            {
                throw new InvalidOperationException(Constants.ErrorMessages.NotAllowed);
            }
            this.SendInvite(teamName, username);
            return $"Team {teamName} invited {username}";
        }

        private void SendInvite(string teamName, string username)
        {
            using (var ctx = new TeamBuilderContext())
            {
                var team = ctx.Teams.FirstOrDefault(t => t.Name == teamName);
                var user = ctx.Users.FirstOrDefault(u => u.Username == username);

                var invitation = new Invitation()
                {
                    InvitedUser = user,
                    Team = team
                };

                if (team.Creator == user)
                {
                    team.Members.Add(user);
                }

                ctx.Invitations.Add(invitation);
                ctx.SaveChanges();
            }
        }

        private bool IsCreatorOrPartOfTeam(string teamName)
        {
            var currentUser = AuthenticationManager.GetCurrentUser();
            using (var ctx = new TeamBuilderContext())
            {
                return ctx.Teams.Any(t => t.Name == teamName && t.CreatorId == currentUser.Id || t.Members.Any(s => s.Username == currentUser.Username));
            }
        }

        private bool IsInvitePending(string teamName, string username)
        {
            using (var ctx = new TeamBuilderContext())
            {
                return ctx.Invitations.Any(c => c.Team.Name == teamName && c.InvitedUser.Username == username);
            }
        }
    }
}
