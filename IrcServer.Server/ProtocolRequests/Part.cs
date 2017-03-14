namespace IrcServer.ProtocolRequests
{
    class Part : IServerRequest
    {
        public void Run(User user, string channelName)
        {
            channelName = channelName.ToLower();

            Channel channel;
            Server.Channels.TryGetValue(channelName, out channel);
            channel?.UserPart(user);
        }
    }
}
