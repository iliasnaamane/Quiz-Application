using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace quizapp.Models
{
    public class QuestionInfo
    {
        public int score { get; set; }
        public int step { get; set; }
        public Questions q { get; set; }
    }
}