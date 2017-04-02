using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamBuilder.App.Utilities;
using TeamBuilder.Data;
using TeamBuilder.Models;

namespace TeamBuilder.App.Core.Commands
{
    public class ExportTeamCommand
    {
        public string Execute(string[] inputArgs)
        {
            Check.CheckLenght(1, inputArgs);
            string teamName = inputArgs[0];
            if (!CommandHelper.IsTeamExists(teamName))
            {
                throw new ArgumentException(Constants.ErrorMessages.TeamNotFound, teamName);
            }

            Team team = this.GetTeamByName(teamName);

            this.ExportTeam(team);

            return $"You successfully exported {teamName} team!";
        }

        private void ExportTeam(Team team)
        {
            var json = JsonConvert.SerializeObject(new
            {
                Name = team.Name,
                Acronym = team.Actronym,
                Members = team.Members.Select(c => c.Username)
                
            }, Formatting.Indented);

            File.WriteAllText("../../team.json", json);
        }

        private Team GetTeamByName(string teamName)
        {
            var ctx = new TeamBuilderContext();

            return ctx.Teams.FirstOrDefault(c => c.Name == teamName);

        }
    }
}
