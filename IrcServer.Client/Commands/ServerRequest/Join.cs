using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrcServer.Client.Commands.ServerRequest
{
    class Join : IServerRequest
    {
        public void Run(string command)
        {
            ClientMessage.Info($"Joined channel {command}");
        }
    }
}
