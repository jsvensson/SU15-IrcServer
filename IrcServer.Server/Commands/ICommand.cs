namespace IrcServer.Commands
{
    public interface ICommand
    {
        string Run(string command);
    }
}