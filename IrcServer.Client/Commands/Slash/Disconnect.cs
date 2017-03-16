namespace IrcServer.Client.Commands.Slash
{
    class Disconnect : ISlashCommand
    {
        public void Run(string command)
        {
            ClientConnection.Disconnect();
        }
    }
}
