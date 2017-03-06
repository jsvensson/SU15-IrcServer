namespace IrcServer.Commands
{
    public interface ICommand
    {
        void Run(User user, string command);
    }
}