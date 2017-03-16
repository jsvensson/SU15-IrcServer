using System.Collections.Generic;
using IrcServer.Client.Commands.Slash;

namespace IrcServer.Client
{
    static class SlashCommandRegistry
    {
        private static Dictionary<string, ISlashCommand> Commands { get; } = new Dictionary<string, ISlashCommand>();

        public static void RegisterHandler(string command, ISlashCommand handler)
        {
            command = command.ToLower();
            Commands.Add(command, handler);
        }

        public static ISlashCommand GetHandler(string verb)
        {
            ISlashCommand command;
            Commands.TryGetValue(verb.ToLower(), out command);
            return command;
        }
    }
}
