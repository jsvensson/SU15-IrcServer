﻿using System;
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

            // Register commands
            SlashCommandRegistry.RegisterCommand("connect", new Commands.Slash.Connect());
            SlashCommandRegistry.RegisterCommand("disconnect", new Commands.Slash.Disconnect());
            SlashCommandRegistry.RegisterCommand("raw", new Commands.Slash.Raw());
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

            // Clear input
            InputTextBox.Text = string.Empty;
        }
    }
}
