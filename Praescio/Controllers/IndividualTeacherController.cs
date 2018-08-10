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
    public class IndividualTeacherController : Controller
    {
        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult AddAssignment()
        {
            return View();
        }

        public ActionResult AddStudent()
        {
            return View();
        }

        public ActionResult RegisterStudentHistory()
        {
            return View();
        }

        public ActionResult UpgradePackage()
        {
            return View();
        }
    }
}