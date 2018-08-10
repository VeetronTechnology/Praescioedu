using Praescio.CustomFilter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Praescio.Controllers
{


    public class MCQ
    {
        public string Question { get; set; }
        public string Text1 { get; set; }
        public object Text2 { get; set; }
        public object Text3 { get; set; }
        public object Text4 { get; set; }
        public object img1 { get; set; }
        public object img2 { get; set; }
        public object img3 { get; set; }
        public object img4 { get; set; }
        public string Keyname { get; set; }
    }

    [SessionExpired]
    [ChangePasswordFirstLogin]
    [RoutePrefix("Praescio")]
    public class PraescioCommonController : BaseController
    {

        [Route("TeacherList")]
        public ActionResult TeacherList(bool isIndividual = false, string version = "", int institutionId = 0)
        {
            ViewBag.IsIndividual = isIndividual;
            ViewBag.Version = version;
            ViewBag.InstitutionId = institutionId;

            return View();
        }

        [Route("StudentList")]
        public ActionResult StudentList(bool isIndividual = false, string version = "", int institutionId = 0, int standardId = 0)
        {
            ViewBag.IsIndividual = isIndividual;
            ViewBag.Version = version;
            ViewBag.InstitutionId = institutionId;
            ViewBag.StandardId = standardId;

            return View();
        }



        [Route("AddAssignment")]
        public ActionResult AddAssignment(int assignmentType,int assignmentid=0)
        {
            ViewBag.AssignmentType = assignmentType;
            ViewBag.Assignmentid = assignmentid;
            return View();
        }

        [Route("UploadAssignmentQuestion")]
        [Route("UploadPExtraQuestion")]
        public ActionResult UploadAssignmentQuestion(int assignmentType, int assignmentid=0)
        {
            ViewBag.AssignmentType = assignmentType;
            ViewBag.Assignmentid = assignmentid;
            return View();
        }

        [Route("ViewAssignment")]
        public ActionResult ViewAssignment(int assignmentType)
        {
            ViewBag.AssignmentType = assignmentType;
            return View();
        }

        [Route("ViewAssignmentHKP")]
        public ActionResult ViewAssignmentHKP(int assignmentType)
        {
            ViewBag.AssignmentType = assignmentType;
            return View();
        }

        [Route("ViewAssignmentHKPStudent")]
        public ActionResult ViewAssignmentHKPStudent(int assignmentType)
        {
            ViewBag.AssignmentType = assignmentType;
            return View();
        }

        [Route("AssignmentResult")]
        public ActionResult AssignmentResult(int assignmentType)
        {
            ViewBag.AssignmentType = assignmentType;
            return View();
        }

        

        [Route("AssignmentQuestion")]
        public ActionResult ViewAssignmentQuestion(int assignmentId)
        {
            ViewBag.assignmentid = assignmentId;
            return View();
        }

        [Route("ViewQuestionList")]
        public ActionResult ViewQuestionList(int assignmentId, int typeOfQuestion)
        {
            ViewBag.AssignmentId = assignmentId;
            ViewBag.TypeOfQuestion = typeOfQuestion;
            return View();
        }

        [Route("AddMCQ")]
        public ActionResult AddMCQ()
        {
            return View();
        }

        [Route("AddVideo")]
        public ActionResult AddVideo()
        {
            return View();
        }
        [Route("ViewVideo")]
        public ActionResult ViewVideo(int assignmentId)
        {
            ViewBag.AssignmentId = assignmentId;
            return View();
        }

        [Route("AddSynonyms")]
        public ActionResult AddSynonyms()
        {
            return View();
        }

        [Route("AddAntonym")]
        public ActionResult AddAntonym()
        {
            return View();
        }

        [Route("ViewSynonyms")]
        [Route("ViewAntonym")]
        public ActionResult ViewQuestionContent(int categoryTypeId)
        {
            ViewBag.CategoryTypeId = categoryTypeId;
            return View();
        }
        [Route("AddKnowledgeBank")]
        public ActionResult AddKnowledgeBank(int? knowledgeBankId)
        {
            ViewBag.KnowledgeBankId = knowledgeBankId;
            return View();
        }

        [Route("ViewKnowledgeBank")]
        public ActionResult ViewKnowledgeBank()
        {
            return View();
        }
        [Route("KnowledgeBankHistory")]
        public ActionResult KnowledgeBankHistory()
        {
            return View();
        }


        [HttpPost]
        public JsonResult SaveFile(List<File> fileUpload)
        {
            string path = this.SavePDFFile();

            var result = new { Result = "Success", filelocation = path };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public FileResult Download(string filePath)
        {

            var file = this.DownloadFile(filePath);
            return file;
        }

        [Route("AddHandwritingAssignment")]
        public ActionResult AddHandwritingAssignment(int assignmentType, int assignmentid = 0)
        {
            ViewBag.AssignmentType = assignmentType;
            ViewBag.Assignmentid = assignmentid;
            return View();
        }

        [Route("ViewHandwritingAssigment")]
        public ActionResult ViewHandwritingAssigment()
        {
            return View();
        }
        [Route("AddPronunciationAssignment")]
        public ActionResult AddPronunciationAssignment()
        {
            return View();
        }

        [Route("ViewPronunciationAssignment")]
        public ActionResult ViewPronunciationAssignment()
        {
            return View();
        }

        [Route("Help")]
        public ActionResult Help()
        {
            return View();
        }

        [Route("TestHKP")]
        public ActionResult TestHKP(int assignmentId, int userId, int standardId = 0, int subjectId = 0)
        {
            ViewBag.StandardId = standardId;
            ViewBag.SubjectId = subjectId;
            ViewBag.AssignmentId = assignmentId;
            ViewBag.UserId = userId;
            return View();
        }

        [Route("CreativityDetails")]
        public ActionResult CreativityDetails(int CreativityId)
        {
            ViewBag.CreativityId = CreativityId;
            return View();
        }
    }
}