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
        private List<Question> questions = new List<Question>()
        {
            new Question {Id = 1, QuestionText = "Czy jestem fajny?", CorrectAnswer = true},
            new Question {Id = 2, QuestionText = "Czy lubie placki?", CorrectAnswer = false},
            new Question {Id = 3, QuestionText = "Czy lubie kobiety?", CorrectAnswer = true},
        };
    }
}
