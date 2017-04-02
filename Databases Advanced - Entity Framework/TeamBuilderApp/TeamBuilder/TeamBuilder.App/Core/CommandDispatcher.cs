using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TeamBuilder.App.Core.Commands;

namespace TeamBuilder.App.Core
{
    public class CommandDispatcher
    {
        public string Dispatch(string input)
        {
            string result = string.Empty;
            string[] inputArgs = input.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            string commandName = inputArgs.Length > 0 ? inputArgs[0] : string.Empty;
            inputArgs = inputArgs.Skip(1).ToArray();

            // Get command's type.
            Type commandType = Type.GetType(
                                 "TeamBuilder.App.Core.Commands." + commandName + "Command");

            // If command's type is not found – it is not valid command.
            if (commandType == null)
            {
                throw new NotSupportedException($"Command {commandName} not supported!");
            }

            // Create instance of command with the type that we already extracted.
            object command = Activator.CreateInstance(commandType);

            // Get the method called “Execute” of the command.
            MethodInfo executeMethod = command.GetType().GetMethod("Execute");

            // Invoke the method we found passing the instance of the command and
            // array of all expected arguments that the method should take when it is invoked.
            result = executeMethod.Invoke(command, new object[] { inputArgs }) as string;

            return result;
        }
    }
}
