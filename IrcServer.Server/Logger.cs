using System;

namespace IrcServer
{
    public static class Logger
    {
        public static LogLevel LogLevel { get; set; } = LogLevel.Info;

        public static void Info(string message)
        {
            Console.WriteLine($"[INFO] {message}");
        }
    }

    [Flags]
    public enum LogLevel
    {
        Info = 1,
        Debug = 2,
        Warning = 4,
        Error = 8
    }
}