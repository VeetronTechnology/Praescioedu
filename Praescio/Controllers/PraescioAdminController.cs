using Newtonsoft.Json;
using Praescio.CustomFilter;
using Praescio.Domain.AppCode;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Praescio.Controllers
{
    public class File
    {
        public string Name { get; set; }
        public HttpPostedFile file { get; set; }
    }

    [SessionExpired]
    [ChangePasswordFirstLogin]
    public class PraescioAdminController : BaseController
    {
        // GET: Registration
        public ActionResult RegisterSchool(int? institutionId=0)
        {
            ViewBag.InstitutionId = institutionId;
            return View();
        }

        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult SchoolInfo()
        {
            return View();
        }

        public ActionResult SchoolList()
        {
            return View();
        }

        public ActionResult SchoolNotice()
        {
            return View();
        }

        public ActionResult RegisterIndividualTeacher(int institutionId = 0, string version = "Trial", int accountid = 0)
        {
            ViewBag.InstitutionId = institutionId;
            ViewBag.AccountId = accountid;
            ViewBag.Version = version;
            return View();
        }



        public ActionResult StudentsList(string version)
        {
            ViewBag.Version = version;

            if (version == VersionType.Paid)
            {
                return View("StudentsListFullVersion");
            }
            else
            {
                return View("StudentsListTrailVersion");
            }
        }

        public ActionResult RegisterIndividualStudent(int institutionId = 0, string version = "Trial", int accountid = 0)
        {
            ViewBag.InstitutionId = institutionId;
            ViewBag.AccountId = accountid;
            ViewBag.Version = version;
            return View();
        }

        public ActionResult SchoolBoard()
        {
            return View();
        }

        public ActionResult SchoolDetails(int institutionId)
        {
            ViewBag.InstitutionId = institutionId;
            return View();
        }

        public ActionResult AddLesson()
        {
            return View();
        }

        public ActionResult ViewLesson()
        {
            return View();
        }

        public ActionResult LoadXMLQuestion()
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

        public ActionResult HandwritingDevelopment()
        {
            return View();
        }
        public ActionResult PronunciationDevelopment()
        {
            return View();
        }
        public ActionResult TeachersAssignment()
        {
            return View();
        }

        public ActionResult SchoolInformation()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SaveFile(List<File> fileUpload)
        {
            string path = this.SavePDFFile();

            var result = new { Result = "Successed", filelocation = path };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public FileResult Download(string filepath)
        {
            var file = this.DownloadFile(filepath);
            return file;
        }
    }
}