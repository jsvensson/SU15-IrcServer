using System;

namespace IrcServer.Commands
{
    public class Time : ICommand
    {
        public string Run(string command)
        {
            string serverTime = DateTime.Now.ToLongTimeString();
            return $"SERVERTIME {serverTime}";
        }
    }
}
