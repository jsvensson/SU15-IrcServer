using System;

namespace IrcServer.Commands
{
    class PrivMsg : IServerCommand
    {
        public void Run(User user, string command)
        {
            Tuple<string, string> message = GetMessage(command);

            Channel channel;
            Server.Channels.TryGetValue(message.Item1, out channel);

            if (channel != null)
            {
                channel.Message(user, message.Item2);
            }
            else
            {
                // Tell user they're not in that channel
                user.WriteLine($"ERROR Not member of channel {message.Item1}");
            }
        }

        private Tuple<string, string> GetMessage(string value)
        {
            string target = value.Substring(0, value.IndexOf(' ')).ToLower();
            string message = value.Substring(value.IndexOf(' ') + 1);

            return Tuple.Create(target, message);
        }
    }
}
