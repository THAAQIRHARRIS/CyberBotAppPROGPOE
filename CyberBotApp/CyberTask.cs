using System;

namespace CyberBotApp
{
    public class CyberTask
    {
        // =========================
        // DATABASE / IDENTIFIER
        // =========================
        public int Id { get; set; }

        // =========================
        // TASK DETAILS
        // =========================
        public string Title { get; set; }
        public string Description { get; set; }

        // Optional reminder date
        public DateTime? ReminderDate { get; set; }

        // Task completion status
        public bool IsCompleted { get; set; }

        // =========================
        // DEFAULT CONSTRUCTOR
        // =========================
        public CyberTask()
        {
            Title = "";
            Description = "";
            ReminderDate = null;
            IsCompleted = false;
        }

        // =========================
        // MAIN CONSTRUCTOR
        // =========================
        public CyberTask(string title, string description, DateTime? reminderDate)
        {
            Title = title;
            Description = description;
            ReminderDate = reminderDate;
            IsCompleted = false;
        }

        // =========================
        // DISPLAY FORMAT (UI)
        // =========================
        public override string ToString()
        {
            string reminderText = ReminderDate.HasValue
                ? ReminderDate.Value.ToString("yyyy-MM-dd")
                : "No reminder";

            string status = IsCompleted ? "Completed" : "Pending";

            return $"Title: {Title}\n" +
                   $"Description: {Description}\n" +
                   $"Reminder: {reminderText}\n" +
                   $"Status: {status}";
        }
    }
}