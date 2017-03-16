using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrcServer.Client.Commands.Slash
{
    class SetChannel : ISlashCommand
    {
        public void Run(string value)
        {
            // Sanity check
            if (value.IndexOf('#') != 0)
            {
                ClientMessage.Info("Malformed channel format");
                return;
            }

            Client.SetActiveChannel(value);
        }
    }
}
