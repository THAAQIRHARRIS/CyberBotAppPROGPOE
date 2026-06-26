using System.Collections.Generic;

namespace CyberBotApp
{
    public class QuizManager
    {
        private List<QuizQuestion> questions;
        private int index = 0;
        private int score = 0;

        public QuizManager()
        {
            LoadQuestions();
        }

        private void LoadQuestions()
        {
            questions = new List<QuizQuestion>
            {
                new QuizQuestion
                {
                    Question = "What is phishing?",
                    OptionA = "Fake emails stealing data",
                    OptionB = "Firewall system",
                    OptionC = "Antivirus software",
                    OptionD = "VPN",
                    CorrectAnswer = "A"
                },

                new QuizQuestion
                {
                    Question = "What should you do with suspicious emails?",
                    OptionA = "Open them",
                    OptionB = "Delete or report them",
                    OptionC = "Reply immediately",
                    OptionD = "Forward them",
                    CorrectAnswer = "B"
                },

                new QuizQuestion
                {
                    Question = "What is a strong password?",
                    OptionA = "123456",
                    OptionB = "Your name",
                    OptionC = "Password123",
                    OptionD = "Random mix of characters",
                    CorrectAnswer = "D"
                },

                new QuizQuestion
                {
                    Question = "What does 2FA mean?",
                    OptionA = "Two File Access",
                    OptionB = "Two Factor Authentication",
                    OptionC = "Fast login",
                    OptionD = "Firewall access",
                    CorrectAnswer = "B"
                },

                new QuizQuestion
                {
                    Question = "What is malware?",
                    OptionA = "Helpful software",
                    OptionB = "Security tool",
                    OptionC = "Malicious software",
                    OptionD = "Browser plugin",
                    CorrectAnswer = "C"
                }
            };
        }

        public QuizQuestion GetCurrentQuestion()
        {
            if (index >= questions.Count)
                return null;

            return questions[index];
        }

        public bool Answer(string selected)
        {
            if (questions[index].CorrectAnswer == selected)
                score++;

            index++;
            return true;
        }

        public int GetScore() => score;

        public int GetTotal() => questions.Count;

        public void Reset()
        {
            index = 0;
            score = 0;
        }
    }
}