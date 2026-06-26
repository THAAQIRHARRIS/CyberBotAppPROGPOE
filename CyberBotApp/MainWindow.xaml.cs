using System;
using System.Windows;

namespace CyberBotApp
{
    public partial class MainWindow : Window
    {
        private CyberBot bot;
        private QuizManager quiz;

        private string userName = "User";

        public MainWindow()
        {
            InitializeComponent();

            // ================= INIT SYSTEM =================
            bot = new CyberBot();
            quiz = new QuizManager();

            // ================= UI START STATE =================
            DisplayMessage("CyberBot is now running...");
            DisplayMessage("Ask me about cybersecurity or use tabs.");

            RefreshTasks();
            RefreshActivityLog();
        }

        // =====================================================
        // CHAT SYSTEM
        // =====================================================
        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                userName = UsernameBox.Text;

                if (string.IsNullOrWhiteSpace(userName))
                    userName = "User";

                string input = UserInputBox.Text;

                if (string.IsNullOrWhiteSpace(input))
                {
                    DisplayMessage("Please type a message.");
                    return;
                }

                if (input.ToLower() == "exit")
                {
                    Application.Current.Shutdown();
                    return;
                }

                DisplayMessage($"{userName}: {input}");

                string response = bot.GetResponse(input, userName);

                DisplayMessage("Bot: " + response);

                UserInputBox.Clear();

                // UPDATE UI AFTER EVERY CHAT
                RefreshTasks();
                RefreshActivityLog();
            }
            catch
            {
                DisplayMessage("Error: Something went wrong.");
            }
        }

        // =====================================================
        // DISPLAY CHAT
        // =====================================================
        private void DisplayMessage(string message)
        {
            ChatBox.AppendText(message + Environment.NewLine + Environment.NewLine);
            ChatBox.ScrollToEnd();
        }

        // =====================================================
        // TASK SYSTEM
        // =====================================================
        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TaskTitleBox.Text))
            {
                MessageBox.Show("Please enter a task title.");
                return;
            }

            CyberTask task = new CyberTask
            {
                Id = new Random().Next(1000, 9999),
                Title = TaskTitleBox.Text,
                Description = TaskDescriptionBox.Text,
                ReminderDate = ReminderDatePicker.SelectedDate,
                IsCompleted = false
            };

            bot.AddTask(task);

            RefreshTasks();
            RefreshActivityLog();

            ClearTaskInputs();
        }

        private void DeleteTaskButton_Click(object sender, RoutedEventArgs e)
        {
            if (TaskListBox.SelectedItem == null)
            {
                MessageBox.Show("Select a task first.");
                return;
            }

            string selected = TaskListBox.SelectedItem.ToString();

            bot.DeleteTask(selected);

            RefreshTasks();
            RefreshActivityLog();
        }

        private void CompleteTaskButton_Click(object sender, RoutedEventArgs e)
        {
            if (TaskListBox.SelectedItem == null)
            {
                MessageBox.Show("Select a task first.");
                return;
            }

            string selected = TaskListBox.SelectedItem.ToString();

            bot.CompleteTask(selected);

            RefreshTasks();
            RefreshActivityLog();
        }

        private void RefreshTasksButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshTasks();
        }

        private void RefreshTasks()
        {
            TaskListBox.Items.Clear();

            foreach (var task in bot.GetTasks())
            {
                string status = task.IsCompleted ? "✔ Done" : "⏳ Pending";
                TaskListBox.Items.Add($"{task.Title} [{status}]");
            }
        }

        private void ClearTaskInputs()
        {
            TaskTitleBox.Clear();
            TaskDescriptionBox.Clear();
            ReminderDatePicker.SelectedDate = null;
        }

        // =====================================================
        // QUIZ SYSTEM
        // =====================================================
        private void StartQuizButton_Click(object sender, RoutedEventArgs e)
        {
            quiz.Reset();
            LoadQuestion();

            RefreshActivityLog();
        }

        private void LoadQuestion()
        {
            var q = quiz.GetCurrentQuestion();

            if (q == null)
            {
                QuestionText.Text =
                    $"Quiz Completed!\nFinal Score: {quiz.GetScore()}/{quiz.GetTotal()}";

                OptionA.IsChecked = false;
                OptionB.IsChecked = false;
                OptionC.IsChecked = false;
                OptionD.IsChecked = false;

                return;
            }

            QuestionText.Text = q.Question;

            OptionA.Content = "A. " + q.OptionA;
            OptionB.Content = "B. " + q.OptionB;
            OptionC.Content = "C. " + q.OptionC;
            OptionD.Content = "D. " + q.OptionD;

            OptionA.IsChecked = false;
            OptionB.IsChecked = false;
            OptionC.IsChecked = false;
            OptionD.IsChecked = false;
        }

        private void NextQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            string selected = "";

            if (OptionA.IsChecked == true) selected = "A";
            else if (OptionB.IsChecked == true) selected = "B";
            else if (OptionC.IsChecked == true) selected = "C";
            else if (OptionD.IsChecked == true) selected = "D";

            if (string.IsNullOrEmpty(selected))
            {
                MessageBox.Show("Please select an answer.");
                return;
            }

            quiz.Answer(selected);

            ScoreLabel.Content = $"Score: {quiz.GetScore()}";

            LoadQuestion();

            RefreshActivityLog();
        }

        // =====================================================
        // ACTIVITY LOG SYSTEM
        // =====================================================
        private void RefreshActivityLog()
        {
            ActivityLogListBox.Items.Clear();

            foreach (var log in bot.GetActivityLog())
            {
                ActivityLogListBox.Items.Add(log);
            }
        }
    }
}