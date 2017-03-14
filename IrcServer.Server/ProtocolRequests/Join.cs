using System.Text.RegularExpressions;

namespace IrcServer.ProtocolRequests
{
    class Join : IServerRequest
    {
        public void Run(User user, string channelName)
        {
            channelName = channelName.ToLower();

            // Sanity check on channel name
            var validChannelName = new Regex(@"^#[a-z0-9]+$");
            bool validName = validChannelName.IsMatch(channelName);

            if (!validName)
            {
                user.WriteLine($"{(int)NumericReply.NoSuchChannel} {channelName} :No such channel");
                return;
            }

            // Check if channel already exists
            Channel channel;
            Server.Channels.TryGetValue(channelName, out channel);

            if (channel == null)
            {
                channel = Server.CreateChannel(channelName);
            }

            Logger.Info($"User {user.Nickname} joined {channel.Name}");
            Server.Channels[channelName].UserJoin(user);
        }

    }
}
