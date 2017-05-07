using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using quizapp.Models;

namespace quizapp.Controllers
{
    public class HomeController : Controller
    {
        private quizDBContainer db = new quizDBContainer();
        public ActionResult Index()
        {
           List<string> list = db.QuizSet.Select(quiz => quiz.thematic).Distinct().ToList();

            return View(list);
        }
    }
}
