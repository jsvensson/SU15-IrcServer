using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;

namespace IrcServer.Client
{
    public static class ClientMessage
    {
        private static TextBlock textBlock;

        public static void SetTarget(TextBlock tb)
        {
            // Ugly as heck but I'm lazy today.
            textBlock = tb;
        }

        private static void WriteLine(string text)
        {
            // Check newline, add if needed
            if (text.IndexOf('\n') == -1)
            {
                text = text + '\n';
            }

            textBlock.Text += text;
        }

        public static void Info(string message)
        {
            WriteLine($"[INFO] {message}");
        }

        public static void Notice(string message)
        {
            WriteLine($"-notice- {message}");
        }

        public static void ChatMessage(string channel, string user, string message)
        {
            WriteLine($"{channel} <{user}> {message}");
        }
    }
}
