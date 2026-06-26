using System;
using System.Collections.Generic;
using System.Linq;

namespace CyberBotApp
{
    public class CyberBot
    {
        // ================= CHAT DATA =================
        private Dictionary<string, List<string>> responses;
        private Random random = new Random();

        // ================= MEMORY =================
        private string rememberedTopic = "";

        // ================= TASKS =================
        private List<CyberTask> tasks = new List<CyberTask>();

        // ================= ACTIVITY LOG =================
        private List<string> activityLog = new List<string>();

        public CyberBot()
        {
            InitializeResponses();
        }

        // ================= RESPONSES =================
        private void InitializeResponses()
        {
            responses = new Dictionary<string, List<string>>()
            {
                { "password", new List<string>
                    {
                        "Use strong passwords with 12+ characters.",
                        "Avoid personal information in passwords.",
                        "Use a password manager."
                    }
                },

                { "phishing", new List<string>
                    {
                        "Never click suspicious links.",
                        "Check sender emails carefully.",
                        "Report phishing attempts."
                    }
                },

                { "privacy", new List<string>
                    {
                        "Enable 2FA for better protection.",
                        "Review privacy settings regularly.",
                        "Avoid oversharing online."
                    }
                },

                { "scam", new List<string>
                    {
                        "Be careful of urgent messages.",
                        "Never send money to unknown sources.",
                        "Verify before trusting requests."
                    }
                },

                { "safe browsing", new List<string>
                    {
                        "Use HTTPS websites only.",
                        "Avoid unknown downloads.",
                        "Keep browser updated."
                    }
                }
            };
        }

        // ================= ACTIVITY LOG =================
        private void AddLog(string action)
        {
            activityLog.Add($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {action}");

            if (activityLog.Count > 10)
                activityLog.RemoveAt(0);
        }

        public List<string> GetActivityLog()
        {
            return activityLog.ToList();
        }

        // ================= TASK SYSTEM =================
        public void AddTask(CyberTask task)
        {
            tasks.Add(task);
            AddLog($"Task added: {task.Title}");
        }

        public List<CyberTask> GetTasks()
        {
            return tasks;
        }

        public void DeleteTask(string title)
        {
            var task = tasks.FirstOrDefault(t => t.Title == title);

            if (task != null)
            {
                tasks.Remove(task);
                AddLog($"Task deleted: {title}");
            }
        }

        public void CompleteTask(string title)
        {
            var task = tasks.FirstOrDefault(t => t.Title == title);

            if (task != null)
            {
                task.IsCompleted = true;
                AddLog($"Task completed: {title}");
            }
        }

        // ================= NLP INTENT DETECTION =================
        private string DetectIntent(string input)
        {
            input = input.ToLower();

            if (input.Contains("add task") || input.Contains("create task"))
                return "ADD_TASK";

            if (input.Contains("delete task") || input.Contains("remove task"))
                return "DELETE_TASK";

            if (input.Contains("complete task") || input.Contains("finish task"))
                return "COMPLETE_TASK";

            if (input.Contains("quiz") || input.Contains("start quiz"))
                return "QUIZ";

            if (input.Contains("log") || input.Contains("activity"))
                return "LOG";

            if (input.Contains("remind"))
                return "REMINDER";

            if (input.Contains("password") ||
                input.Contains("phishing") ||
                input.Contains("scam") ||
                input.Contains("privacy"))
                return "CYBER_TOPIC";

            return "UNKNOWN";
        }

        // ================= MAIN CHAT FUNCTION =================
        public string GetResponse(string input, string userName)
        {
            string intent = DetectIntent(input);
            AddLog($"Intent detected: {intent}");

            switch (intent)
            {
                case "ADD_TASK":
                    string taskText = input
                        .Replace("add task", "", StringComparison.OrdinalIgnoreCase)
                        .Replace("create task", "", StringComparison.OrdinalIgnoreCase)
                        .Trim();

                    if (!string.IsNullOrEmpty(taskText))
                    {
                        CyberTask task = new CyberTask
                        {
                            Id = new Random().Next(1000, 9999),
                            Title = taskText,
                            Description = "Added via NLP",
                            IsCompleted = false
                        };

                        AddTask(task);

                        return $"Task added: {task.Title}";
                    }

                    return "Please specify the task.";

                case "DELETE_TASK":
                    AddLog("Delete task requested");
                    return "Go to Task tab to delete a task.";

                case "COMPLETE_TASK":
                    AddLog("Complete task requested");
                    return "Go to Task tab to complete a task.";

                case "QUIZ":
                    AddLog("Quiz started");
                    return "Go to Quiz tab to start the game.";

                case "LOG":
                    AddLog("Activity log requested");
                    return string.Join("\n", activityLog);

                case "REMINDER":
                    AddLog("Reminder requested");
                    return "Reminder noted (database version will store dates).";

                case "CYBER_TOPIC":
                    return HandleCyberTopics(input);

                default:
                    AddLog("Unknown input");
                    return "Try asking about passwords, phishing, scams, or tasks.";
            }
        }

        // ================= CYBER TOPIC HANDLER =================
        private string HandleCyberTopics(string input)
        {
            input = input.ToLower();

            if (input.Contains("password"))
            {
                AddLog("Discussed password safety");
                return GetRandom("password");
            }

            if (input.Contains("phishing"))
            {
                AddLog("Discussed phishing");
                return GetRandom("phishing");
            }

            if (input.Contains("scam"))
            {
                AddLog("Discussed scams");
                return GetRandom("scam");
            }

            if (input.Contains("privacy"))
            {
                AddLog("Discussed privacy");
                return GetRandom("privacy");
            }

            if (input.Contains("safe browsing"))
            {
                AddLog("Discussed safe browsing");
                return GetRandom("safe browsing");
            }

            return "Ask about passwords, phishing, scams, or privacy.";
        }

        // ================= RANDOM RESPONSE =================
        private string GetRandom(string key)
        {
            var list = responses[key];
            return list[random.Next(list.Count)];
        }
    }
}