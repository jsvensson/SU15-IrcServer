using System.Collections.Generic;
using IrcServer.Commands;

namespace IrcServer
{
    static class CommandRegistry
    {
        private static Dictionary<string, IServerCommand> Commands { get; } = new Dictionary<string, IServerCommand>();

        public static void RegisterCommand(string command, IServerCommand handler)
        {
            command = command.ToUpper();
            Commands.Add(command, handler);
        }

        public static IServerCommand GetCommand(string verb)
        {
            IServerCommand command;
            Commands.TryGetValue(verb, out command);
            return command;
        }
    }
}
