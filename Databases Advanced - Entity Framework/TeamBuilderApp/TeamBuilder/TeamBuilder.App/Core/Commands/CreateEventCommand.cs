using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamBuilder.App.Utilities;
using TeamBuilder.Data;
using TeamBuilder.Models;

namespace TeamBuilder.App.Core.Commands
{
    public class CreateEventCommand
    {
        public string Execute(string[] inputArgs)
        {
            // TODO: Check input lenght.
            Check.CheckLenght(6, inputArgs);
            // TODO: Authorize for logged in users.
            AuthenticationManager.Authorize();
            // TODO: Get event name and description

            var eventName = inputArgs[0];
            var description = inputArgs[1];

            DateTime startDateTime;

            bool isStartDateTime = DateTime.TryParseExact(inputArgs[2] + " " + inputArgs[3], Constants.DateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out startDateTime);

            DateTime endDateTime;

            bool isEndDateTime = DateTime.TryParseExact(inputArgs[4] + " " + inputArgs[5], Constants.DateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out endDateTime);

            if (!isEndDateTime || !isStartDateTime)
            {
                throw new ArgumentException(Constants.ErrorMessages.InvalidDateFormat);
            }

            if (startDateTime > endDateTime)
            {
                throw new ArgumentException(Constants.ErrorMessages.InvalidDateFormat);
            }
            this.CreateEvent(eventName, description, startDateTime, endDateTime);
            return $"Event {eventName} was created successfully!";
        }

        private void CreateEvent(string eventName, string description, DateTime startDateTime, DateTime endDateTime)
        {
            using (var ctx = new TeamBuilderContext())
            {
                Event e = new Event()
                {
                    Name = eventName,
                    Description = description,
                    StartDate = startDateTime,
                    EndDate = endDateTime,
                    CreatorId = AuthenticationManager.GetCurrentUser().Id
                };

                ctx.Events.Add(e);
                ctx.SaveChanges();

            }
        }
    }
}
