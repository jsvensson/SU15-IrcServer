using System;
using IrcServer.Irc;

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
