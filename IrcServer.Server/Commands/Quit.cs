namespace IrcServer.Commands
{
    class Quit : IServerCommand
    {
        public void Run(User user, string command)
        {
            if (command == null)
            {
                command = "Connection reset by peer";
            }

            user.Disconnect(command);
        }
    }
}
