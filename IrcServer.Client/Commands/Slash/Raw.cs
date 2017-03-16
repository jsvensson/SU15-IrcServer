namespace IrcServer.Client.Commands.Slash
{
    /// <summary>
    /// Sends a raw text string to the server.
    /// </summary>
    class Raw : ISlashCommand
    {
        public async void Run(string command)
        {
            await ClientConnection.WriteLine(command);
        }
    }
}
