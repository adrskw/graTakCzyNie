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
            new Question {Id = 1, QuestionText = "Berlin jest stolicą Niemiec?", CorrectAnswer = true},
            new Question {Id = 2, QuestionText = "1 tona liczy 100 kg??", CorrectAnswer = false},
            new Question {Id = 3, QuestionText = "Śniardwy są największym jeziorem w Polsce?", CorrectAnswer = true},
            new Question {Id = 4, QuestionText = "Lewis Carroll to pseudonim Charles'a Lutwidge Dodgsona?", CorrectAnswer = true},
            new Question {Id = 5, QuestionText = "League of Legends jest grą RPG?", CorrectAnswer = false},
            new Question {Id = 6, QuestionText = "Leonardo da Vinci jest autorem Mona Lisy?", CorrectAnswer = true},
            new Question {Id = 7, QuestionText = "W Japonii pełnoletność uzyskuje się w wieku 21 lat?", CorrectAnswer = false},
            new Question {Id = 8, QuestionText = "Kot singapurski to najmniejsza i najlżejsza rasa kotów na świecie?", CorrectAnswer = true},
            new Question {Id = 9, QuestionText = "Biceps to najsilniejszy mięsień w ludzkim ciele?", CorrectAnswer = false},
            new Question {Id = 10, QuestionText = "Liczba pi wynosi około 3,14?", CorrectAnswer = true},
            new Question {Id = 11, QuestionText = "Ziemia jest czwartą planetą od Słońca?", CorrectAnswer = false},
            new Question {Id = 12, QuestionText = "Czy mur Berliński runął w 1989r?", CorrectAnswer = true},
            new Question {Id = 13, QuestionText = "Czy Marilyn Monroe miała wiecej niż 170 cm wzrostu?", CorrectAnswer = false},
            new Question {Id = 14, QuestionText = "Czy CH4O to wzór sumaryczny metanolu?", CorrectAnswer = true},
            new Question {Id = 15, QuestionText = "Czy sambar należy do rodziny jeleniowatych?", CorrectAnswer = true},
            new Question {Id = 16, QuestionText = "Czy Val Kilmer wystąpił w filmie Gorączka?", CorrectAnswer = true},
            new Question {Id = 17, QuestionText = "Czy Stefan Żeromski jest autorem Lalki", CorrectAnswer = false}
        };
        
        public async Task<Question> GetRandomQuestion()
        {
            return await Task.Run(() =>
            {
                Question question = questions[random.Next(0, questions.Count)];
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
