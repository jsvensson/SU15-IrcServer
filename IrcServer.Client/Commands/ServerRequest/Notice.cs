using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrcServer.Client.Commands.ServerRequest
{
    class Notice : IServerRequest
    {
        public void Run(string value)
        {
            ClientMessage.Notice(value);
        }
    }
}
