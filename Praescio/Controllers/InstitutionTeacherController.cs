using Praescio.CustomFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Praescio.Controllers
{
    [SessionExpired]
    [ChangePasswordFirstLogin]
    public class InstitutionTeacherController : Controller
    {

        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult RegisterStudent()
        {
            return View();
        }
        public ActionResult TeacherProfile(int? teacherAccountId, bool isindividual=false)
        {
            ViewBag.TeacherAccountId = teacherAccountId;
            ViewBag.Isindividual = isindividual;
            return View();
        }
        public ActionResult SchoolProfile()
        {
            return View();
        }
        public ActionResult Students()
        {
            return View();
        }
        public ActionResult StudentsDetails()
        {
            return View();
        }
        public ActionResult CheckAssignment(int assignmentType)
        {
            ViewBag.AssignmentType = assignmentType;
            return View();
        }
        public ActionResult TestHistory(int assignmentId, int userId,int standardId = 0, int subjectId = 0)
        {
            ViewBag.StandardId = standardId;
            ViewBag.SubjectId = subjectId;
            ViewBag.AssignmentId = assignmentId;
            ViewBag.UserId = userId;
            return View();
        }

        public ActionResult CheckStudentLesson(int assignmentId, int typeOfQuestion, int userId)
        {
            ViewBag.AssignmentId = assignmentId;
            ViewBag.TypeOfQuestion = typeOfQuestion;
            ViewBag.UserId = userId;
            return View();
        }
        public ActionResult SchoolNotice()
        {
            return View();
        }

        public ActionResult CheckMeaningoflesson()
        {
            return View();
        }
        public ActionResult CheckSynonyms()
        {
            return View();
        }
        public ActionResult CheckAntonyms()
        {

            return View();
        }

        public ActionResult CheckWriteReason()
        {
            return View();
        }
        public ActionResult CheckFillInTheBlanks(int assignmentId, int userId)
        {
            ViewBag.AssignmentId = assignmentId;
            ViewBag.UserId = userId;
            return View();
        }
        public ActionResult CheckMatchTheFollowing(int assignmentId, int userId)
        {
            ViewBag.AssignmentId = assignmentId;
            ViewBag.UserId = userId;
            return View();
        }
        public ActionResult CheckMCQ(int assignmentId, int userId)
        {
            ViewBag.AssignmentId = assignmentId;
            ViewBag.UserId = userId;
            return View();
        }
        public ActionResult CheckTrueFalseQuestions(int assignmentId, int userId)
        {
            ViewBag.AssignmentId = assignmentId;
            ViewBag.UserId = userId;
            return View();
        }

        public ActionResult CheckWriteAnswerInOneSentence()
        {
            return View();
        }
        public ActionResult CheckDescribeBriefly()
        {
            return View();
        }
        public ActionResult CheckDifferentiateBetween()
        {
            return View();
        }
        public ActionResult CheckExercise()
        {
            return View();
        }
        public ActionResult CheckWriteShortNote()
        {
            return View();
        }
        public ActionResult CheckTopicConcept()
        {
            return View();
        }





    }
}