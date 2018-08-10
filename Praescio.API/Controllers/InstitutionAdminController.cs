using Newtonsoft.Json;
using Praescio.API.CustomFilter;
using Praescio.BusinessEntities.Common;
using Praescio.Domain.DAL;
using Praescio.Domain.Entities;
using Praescio.Domain.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Praescio.API.Controllers
{
    [AuthorizeInitializeAttribute]
    [RoutePrefix("api/InstitutionAdmin")]
    public class InstitutionAdminController : BaseApiController
    {
        [HttpGet]
        [Route("GetStandardUserCount")]
        public HttpResponseMessage GetStandardUserCount()
        {
            StudentStandardTotal studentCountByStandard = new StudentStandardTotal();
            using (PraescioContext db = new PraescioContext())
            {
                studentCountByStandard = db.Database.SqlQuery<StudentStandardTotal>("exec procGetTotalStudentByStandard @InstitutionAccountId", new SqlParameter("@InstitutionAccountId", LoggedInAccount.InstitutionAccountId)).FirstOrDefault();
            }
            return Request.CreateResponse(HttpStatusCode.OK, studentCountByStandard);


        }

        [HttpGet]
        [Route("SchoolProfile")]
        public HttpResponseMessage SchoolProfile(int institutionId = 0)
        {
            PraescioContext db = new PraescioContext();
            institutionId = (institutionId == 0 ? Convert.ToInt16(LoggedInAccount.InstitutionAccountId) : institutionId);

            var institution = db.OrganizationAccount.FirstOrDefault(x => x.InstitutionAccountId == institutionId);
          

            InstitutionPackageContent package = new InstitutionPackageContent();
            package.RegisteredStudent = db.Account.Where(x => x.InstitutionAccountId == institutionId && x.AccountTypeId == (int)BusinessEntities.Common.AccountType.Student).Count();
            package.RegisteredTeacher = db.Account.Where(x => x.InstitutionAccountId == institutionId && x.AccountTypeId == (int)BusinessEntities.Common.AccountType.Teacher).Count();
            package.PendingStudent = Math.Max(0, Convert.ToInt32(institution.NoOfStudent) - package.RegisteredStudent);
            package.PendingTeacher = Math.Max(0, Convert.ToInt32(institution.NoOfTeacher) - package.RegisteredTeacher);
            package.PrincipalDetail = db.PrincipalDetail.FirstOrDefault(x => x.InstitutionAccountId == institutionId);


            return Request.CreateResponse(HttpStatusCode.OK, new { Institution = institution, Package = package });         

        }

        [HttpGet]
        [Route("RequestPackage")]
        public HttpResponseMessage RequestPackage(PackageRequestViewModel packageRequest)
        {
            using (PraescioContext db = new PraescioContext())
            {
                LogActivities(new Mst_Activities
                {
                    ActivityName = ActivityType.InstitutionPackageRequest.ToString(),
                    ContactName = packageRequest.Name,
                    CreatedBy = LoggedInAccount.AccountId,
                    Email = packageRequest.Email,
                    Phone = packageRequest.MobileNo,
                    Query = packageRequest.Query,
                    URL = "/InstitutionAdmin/SchoolProfile?institutionAccountId=" + LoggedInAccount.InstitutionAccountId.ToString(),
                    CreatedOn = DateTime.Now
                });
            }
            return Request.CreateResponse(HttpStatusCode.OK, "success");

        }

    }
}
