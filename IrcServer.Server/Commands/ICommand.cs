namespace IrcServer.Irc
{
    public interface ICommand
    {
        string Run(string command);
    }
}