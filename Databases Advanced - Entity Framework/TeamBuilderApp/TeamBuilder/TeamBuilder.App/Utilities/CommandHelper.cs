using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamBuilder.Data;
using TeamBuilder.Models;

namespace TeamBuilder.App.Utilities
{
    public static class CommandHelper
    {
        public static bool IsTeamExists(string teamName)
        {
            using (var ctx = new TeamBuilderContext())
            {
                return ctx.Teams.Any(t => t.Name == teamName);
            }
        }

        public static bool IsUserExisting(string username)
        {
            using (var ctx = new TeamBuilderContext())
            {
                return ctx.Users.Any(t => t.Username == username);
            }
        }

        public static bool IsInviteExisting(string teamName, User user)
        {
            using (var ctx = new TeamBuilderContext())
            {
                return ctx.Invitations.Any(i => i.Team.Name == teamName && i.InvitedUserId == user.Id && i.IsActive == true);
            }
        }
        public static bool IsMemberOfTeam(string teamName, string usename)
        {
            using (var ctx = new TeamBuilderContext())
            {
                return ctx.Teams.Any(t => t.Name == teamName && t.Members.Any(c => c.Username == usename));
            }
        }
        public static bool IsEventExisting(string eventName)
        {
            using (var ctx = new TeamBuilderContext())
            {
                return ctx.Events.Any(t => t.Name == eventName);
            }
        }

        public static bool IsUserCreatorOfEvent(string eventName, string username)
        {
            using (var ctx = new TeamBuilderContext())
            {
                return ctx.Events.Any(t => t.Name == eventName && t.Creator.Username == username);
            }
        }

        public static bool IsUserCreatorOfTeam(string teamName, string username)
        {
            using (var ctx = new TeamBuilderContext())
            {
                return ctx.Teams.Any(t => t.Name == teamName && t.Creator.Username == username);
            }
        }
    }
}
