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
    public class CreateTeamCommand
    {
        public string Execute(string[] inputArgs)
        {
            if (inputArgs.Length != 2 && inputArgs.Length != 3)
            {
                throw new ArgumentOutOfRangeException(nameof(inputArgs));

            }

            AuthenticationManager.Authorize();

            string teamName = inputArgs[0];

            if (teamName.Length > 25)
            {
                throw new ArgumentException(Constants.ErrorMessages.NotAllowed);
            }

            if (CommandHelper.IsTeamExists(teamName))
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.TeamExists, teamName));
            }

            string acronym = inputArgs[1];

            if (acronym.Length != 3)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.InvalidAcronym, acronym));
            }

            string description = inputArgs.Length == 3 ? inputArgs[2] : null;

            this.AddTeam(teamName, acronym, description);

            return $"Team {teamName} was succesfully created!";
        }

        private void AddTeam(string teamName, string acronym, string description)
        {
            using (var ctx = new TeamBuilderContext())
            {
                var t = new Team()
                {
                    Name = teamName,
                    Actronym = acronym,
                    Description = description,
                    CreatorId = AuthenticationManager.GetCurrentUser().Id 
                };
                ctx.Teams.Add(t);
                ctx.SaveChanges();
            }
        }
    }
}
