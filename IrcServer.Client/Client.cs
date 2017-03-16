namespace IrcServer.Client
{
    public static class Client
    {
        public static string ActiveChannel { get; private set; } = string.Empty;

        public static void SetActiveChannel(string name)
        {
            ActiveChannel = name;
            ClientMessage.Info($"Active channel: {name}");
        }

        public static void ChannelMessage(string message)
        {
            if (ActiveChannel.IndexOf('#') == 0)
            {
                string msg = $"PRIVMSG {ActiveChannel} {message}";
                ClientConnection.WriteLine(msg);
            }
        }
    }
}
