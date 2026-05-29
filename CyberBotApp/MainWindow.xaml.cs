using System;
using System.Windows;

namespace CyberBotApp
{
    public partial class MainWindow : Window
    {
        private CyberBot bot;

        private AudioManager audioManager;

        private string userName = "";

        public MainWindow()
        {
            InitializeComponent();

            bot = new CyberBot();

            audioManager = new AudioManager();

            DisplayMessage("Welcome to CyberBot.");
            DisplayMessage("Please enter your Username in the above space.");
            DisplayMessage("Ask me anything about cybersecurity.");
            DisplayMessage("Type 'exit' to close the application.");

            audioManager.PlayStartupSound();
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                userName = UsernameBox.Text.Trim();

                if (string.IsNullOrWhiteSpace(userName))
                {
                    userName = "Anonymous";
                }

                string input = UserInputBox.Text.Trim();

                if (string.IsNullOrWhiteSpace(input))
                {
                    DisplayMessage("Please enter a question.");
                    return;
                }

                if (input.ToLower() == "exit" ||
                    input.ToLower() == "quit")
                {
                    Application.Current.Shutdown();
                    return;
                }

                DisplayMessage($"[{userName}] > {input}");

                string response = bot.GetResponse(input, userName);

                DisplayMessage(response);

                UserInputBox.Clear();
            }
            catch
            {
                DisplayMessage("An unexpected error occurred.");
            }
        }

        private void DisplayMessage(string message)
        {
            ChatBox.AppendText(
                message + Environment.NewLine + Environment.NewLine);

            ChatBox.ScrollToEnd();
        }
    }
}