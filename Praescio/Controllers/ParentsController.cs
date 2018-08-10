using Praescio.CustomFilter;
using Praescio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Praescio.Controllers
{
    [ChangePasswordFirstLogin]
    public class ParentsController : Controller
    {
        // GET: Parents
        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult StudentPackageDetails()
        {
            return View();
        }
        public ActionResult CommentHistory()
        {
            return View();
        }

        public ActionResult HistoryDetails()
        {
            return View();
        }
        public ActionResult StudentsCreativity()
        {
            return View();
        }

        public ActionResult CreativityUploads()
        {
            return View();
        }
        public ActionResult GiveComments()
        {
            return View();
        }

        public ActionResult SchoolInformation()
        {
            ViewBag.InstitutionId = Common.ACCOUNT.InstitutionAccountId;
            return View();
        }

        public ActionResult GiveComments(int CreativityId)
        {
            ViewBag.CreativityId = CreativityId;
            return View();
        }

    }
}