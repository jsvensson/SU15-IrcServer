namespace IrcServer.Commands
{
    public interface IServerCommand
    {
        void Run(User user, string command);
    }
}