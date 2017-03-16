namespace IrcServer.Client.Commands.Slash
{
    /// <summary>
    /// Sends a raw text string to the server.
    /// </summary>
    class Raw : ISlashCommand
    {
        public void Run(string command)
        {
            ClientConnection.WriteLine(command).Wait();
        }
    }
}
