using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graTakCzyNieLibrary
{
    public class Question
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public bool? CorrectAnswer { get; set; }
    }

    public class QuestionDatabase
    {
        private Random random = new Random();

        private List<Question> questions = new List<Question>()
        {
            new Question {Id = 1, QuestionText = "Czy jestem fajny?", CorrectAnswer = true},
            new Question {Id = 2, QuestionText = "Czy lubie placki?", CorrectAnswer = false},
            new Question {Id = 3, QuestionText = "Czy lubie kobiety?", CorrectAnswer = true},
        };

        public async Task<Question> GetRandomQuestion()
        {
            return await Task.Run(() =>
            {
                Question question = questions[random.Next(1, questions.Count)];
                return new Question
                {
                    Id = question.Id,
                    QuestionText = question.QuestionText,
                };
            });

        }
        public async Task<bool?> CheckAnswer(int questionId, bool answer)
        {
            return await Task.Run(() =>
            {
                var question = questions.FirstOrDefault(f => f.Id == questionId);
                if (question == null)
                {
                    return default;
                }
                if (question.CorrectAnswer == answer)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            });
        }
    }
}
