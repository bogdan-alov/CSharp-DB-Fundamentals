using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TeamBuilder.App.Utilities;
using TeamBuilder.Data;
using TeamBuilder.Models;

namespace TeamBuilder.App.Core.Commands
{
    public class ImportTeamsCommand
    {
        public string Execute(string[] inputArgs)
        {
            Check.CheckLenght(1, inputArgs);
            string filePath = inputArgs[0];

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(string.Format(Constants.ErrorMessages.FileNotFound, filePath));
            }

            List<Team> teams;

            try
            {
                teams = this.GetTeamsFromXml(filePath);
            }
            catch (Exception)
            {

                throw new FormatException(Constants.ErrorMessages.InvalidXMLFormat);
            }


            this.AddTeams(teams);

            return $"You have successfully imported {teams.Count} teams!";
        }

        private void AddTeams(List<Team> teams)
        {
            using (var ctx = new TeamBuilderContext())
            {
                ctx.Teams.AddRange(teams);
                ctx.SaveChanges();
            }
        }

        private List<Team> GetTeamsFromXml(string filePath)
        {
            XDocument xdoc = XDocument.Load(filePath);
            var teamsList = new List<Team>();
            var teams = xdoc.Root.Elements();
            foreach (var team in teams)
            {
                var newTeam = new Team()
                {
                    Name = team.Element("name").Value,
                    Actronym = team.Element("acronym").Value,
                    CreatorId = int.Parse(team.Element("creator-id").Value),
                    Description = team.Element("description").Value
                };

                teamsList.Add(newTeam);
            }


            return teamsList;
        }
    }
}
