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
    public class ImportUsersCommand
    {
        public string Execute(string[] inputArgs)
        {
            Check.CheckLenght(1, inputArgs);
            string filePath = inputArgs[0];

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(string.Format(Constants.ErrorMessages.FileNotFound, filePath));
            }

            List<User> users;

            try
            {
                users = this.GetUsersFromXml(filePath);
            }
            catch (Exception)
            {

                throw new FormatException(Constants.ErrorMessages.InvalidXMLFormat);
            }


            this.AddUsers(users);

            return $"You have successfully imported {users.Count} users!";
        }

        private void AddUsers(List<User> users)
        {
            using (var ctx = new TeamBuilderContext())
            {
                ctx.Users.AddRange(users);
                ctx.SaveChanges();
            }
        }

        private List<User> GetUsersFromXml(string filePath)
        {
            XDocument xdoc = XDocument.Load(filePath);
            var usersList = new List<User>();
            var users = xdoc.Root.Elements();
            foreach (var user in users)
            {
                var newUser = new User()
                {
                    FirstName = user.Element("first-name").Value,
                    LastName = user.Element("last-name").Value,
                    Age = int.Parse(user.Element("age").Value),
                    Password = user.Element("password").Value,
                    Username = user.Element("username").Value
     
                };

                usersList.Add(newUser);
            }


            return usersList; 
        }
    }
}
