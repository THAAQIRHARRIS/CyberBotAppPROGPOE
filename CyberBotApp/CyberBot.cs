using System;
using System.Collections.Generic;
using System.Linq;

namespace CyberBotApp
{
    public class CyberBot
    {
        private Dictionary<string, List<string>> responses;
        private Random random = new Random();

        private string lastTopic = "";
        private string rememberedTopic = "";

        // PART 3
        private List<string> activityLog = new List<string>();
        private List<string> tasks = new List<string>();

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

        // ACTIVITY LOG
        private void AddLog(string action)
        {
            activityLog.Add($"{DateTime.Now:g} - {action}");

            if (activityLog.Count > 10)
            {
                activityLog.RemoveAt(0);
            }
        }

        public List<string> GetActivityLog()
        {
            return activityLog;
        }

        // TASKS
        public void AddTask(string task)
        {
            tasks.Add(task);
            AddLog($"Task added: {task}");
        }

        public List<string> GetTasks()
        {
            return tasks;
        }

        public string GetResponse(string input, string userName)
        {
            string userInput = input.ToLower().Trim();

            // Greeting
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

            // -----------------------
            // NLP TASK DETECTION
            // -----------------------

            if (userInput.Contains("add task") ||
                userInput.Contains("new task") ||
                userInput.Contains("create task"))
            {
                string task = input;

                task = task.Replace("add task", "", StringComparison.OrdinalIgnoreCase);
                task = task.Replace("new task", "", StringComparison.OrdinalIgnoreCase);
                task = task.Replace("create task", "", StringComparison.OrdinalIgnoreCase);

                task = task.Trim();

                if (task != "")
                {
                    AddTask(task);

                    return $"Task added successfully:\n{task}";
                }

                return "Please tell me what task you'd like to add.";
            }

            // Reminder detection
            if (userInput.Contains("remind me"))
            {
                AddLog("Reminder created");

                return "Reminder noted! (Reminder dates will be added in the database version.)";
            }

            // Show tasks
            if (userInput.Contains("show tasks") ||
                userInput.Contains("my tasks"))
            {
                if (tasks.Count == 0)
                {
                    return "You currently have no tasks.";
                }

                return "Your Tasks:\n\n• " +
                       string.Join("\n• ", tasks);
            }

            // Quiz
            if (userInput.Contains("quiz") ||
                userInput.Contains("start quiz"))
            {
                AddLog("Quiz started");

                return "Starting Cybersecurity Quiz...";
            }

            // Activity Log
            if (userInput.Contains("activity") ||
                userInput.Contains("what have you done") ||
                userInput.Contains("show log"))
            {
                if (activityLog.Count == 0)
                {
                    return "No recent activity.";
                }

                return "Recent Activity\n\n" +
                       string.Join("\n", activityLog);
            }

            // Sentiment Detection
            if (userInput.Contains("worried") ||
                userInput.Contains("scared") ||
                userInput.Contains("frustrated"))
            {
                if (userInput.Contains("scam"))
                {
                    lastTopic = "scam";

                    AddLog("Discussed scam safety");

                    return "I understand your concern. Never share banking details with unverified people online.";
                }

                return "I understand your concern. Cybersecurity can feel overwhelming sometimes.";
            }

            // Curious
            if (userInput.Contains("curious"))
            {
                return "Curiosity is excellent in cybersecurity. Learning helps you stay protected.";
            }

            // Memory
            if (userInput.Contains("interested in"))
            {
                string[] parts = userInput.Split(
                    new string[] { "interested in" },
                    StringSplitOptions.None);

                if (parts.Length > 1)
                {
                    rememberedTopic = parts[1].Trim();

                    AddLog($"Remembered interest: {rememberedTopic}");

                    return $"Great! I'll remember that you're interested in {rememberedTopic}.";
                }
            }

            // Recall
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

            // Cybersecurity keyword detection
            foreach (var keyword in responses.Keys)
            {
                if (userInput.Contains(keyword))
                {
                    lastTopic = keyword;

                    AddLog($"Discussed {keyword}");

                    List<string> responseList = responses[keyword];

                    return responseList[random.Next(responseList.Count)];
                }
            }

            // Help
            if (userInput.Contains("help") ||
                userInput.Contains("ask"))
            {
                return
@"You can ask me about:

• Passwords
• Phishing
• Privacy
• Scams
• Safe Browsing

You can also say:

• Add Task Review passwords
• Show Tasks
• Start Quiz
• Show Activity Log";
            }

            return "I'm not sure I understand. Can you try rephrasing?";
        }
    }
}