using System;
using System.Collections.Generic;
using System.Windows;
using NAudio.Wave;

namespace CyberBotApp
{
    public partial class MainWindow : Window
    {
        private string userName = "User";

        private Dictionary<string, List<string>> responses;

        private Random random = new Random();

        private string lastTopic = "";

        private string rememberedTopic = "";

        public MainWindow()
        {
            InitializeComponent();

            InitializeResponses();

            DisplayMessage("Initializing bootup protocol...");
            DisplayMessage("Welcome to CyberBot.");
            DisplayMessage("Access Granted.");
            DisplayMessage("Ask me anything about cybersecurity.");
            DisplayMessage("Type 'exit' to close the application.");

            PlayStartupSound();
        }

        private void InitializeResponses()
        {
            responses = new Dictionary<string, List<string>>()
            {
                {
                    "password",
                    new List<string>()
                    {
                        "Use strong passwords with at least 12 characters.",
                        "Avoid using birthdays or personal names in passwords.",
                        "A password manager can help keep your passwords secure."
                    }
                },

                {
                    "phishing",
                    new List<string>()
                    {
                        "Never click suspicious email links.",
                        "Scammers often pretend to be trusted organisations.",
                        "Always verify email senders before opening attachments."
                    }
                },

                {
                    "privacy",
                    new List<string>()
                    {
                        "Review your social media privacy settings regularly.",
                        "Avoid sharing sensitive information publicly online.",
                        "Enable two-factor authentication for better privacy."
                    }
                },

                {
                    "scam",
                    new List<string>()
                    {
                        "Be cautious of offers that sound too good to be true.",
                        "Scammers often create urgency to pressure victims.",
                        "Never send money to unknown online contacts."
                    }
                },

                {
                    "safe browsing",
                    new List<string>()
                    {
                        "Always check for HTTPS websites before entering personal information.",
                        "Avoid downloading files from untrusted websites.",
                        "Keep your browser updated for better security."
                    }
                }
            };
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

                DisplayMessage($"[{userName}] > {input}");

                ProcessInput(input);

                UserInputBox.Clear();
            }
            catch
            {
                DisplayMessage("An unexpected error occurred.");
            }
        }

        private void ProcessInput(string input)
        {
            try
            {
                string userInput = input.ToLower().Trim();

                // Exit application
                if (userInput == "exit" || userInput == "quit")
                {
                    Application.Current.Shutdown();
                    return;
                }

                // Greetings
                if (userInput.Contains("hello") ||
                    userInput.Contains("hey") ||
                    userInput.Contains("hi"))
                {
                    DisplayMessage($"Hello {userName}. How can I help you today?");
                    return;
                }

                // How are you
                if (userInput.Contains("how are you"))
                {
                    DisplayMessage("I am functioning perfectly and ready to help.");
                    return;
                }

                // Purpose
                if (userInput.Contains("purpose") ||
                    userInput.Contains("what do you do"))
                {
                    DisplayMessage("My purpose is to help users stay safe online and learn cybersecurity.");
                    return;
                }

                // Sentiment Detection
                if (userInput.Contains("worried") ||
                    userInput.Contains("scared") ||
                    userInput.Contains("frustrated"))
                {
                    DisplayMessage("I understand your concern. Cybersecurity can feel overwhelming sometimes.");

                    if (userInput.Contains("scam"))
                    {
                        DisplayMessage("Remember: never share banking details with unverified people online.");
                        lastTopic = "scam";
                        return;
                    }
                }

                // Curious sentiment
                if (userInput.Contains("curious"))
                {
                    DisplayMessage("Curiosity is excellent in cybersecurity. Learning helps you stay protected.");
                    return;
                }

                // Memory Feature
                if (userInput.Contains("interested in"))
                {
                    string[] parts = userInput.Split(
                        new string[] { "interested in" },
                        StringSplitOptions.None);

                    if (parts.Length > 1)
                    {
                        rememberedTopic = parts[1].Trim();

                        DisplayMessage(
                            $"Great! I'll remember that you're interested in {rememberedTopic}.");

                        return;
                    }
                }

                // Recall Memory
                if (!string.IsNullOrEmpty(rememberedTopic) &&
                    userInput.Contains("recommend"))
                {
                    DisplayMessage(
                        $"Since you're interested in {rememberedTopic}, I recommend researching best practices related to it.");

                    return;
                }

                // Conversation Flow
                if (userInput.Contains("tell me more") ||
                    userInput.Contains("another tip") ||
                    userInput.Contains("more"))
                {
                    if (!string.IsNullOrEmpty(lastTopic) &&
                        responses.ContainsKey(lastTopic))
                    {
                        List<string> responseList = responses[lastTopic];

                        string randomResponse =
                            responseList[random.Next(responseList.Count)];

                        DisplayMessage(randomResponse);

                        return;
                    }

                    DisplayMessage(
                        "Please specify which cybersecurity topic you'd like more information about.");

                    return;
                }

                // Main Keyword Detection
                foreach (var keyword in responses.Keys)
                {
                    if (userInput.Contains(keyword))
                    {
                        lastTopic = keyword;

                        List<string> responseList = responses[keyword];

                        string randomResponse =
                            responseList[random.Next(responseList.Count)];

                        DisplayMessage(randomResponse);

                        return;
                    }
                }

                // Ask command
                if (userInput.Contains("ask"))
                {
                    DisplayMessage(
                        "You can ask about passwords, phishing, scams, privacy, or safe browsing.");

                    return;
                }

                // Unknown input
                DisplayMessage(
                    "I'm not sure I understand. Can you try rephrasing?");
            }
            catch
            {
                DisplayMessage(
                    "Something went wrong, but CyberBot is still running safely.");
            }
        }

        private void DisplayMessage(string message)
        {
            ChatBox.AppendText(
                message + Environment.NewLine + Environment.NewLine);

            ChatBox.ScrollToEnd();
        }

        private void PlayStartupSound()
        {
            try
            {
                var audioFile =
                    new AudioFileReader("CyberBotgreeting.wav");

                var outputDevice = new WaveOutEvent();

                outputDevice.Init(audioFile);

                outputDevice.Play();
            }
            catch
            {
                DisplayMessage("Startup audio could not be played.");
            }
        }
    }
}