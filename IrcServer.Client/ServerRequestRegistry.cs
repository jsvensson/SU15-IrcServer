using System.Collections.Generic;
using IrcServer.Client.Commands.ServerRequest;

namespace IrcServer.Client
{
    static class ServerRequestRegistry
    {
        private static Dictionary<string, IServerRequest> Commands { get; } = new Dictionary<string, IServerRequest>();

        public static void RegisterHandler(string command, IServerRequest handler)
        {
            command = command.ToLower();
            Commands.Add(command, handler);
        }

        public static IServerRequest GetHandler(string verb)
        {
            IServerRequest command;
            Commands.TryGetValue(verb.ToLower(), out command);
            return command;
        }
    }
}
