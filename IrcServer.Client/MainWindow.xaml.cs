using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IrcServer.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Hook main window up to messenger
            ClientMessage.SetTarget(ChannelTextBlock);

            // Register slash commands
            SlashCommandRegistry.RegisterHandler("connect", new Commands.Slash.Connect());
            SlashCommandRegistry.RegisterHandler("disconnect", new Commands.Slash.Disconnect());
            SlashCommandRegistry.RegisterHandler("raw", new Commands.Slash.Raw());
            SlashCommandRegistry.RegisterHandler("chan", new Commands.Slash.SetChannel());
            SlashCommandRegistry.RegisterHandler("join", new Commands.Slash.Join());

            // Register server requests
            ServerRequestRegistry.RegisterHandler("NOTICE", new Commands.ServerRequest.Notice());
            ServerRequestRegistry.RegisterHandler("JOIN", new Commands.ServerRequest.Join());
            ServerRequestRegistry.RegisterHandler("PRIVMSG", new Commands.ServerRequest.PrivMsg());
        }

        private void InputTextBox_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Return) return;

            string text = InputTextBox.Text;

            // Check for slash command
            if (text.IndexOf('/') == 0)
            {
                SlashParser.Parse(this, text);
            }
            else
            {
                // Send to active channel
                Client.ChannelMessage(text);
            }

            // Clear input, scroll to bottom
            InputTextBox.Text = string.Empty;
            ChatScrollViewer.ScrollToBottom();
        }
    }
}
