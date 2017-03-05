using System.Collections.Generic;
using IrcServer.Commands;

namespace IrcServer
{
    static class CommandRegistry
    {
        public static Dictionary<string, ICommand> Commands { get; } = new Dictionary<string, ICommand>();

        public static void RegisterCommand(string command, ICommand callback)
        {
            command = command.ToUpper();
            Commands.Add(command, callback);
        }
    }
}
