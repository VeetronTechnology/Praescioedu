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
    public class InstitutionAdminController : Controller
    {
        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult RegisterTeacher(int accountid=0)
        {
            ViewBag.Accountid = accountid;
            return View();
        }
        public ActionResult ViewTeacher()
        {
            return View();
        }
        public ActionResult ViewStudent()
        {
            return View();
        }

        public ActionResult StandaredDetails()
        {
            return View();
        }

        public ActionResult Standards()
        {
            return View();
        }

        public ActionResult RegisterStudent(int accountid = 0)
        {
            ViewBag.Accountid = accountid;
            return View();
        }

        public ActionResult AddAssignment()
        {
            return View();
        }

        public ActionResult SchoolProfile()
        {
            return View();
        }

        public ActionResult AddKnowledgeBank()
        {
            return View();
        }
        public ActionResult ViewKnowledgeBank()
        {
            return View();
        }

        public ActionResult KnowledgeBankHistory()
        {
            return View();
        }

        public ActionResult ViewEDUPackage()
        {
            return View();
        }

        public ActionResult PurchaseEDUPackage()
        {
            return View();
        }
    }
}