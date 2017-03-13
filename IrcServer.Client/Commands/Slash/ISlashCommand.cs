namespace IrcServer.Client.Commands.Slash
{
    public interface ISlashCommand
    {
        void Run(string command);
    }
}