using System;

namespace CyberBotApp
{
    public class CyberTask
    {
        // Unique task ID (from MySQL)
        public int Id { get; set; }

        // Task title
        public string Title { get; set; }

        // Detailed description
        public string Description { get; set; }

        // Optional reminder date
        public DateTime? ReminderDate { get; set; }

        // Task completion status
        public bool IsCompleted { get; set; }

        // Default constructor
        public CyberTask()
        {
            Title = "";
            Description = "";
            ReminderDate = null;
            IsCompleted = false;
        }

        // Constructor with values
        public CyberTask(string title, string description, DateTime? reminderDate)
        {
            Title = title;
            Description = description;
            ReminderDate = reminderDate;
            IsCompleted = false;
        }

        // Display task nicely in the GUI
        public override string ToString()
        {
            string reminder = ReminderDate.HasValue
                ? ReminderDate.Value.ToShortDateString()
                : "No Reminder";

            string status = IsCompleted ? "Completed" : "Pending";

            return $"Title: {Title}\n" +
                   $"Description: {Description}\n" +
                   $"Reminder: {reminder}\n" +
                   $"Status: {status}";
        }
    }
}
