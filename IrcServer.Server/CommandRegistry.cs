using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IrcServer.Irc;

namespace IrcServer
{
    static class CommandRegistry
    {
        public static Dictionary<string, ICommand> Commands { get; private set; }

        static CommandRegistry()
        {
            Commands = new Dictionary<string, ICommand>();
        }

        public static void RegisterCommand(string command, ICommand callback)
        {
            command = command.ToUpper();
            Commands.Add(command, callback);
        }
    }
}
