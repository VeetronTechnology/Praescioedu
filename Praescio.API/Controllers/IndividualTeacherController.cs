using Praescio.API.CustomFilter;
using Praescio.BusinessEntities.Common;
using Praescio.Domain.DAL;
using Praescio.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Praescio.API.Controllers
{
    [AuthorizeInitializeAttribute]
    [RoutePrefix("api/IndividualTeacher")]
    public class IndividualTeacherController : BaseApiController
    {

        [HttpPost]
        [Route("AddStudentRegisterPermissionForTeacher")]
        public HttpResponseMessage AddStudentRegisterPermissionForTeacher(UserList Teacher)
        {
            using (PraescioContext db = new PraescioContext())
            {
                foreach (var account in Teacher.AccountDetail)
                {
                    var user = db.Account.FirstOrDefault(x => x.AccountId == account.AccountId);
                    user.StudentRegisterAllowed = account.StudentRegisterAllowed;
                    db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, "successfully saved");


        }

        [HttpGet]
        [Route("GetStudentList")]
        public HttpResponseMessage GetStudentList(int standardid, int mediumid, int pageNo = 1, int itemPerPage = 10, string searchText = "")
        {
            PraescioContext db = new PraescioContext();
            var studentList = (from a in db.Account
                               where ((string.IsNullOrEmpty(searchText) && a.FirstName == a.FirstName) ||
                                         (searchText != "" && a.FirstName.Contains(searchText)) || (searchText != "" && a.LastName.Contains(searchText)) || (searchText != "" && a.Email.Contains(searchText))
                                         || (searchText != "" && a.MobileNo.Contains(searchText))) &&
                              a.AccountTypeId== (int)AccountType.IndividualStudent && a.StudentStandardId == standardid && a.MediumId == mediumid && a.IsActive
                                   select a);
            var accountList = studentList.OrderBy(x => x.AccountId).Skip((pageNo - 1) * itemPerPage).Take(itemPerPage).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, new { studentDetail = accountList, totalRecord = studentList.Count() });

        }

    }
}
