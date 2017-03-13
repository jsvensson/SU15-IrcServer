namespace IrcServer.Client.Commands.Slash
{
    class Connect : ISlashCommand
    {
        public void Run(string command)
        {
            string[] args = command.Trim().Split(' ');
            string host = args[0];
            int port = int.Parse(args[1]);

            ClientMessage.Info($"Connecting to {host}:{port}...");
            ClientConnection.Connect(host, port);
        }
    }
}
