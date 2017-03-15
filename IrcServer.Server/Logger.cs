using System;

namespace IrcServer
{
    public static class Logger
    {
        public static LogLevel LogLevel { get; set; } = LogLevel.Info;

        public static void Info(string message)
        {
            Print("INFO", message);
        }

        public static void Warning(string message)
        {
            Print("WARNING", message, ConsoleColor.Red);
        }


        private static void Print(string type, string message, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine($"[{type}] {message}");
            Console.ResetColor();
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