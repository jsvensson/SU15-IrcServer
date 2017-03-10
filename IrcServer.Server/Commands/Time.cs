using System;

namespace IrcServer.Commands
{
    public class Time : IServerCommand
    {
        public void Run(User user, string command)
        {
            string serverTime = DateTime.Now.ToLongTimeString();
            user.WriteLine($"SERVERTIME {serverTime}");
        }
    }
}
