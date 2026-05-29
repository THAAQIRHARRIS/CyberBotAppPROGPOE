using System;
using System.Collections.Generic;

namespace CyberBotApp
{
    public class CyberBot
    {
        private Dictionary<string, List<string>> responses;

        private Random random = new Random();

        private string lastTopic = "";

        private string rememberedTopic = "";

        public CyberBot()
        {
            InitializeResponses();
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

        public string GetResponse(string input, string userName)
        {
            string userInput = input.ToLower().Trim();

            // Greetings
            if (userInput.Contains("hello") ||
                userInput.Contains("hey") ||
                userInput.Contains("hi"))
            {
                return $"Hello {userName}. How can I help you today?";
            }

            // How are you
            if (userInput.Contains("how are you"))
            {
                return "I am functioning perfectly and ready to help.";
            }

            // Purpose
            if (userInput.Contains("purpose") ||
                userInput.Contains("what do you do"))
            {
                return "My purpose is to help users stay safe online and learn cybersecurity.";
            }

            // Sentiment Detection
            if (userInput.Contains("worried") ||
                userInput.Contains("scared") ||
                userInput.Contains("frustrated"))
            {
                if (userInput.Contains("scam"))
                {
                    lastTopic = "scam";

                    return "I understand your concern. Never share banking details with unverified people online.";
                }

                return "I understand your concern. Cybersecurity can feel overwhelming sometimes.";
            }

            // Curious sentiment
            if (userInput.Contains("curious"))
            {
                return "Curiosity is excellent in cybersecurity. Learning helps you stay protected.";
            }

            // Memory feature
            if (userInput.Contains("interested in"))
            {
                string[] parts = userInput.Split(
                    new string[] { "interested in" },
                    StringSplitOptions.None);

                if (parts.Length > 1)
                {
                    rememberedTopic = parts[1].Trim();

                    return $"Great! I'll remember that you're interested in {rememberedTopic}.";
                }
            }

            // Recall memory
            if (!string.IsNullOrEmpty(rememberedTopic) &&
                userInput.Contains("recommend"))
            {
                return $"Since you're interested in {rememberedTopic}, I recommend researching best practices related to it.";
            }

            // More info
            if (userInput.Contains("tell me more") ||
                userInput.Contains("another tip") ||
                userInput.Contains("more"))
            {
                if (!string.IsNullOrEmpty(lastTopic) &&
                    responses.ContainsKey(lastTopic))
                {
                    List<string> responseList = responses[lastTopic];

                    return responseList[random.Next(responseList.Count)];
                }

                return "Please specify which cybersecurity topic you'd like more information about.";
            }

            // Keyword detection
            foreach (var keyword in responses.Keys)
            {
                if (userInput.Contains(keyword))
                {
                    lastTopic = keyword;

                    List<string> responseList = responses[keyword];

                    return responseList[random.Next(responseList.Count)];
                }
            }

            // Ask command
            if (userInput.Contains("ask"))
            {
                return "You can ask about passwords, phishing, scams, privacy, or safe browsing.";
            }

            return "I'm not sure I understand. Can you try rephrasing?";
        }
    }
}
