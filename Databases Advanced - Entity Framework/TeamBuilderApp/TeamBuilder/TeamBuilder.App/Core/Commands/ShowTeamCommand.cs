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
    public class ShowTeamCommand
    {
        public string Execute(string[] inputArgs)
        {
            // TODO: Check input lenght.
            Check.CheckLenght(1, inputArgs);
            // TODO: Authorize for logged in users.
            AuthenticationManager.Authorize();

            var teamName = inputArgs[0];

            if (!CommandHelper.IsTeamExists(teamName))
            {
                throw new ArgumentException(Constants.ErrorMessages.TeamNotFound, teamName);
            }

            var thisTeam = this.Get(teamName);

            Console.WriteLine(thisTeam.Name);
            Console.WriteLine(thisTeam.Actronym);
            if (thisTeam.Members.Count > 0)
            {
                Console.WriteLine("Members:");
                foreach (var member in thisTeam.Members)
                {
                    Console.WriteLine($"- {member.Username}");
                }
            }
            else
            {
                Console.WriteLine("No members to this team!");
            }
            return $@"----------------------------------------";
        }



        private Team Get(string teamName)
        {
            var ctx = new TeamBuilderContext();
            var team = ctx.Teams.FirstOrDefault(c => c.Name == teamName);

            return team;


        }
    }
}
