using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamBuilder.App.Utilities
{
    public static class Constants
    {
        public const int MinUsernameLenght = 3;
        public const int MaxUsernameLenght = 25;

        public const int MaxFirstNameLenght = 25;

        public const int MaxLastNameLenght = 25;

        public const int MinPasswordLenght = 6;
        public const int MaxPasswordLenght = 30;

        public const string DateTimeFormat = "dd/MM/yyyy HH:mm";

        public static class ErrorMessages
        {
            // Common error messages.

            public const string InvalidArgumentsCount = "Invalid arguments count!";

            public const string LogoutFirst = "You should logout first!";
            public const string LoginFirst = "You should login first!";

            public const string TeamOrUserNotExist = "Team or user does not exist!";
            public const string InviteIsAlreadySent = "Invite is already sent!";

            public const string NotAllowed = "Not allowed!";

            public const string TeamNotFound = "Team {0} not found!";
            public const string UserNotFound = "User {0} not found!";
            public const string EventNotFound = "Event {0} not found!";
            public const string InviteNotFound = "Invite from {0} is not found!";

            public const string NotPartOfTeam = "User {0} is not a member in {1}";

            public const string CommandNotAllowed = "Command not allowed. Use {0} instead.";

            public const string CannotAddSameTeamTwice = "Cannot add same team twice!";

            // User error message.

            public const string UsernameNotValid = "Username {0} not valid!";

            public const string PasswordNotValid = "Password {0} not valid!";

            public const string PasswordDoesNotMatch = "Passwords do not match!";

            public const string AgeNotValid = "Age not valid!";

            public const string GenderNotValid = "Gender should either be 'Male' or 'Female' !";

            public const string UsernameIsTaken = "Username {0} is already taken!";
            public const string UserOrPAsswordIsInvalid = "Invalid username or password!";

            public const string InvalidDateFormat = "Please insert dates in the format: [dd/MM/yyyy HH:mm]!";

            //Team error message.

            public const string InvalidAcronym = "Acronym {0} not valid!";
            public const string TeamExists = "Team {0} already exists, nigga!";

            public const string FileNotFound = "Path {0} is not valid!";
            public const string InvalidXMLFormat = "Invalid XML format!";

        }


    }
}
