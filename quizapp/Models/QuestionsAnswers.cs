using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace quizapp.Models
{
    public class QuestionsAnswers
    {
        public  Questions Questions { get; set; }
        public Answers Answers { get; set; }
    }
}