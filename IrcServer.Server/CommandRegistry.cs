using System.Collections.Generic;
using IrcServer.Commands;

namespace IrcServer
{
    static class CommandRegistry
    {
        private static Dictionary<string, ICommand> Commands { get; } = new Dictionary<string, ICommand>();

        public static void RegisterCommand(string command, ICommand callback)
        {
            command = command.ToUpper();
            Commands.Add(command, callback);
        }

        public static ICommand GetCommand(string verb)
        {
            ICommand command;
            Commands.TryGetValue(verb, out command);
            return command;
        }
    }
}
