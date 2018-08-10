using Praescio.CustomFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Praescio.Controllers
{
    [ChangePasswordFirstLogin]
    public class InstitutionStudentController : Controller
    {
        // GET: InstitutionStudent
        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult StudentProfile(int? StudentAccountId,bool isIndividual=false)
        {
            ViewBag.StudentAccountId = StudentAccountId;
            ViewBag.IsIndividual = isIndividual;
            return View();
        }
        public ActionResult SchoolProfile()
        {
            return View();
        }
        public ActionResult AssignmentList()
        {
            return View();
        }

        public ActionResult TestStart()
        {
            return View();
        }
        public ActionResult Meaningoflesson(int assignmentid, int typeOfQuestion)
        {
            ViewBag.Assignmentid = assignmentid;
            ViewBag.TypeOfQuestion = typeOfQuestion;

            return View();
        }
        public ActionResult Synonyms(int assignmentid, int typeOfQuestion)
        {
            ViewBag.Assignmentid = assignmentid;
            ViewBag.TypeOfQuestion = typeOfQuestion;

            return View();
        }
        public ActionResult Antonyms(int assignmentid, int typeOfQuestion)
        {
            ViewBag.Assignmentid = assignmentid;
            ViewBag.TypeOfQuestion = typeOfQuestion;

            return View();
        }
        public ActionResult WriteReason(int assignmentid, int typeOfQuestion)
        {
            ViewBag.Assignmentid = assignmentid;
            ViewBag.TypeOfQuestion = typeOfQuestion;

            return View();
        }
        public ActionResult FillInTheBlanks(int assignmentid, int typeOfQuestion)
        {
            ViewBag.Assignmentid = assignmentid;
            ViewBag.TypeOfQuestion = typeOfQuestion;

            return View();
        }
        public ActionResult MatchTheFollowing(int assignmentid, int typeOfQuestion)
        {
            ViewBag.Assignmentid = assignmentid;
            ViewBag.TypeOfQuestion = typeOfQuestion;

            return View();
        }
        public ActionResult MultipleChoiceQuestions(int assignmentid, int typeOfQuestion)
        {
            ViewBag.Assignmentid = assignmentid;
            ViewBag.TypeOfQuestion = typeOfQuestion;

            return View();
        }
        public ActionResult TrueFalseQuestions(int assignmentid, int typeOfQuestion)
        {
            ViewBag.Assignmentid = assignmentid;
            ViewBag.TypeOfQuestion = typeOfQuestion;

            return View();
        }
        public ActionResult WriteAnswerInOneSentence(int assignmentid, int typeOfQuestion)
        {
            ViewBag.Assignmentid = assignmentid;
            ViewBag.TypeOfQuestion = typeOfQuestion;

            return View();
        }
        public ActionResult DescribeBriefly(int assignmentid, int typeOfQuestion)
        {
            ViewBag.Assignmentid = assignmentid;
            ViewBag.TypeOfQuestion = typeOfQuestion;

            return View();
        }
        public ActionResult DifferentiateBetween(int assignmentid, int typeOfQuestion)
        {
            ViewBag.Assignmentid = assignmentid;
            ViewBag.TypeOfQuestion = typeOfQuestion;

            return View();
        }
        public ActionResult Exercise(int assignmentid, int typeOfQuestion)
        {
            ViewBag.Assignmentid = assignmentid;
            ViewBag.TypeOfQuestion = typeOfQuestion;

            return View();
        }
        public ActionResult WriteShortNote(int assignmentid, int typeOfQuestion)
        {
            ViewBag.Assignmentid = assignmentid;
            ViewBag.TypeOfQuestion = typeOfQuestion;

            return View();
        }
        public ActionResult ConceptOfAnyTopicOfLesson(int assignmentid, int typeOfQuestion)
        {
            ViewBag.Assignmentid = assignmentid;
            ViewBag.TypeOfQuestion = typeOfQuestion;

            return View();
        }
        public ActionResult ViewVideo(int assignmentid, int typeOfQuestion)
        {
            ViewBag.Assignmentid = assignmentid;
            ViewBag.TypeOfQuestion = typeOfQuestion;

            return View();
        }

        public ActionResult HandwritingDev()
        {
            return View();
        }
        public ActionResult PronunciationDev()
        {
            return View();
        }
        public ActionResult LessonsHistory()
        {
            return View();
        }
        public ActionResult TestHistory(int? assignmentId)
        {
            ViewBag.AssignmentId = assignmentId;
            return View();
        }

        public ActionResult WriteReasonHistory(int? assignmentId)
        {
            ViewBag.AssignmentId = assignmentId;
            return View();
        }

        public ActionResult FillInTheBlanksHistory(int? assignmentId)
        {
            ViewBag.AssignmentId = assignmentId;
            return View();
        }
        public ActionResult MatchTheFollowingHistory(int? assignmentId)
        {
            ViewBag.AssignmentId = assignmentId;
            return View();
        }
        public ActionResult MultipleChoiceQuestionsHistory(int? assignmentId)
        {
            ViewBag.AssignmentId = assignmentId;
            return View();
        }
        public ActionResult TrueFalseQuestionsHistory(int? assignmentId)
        {
            ViewBag.AssignmentId = assignmentId;
            return View();
        }
        public ActionResult WriteAnswerInOneSentenceHistory(int? assignmentId)
        {
            ViewBag.AssignmentId = assignmentId;
            return View();
        }
        public ActionResult DescribeBrieflyHistory(int? assignmentId)
        {
            ViewBag.AssignmentId = assignmentId;
            return View();
        }
        public ActionResult DifferentiateBetweenHistory(int? assignmentId)
        {
            ViewBag.AssignmentId = assignmentId;
            return View();
        }
        public ActionResult ExerciseHistory(int? assignmentId)
        {
            ViewBag.AssignmentId = assignmentId;
            return View();
        }
        public ActionResult WriteShortNoteHistory(int? assignmentId)
        {
            ViewBag.AssignmentId = assignmentId;
            return View();
        }

        public ActionResult UploadCreativity()
        {
            return View();
        }

        public ActionResult CreativityHistory()
        {
            return View();
        }
        public ActionResult CourseCompletionStatus()
        {
            return View();
        }
        public ActionResult OverallProgress()
        {
            return View();
        }

        public ActionResult ViewHandwritingDev()
        {
            return View();
        }

        public ActionResult ViewPronunciationDev()
        {
            return View();
        }

    }
}