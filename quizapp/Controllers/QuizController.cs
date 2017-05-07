using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using quizapp.Models;
using System.Data.Entity.Validation;

namespace quizapp.Controllers
{
    public class QuizController : Controller
    {
        private quizDBContainer db = new quizDBContainer();

        // GET: Quizs
        public ActionResult Index(string theme )
        {
           
            return View(db.QuizSet.Where(quiz => quiz.thematic == theme).ToList());
        }

        public ActionResult CreateQuestion(int id)
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateQuestion(int id,FormCollection fc)
        {
            Questions q = new Questions();
            q.question = fc["Questions.question"];
            Quiz quiz = db.QuizSet.Find(id);
            q.Quiz = quiz;
            Answers a = new Answers();
            a.name = fc["answer"];
            a.status = 1;
            a.Questions = q;
            q.Answers.Add(a);
            for(int i = 0; i < 3; i++)
            {
                Answers a1 = new Answers();
                a1.name = fc["answer_" + i];
                a1.status = 0;
                a1.Questions = q;
                q.Answers.Add(a1);
                db.AnswersSet.Add(a1);
            }
            db.QuestionsSet.Add(q);
            
           
            db.SaveChanges();
            return RedirectToAction("Edit","Quiz",new { Id = id });
        }

        // GET: Quizs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quiz quiz = db.QuizSet.Find(id);
            if (quiz == null)
            {
                return HttpNotFound();
            }
            return View(quiz);
        }

        // GET: Quizs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Quizs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,name,thematic")] Quiz quiz)
        {
            if (ModelState.IsValid)
            {
                db.QuizSet.Add(quiz);
                db.SaveChanges();
                return RedirectToAction("Index","Home",null);
            }

            return View(quiz);
        }

        // GET: Quizs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quiz quiz = db.QuizSet.Find(id);
            if (quiz == null)
            {
                return HttpNotFound();
            }
            ViewBag.quizName = quiz.name;
            ViewBag.quizId = id;
            var model = db.QuestionsSet.Join(db.AnswersSet, q => q.Id, a => a.Id,
                 (questions, answers) => new QuestionsAnswers { Questions = questions, Answers = answers }).Where(x => x.Answers.status == 1).
                 ToList();
            return View(model);
        }

        // POST: Quizs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,name,thematic")] Quiz quiz)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quiz).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(quiz);
        }

        // GET: Quizs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quiz quiz = db.QuizSet.Find(id);
            if (quiz == null)
            {
                return HttpNotFound();
            }
            return View(quiz);
        }

        // POST: Quizs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Quiz quiz = db.QuizSet.Find(id);
            db.QuizSet.Remove(quiz);
            db.QuestionsSet.RemoveRange(db.QuestionsSet.Where(q => q.Quiz.Id == id));
            List<Questions> L = db.QuestionsSet.Where(q => q.Quiz.Id == id).ToList();
            foreach(Questions q in L)
            {
                db.AnswersSet.RemoveRange(db.AnswersSet.Where(a => a.Questions.Id == q.Id));
            }
            
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Test(int id)
        {
            Questions ques = db.QuestionsSet.Where(q => q.Quiz.Id == id).OrderByDescending(q => q.Id).FirstOrDefault();
            if (ques == null) 
                return RedirectToAction("Edit", new {  Id = id});
            ViewBag.Question = ques;
            return View();
        }

       
        public ActionResult getQuestion(scoreInfo qs)
        {
             
            Questions ques = db.QuestionsSet.Where(q => q.Quiz.Id == qs.quizId).OrderByDescending(q => q.Id).Skip(qs.step).FirstOrDefault();
            ViewBag.Question = ques;
            return View();
        }


    }
}
