using Praescio.CustomFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Praescio.Controllers
{
    [SessionExpired]
    public class IndividualStudentController : Controller
    {
        // GET: IndividualStudent
        public ActionResult Dashboard()
        {
            return View();
        }
    }
}