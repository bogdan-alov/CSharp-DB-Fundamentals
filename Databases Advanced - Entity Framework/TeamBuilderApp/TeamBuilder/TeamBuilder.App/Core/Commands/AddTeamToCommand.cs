using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamBuilder.App.Utilities;
using TeamBuilder.Data;

namespace TeamBuilder.App.Core.Commands
{
    public class AddTeamToCommand
    {
        public string Execute(string[] inputArgs)
        {
            // TODO: Check input lenght.
            Check.CheckLenght(2, inputArgs);
            // TODO: Authorize for logged in users.
            AuthenticationManager.Authorize();

            var eventName = inputArgs[0];
            if (!CommandHelper.IsEventExisting(eventName))
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.EventNotFound, eventName));
            }

            var teamName = inputArgs[1];

            if (!CommandHelper.IsTeamExists(teamName))
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.TeamNotFound, teamName));
            }

            if (!CommandHelper.IsUserCreatorOfEvent(eventName, AuthenticationManager.GetCurrentUser().Username))
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.NotAllowed));
            }

            this.AddTeamToEvent(teamName, eventName);

            return $"Team {teamName} added for {eventName}!";
        }

        private void AddTeamToEvent(string teamName, string eventName)
        {
            using (var ctx = new TeamBuilderContext())
            {
                var team = ctx.Teams.FirstOrDefault(s => s.Name == teamName);

                var ev = ctx.Events.OrderByDescending(e => e.StartDate).FirstOrDefault(c => c.Name == eventName);

                if (ev.ParticipatingTeams.Any(t=> t.Name == teamName))
                {
                    throw new InvalidOperationException(Constants.ErrorMessages.CannotAddSameTeamTwice);
                }
                ev.ParticipatingTeams.Add(team);
                ctx.SaveChanges();
            }
        }
    }
}
