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
    public class ShowEventCommand
    {
        public string Execute(string[] inputArgs)
        {
            // TODO: Check input lenght.
            Check.CheckLenght(1, inputArgs);
            // TODO: Authorize for logged in users.
            AuthenticationManager.Authorize();

            var eventName = inputArgs[0];

            if (!CommandHelper.IsEventExisting(eventName))
            {
                throw new ArgumentException(Constants.ErrorMessages.EventNotFound, eventName);
            }

            var thisEvent = this.Get(eventName);
            Console.WriteLine($"{eventName}{thisEvent.StartDate}{thisEvent.EndDate}");
            if (thisEvent.ParticipatingTeams.Count > 0)
            {
                Console.WriteLine("Teams:");
                foreach (var team in thisEvent.ParticipatingTeams)
                {
                    Console.WriteLine($"- {team.Name}");
                }
            }
            else
            {
                Console.WriteLine("No teams to this event!");
            }
            return $@"----------------------------------------";
        }



        private Event Get(string eventName)
        {
            var ctx = new TeamBuilderContext();

            var ev = ctx.Events.FirstOrDefault(c => c.Name == eventName);
            return ev;

        }
    }
}
