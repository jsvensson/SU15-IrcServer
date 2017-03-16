using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrcServer.Client.Commands.Slash
{
    class Join : ISlashCommand
    {
        public void Run(string command)
        {
            ClientConnection.WriteLine($"JOIN {command}");
        }
    }
}
