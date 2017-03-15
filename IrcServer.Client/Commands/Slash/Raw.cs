using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrcServer.Client.Commands.Slash
{
    class Raw : ISlashCommand
    {
        public void Run(string command)
        {
            ClientConnection.WriteLine(command).Wait();
        }
    }
}
