using System.Collections.Generic;
using IrcServer.ProtocolRequests;

namespace IrcServer
{
    static class CommandRegistry
    {
        private static Dictionary<string, IServerRequest> Commands { get; } = new Dictionary<string, IServerRequest>();

        public static void RegisterCommand(string command, IServerRequest handler)
        {
            command = command.ToUpper();
            Commands.Add(command, handler);
        }

        public static IServerRequest GetCommand(string verb)
        {
            IServerRequest request;
            Commands.TryGetValue(verb, out request);
            return request;
        }
    }
}
