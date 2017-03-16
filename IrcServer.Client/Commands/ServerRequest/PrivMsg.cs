using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IrcServer.Client.Commands.ServerRequest
{
    class PrivMsg : IServerRequest
    {
        public void Run(string value)
        {
            // Check message type
            if (value.IndexOf('#') == 0)
            {
                WriteChannelMessage(value);
            }
            else
            {
                WritePrivateMessage(value);
            }
        }

        private void WriteChannelMessage(string value)
        {
            string channel = value.Substring(0, value.IndexOf(' '));
            string message = value.Substring(value.IndexOf(':') + 1);

            // Get user
            int userStart = value.IndexOf(' ') + 1;
            int userLength = value.IndexOf(':') - 1 - userStart;
            string user = value.Substring(userStart, userLength);

            ClientMessage.ChannelMessage(channel, user, message);
        }

        private void WritePrivateMessage(string value)
        {
            ClientMessage.Notice(value);
        }
    }
}
