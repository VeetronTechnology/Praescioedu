using Praescio.API.CustomFilter;
using Praescio.BusinessEntities.Common;
using Praescio.Domain.AppCode;
using Praescio.Domain.DAL;
using Praescio.Domain.Entities;
using Praescio.Domain.Model;
using Praescio.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Praescio.BusinessEntities;

namespace Praescio.API.Controllers
{
    [RoutePrefix("api/PraescioWeb")]
    public class PraescioWebController : BaseApiController
    {
        [HttpGet]
        [Route("GetBoardList")]
        public HttpResponseMessage GetBoardList()
        {
            List<Mst_Board> boardList = new List<Mst_Board>();
            using (PraescioContext db = new PraescioContext())
            {

                boardList = (from s in db.Board
                             where s.IsActive == true
                             select s).ToList();
            }
            return Request.CreateResponse(HttpStatusCode.OK, boardList);
        }
        
        [HttpPost]
        [Route("RegisterTeacher")]
        [Route("RegisterStudent")]
        public HttpResponseMessage RegisterTeacher(AccountDetailViewModel accountDetail)
        {

            PraescioContext db = new PraescioContext();
            Random rand = new Random();

            int RegisteredAccountId = 0;

            if (accountDetail.account.MobileNo != null && accountDetail.account.MobileNo == accountDetail.account.ParentNo)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { sucess = false, message = "Parent's mobile number and Phone number cannot be same!" });
            }
            if (accountDetail.account.Email != null && db.Account.Count(x => x.Email.ToLower() == accountDetail.account.Email.ToLower()) > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { sucess = false, message = "Email already exists!!!" });
            }
            else if ((accountDetail.accounttypeid == (int)Praescio.BusinessEntities.Common.AccountType.IndividualStudent || accountDetail.accounttypeid == (int)Praescio.BusinessEntities.Common.AccountType.Student)
                && db.Account.Count(x => x.ParentEmail.ToLower() == accountDetail.account.ParentEmail.ToLower()) > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { sucess = false, message = "Parent Email already exists!!!" });
            }
            
            /* check allowed registrations -start */
            var institution = db.OrganizationAccount.FirstOrDefault(x => x.InstitutionAccountId == accountDetail.account.InstitutionAccountId);

            InstitutionPackageContent package = new InstitutionPackageContent();
            if (accountDetail.accounttypeid == (int)Praescio.BusinessEntities.Common.AccountType.IndividualStudent
                || accountDetail.accounttypeid == (int)Praescio.BusinessEntities.Common.AccountType.Student)
            {
                package.RegisteredStudent = db.Account.Where(x => x.InstitutionAccountId == accountDetail.account.InstitutionAccountId && x.AccountTypeId == (int)BusinessEntities.Common.AccountType.Student).Count();
                package.PendingStudent = Math.Max(0, Convert.ToInt32(institution.NoOfStudent) - package.RegisteredStudent);
            }
            else
            {
                package.RegisteredTeacher = db.Account.Where(x => x.InstitutionAccountId == accountDetail.account.InstitutionAccountId && x.AccountTypeId == (int)BusinessEntities.Common.AccountType.Teacher).Count();
                package.PendingTeacher = Math.Max(0, Convert.ToInt32(institution.NoOfTeacher) - package.RegisteredTeacher);
            }
            if (accountDetail.account.IsIndividual == false && package.PendingStudent == 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { sucess = false, message = "The maximum registrations allowed for this Institute has been exceeded!!!" });
            }
            /* check allowed registrations -start */

            SET_UserName:
            accountDetail.account.UserName = accountDetail.account.FirstName.Trim() + rand.Next(100000).ToString();
            accountDetail.account.Password = BusinessEntities.AppCode.Common.RandomString(8);

            if (db.Account.Any(x => x.UserName.ToLower() == accountDetail.account.UserName))
            {
                goto SET_UserName;
            }

            accountDetail.account.CreatedOn = DateTime.Now;
            accountDetail.account.CreatedBy = 1; // LoggedInAccount.AccountId;
            accountDetail.account.StudentStandardId = accountDetail.account.StudentStandardId;// accountDetail.studentstandard;
            db.Account.Add(accountDetail.account);
            db.SaveChanges();

            RegisteredAccountId = db.Account.Where(m => m.Email == accountDetail.account.Email && m.UserName.ToLower() == accountDetail.account.UserName).FirstOrDefault().AccountId;

            List<Mst_Account> ParentAccounts = new List<Mst_Account>();

            if (accountDetail.teacherMappingStandard != null && 
                (accountDetail.accounttypeid == (int)Praescio.BusinessEntities.Common.AccountType.IndividualTeacher || accountDetail.accounttypeid == (int)Praescio.BusinessEntities.Common.AccountType.Teacher))
            {
                foreach (var teacherStandardList in accountDetail.teacherMappingStandard)
                {
                    foreach (var subject in teacherStandardList.SubjectId)
                    {
                        db.StandardMapping.Add(new TeacherStandardMapping { TeacherId = accountDetail.account.AccountId, StandardId = teacherStandardList.StandardId, SubjectId = subject });
                    }
                }
            }
            else if (//accountDetail.studentsubject != null && 
                (accountDetail.accounttypeid == (int)Praescio.BusinessEntities.Common.AccountType.IndividualStudent 
                || accountDetail.accounttypeid == (int)Praescio.BusinessEntities.Common.AccountType.Student))
            {
                if(accountDetail.studentsubject != null)
                {
                    foreach (var subject in accountDetail.studentsubject)
                    {
                        db.StandardMapping.Add(new TeacherStandardMapping { TeacherId = accountDetail.account.AccountId, StandardId = (int)accountDetail.account.StudentStandardId, SubjectId = subject });
                    }
                }

                /*for (int i = 0; i < 4; i++)
                {
                    Mst_StudentParentAccount Mst_StudentParentAccount = new Mst_StudentParentAccount
                    {
                        Username = "Parent" + accountDetail.account.AccountId.ToString() + "_" + rand.Next(100000).ToString(),
                        Password = BusinessEntities.AppCode.Common.RandomString(8),
                        AccountTypeId = (int)AccountType.StudentParent,
                        CreatedBy = LoggedInAccount.AccountId,
                        CreatedOn = DateTime.Now,
                        IsActive = false
                    };
                    db.StudentParentAccount.Add(Mst_StudentParentAccount);
                }*/

                Mst_Account Mst_AccountParent = new Mst_Account
                {
                    UserName = "Parent" + accountDetail.account.AccountId.ToString() + "_" + rand.Next(100000).ToString(),
                    Password = BusinessEntities.AppCode.Common.RandomString(8),
                    AccountTypeId = (int)AccountType.StudentParent,
                    CreatedBy = 1, // LoggedInAccount.AccountId,
                    CreatedOn = DateTime.Now,
                    IsActive = true,
                    Email = accountDetail.account.ParentEmail,
                    MobileNo = accountDetail.account.ParentNo,
                    FirstName = accountDetail.account.FirstName,
                    LastName = accountDetail.account.LastName,
                    BoardId = accountDetail.account.BoardId,
                    MediumId = accountDetail.account.MediumId,
                    //Standard = accountDetail.account.Standard,
                    //StandardSubject = accountDetail.account.StandardSubject
                    InstitutionAccountId = accountDetail.account.InstitutionAccountId
                };
                db.Account.Add(Mst_AccountParent);
                ParentAccounts.Add(Mst_AccountParent);
            }

            db.SaveChanges();

            MailType type = MailType.NoContent;

            if (accountDetail.accounttypeid == (int)Praescio.BusinessEntities.Common.AccountType.IndividualStudent)
            {
                type = MailType.IndividualStudentRegister;
                LogActivities(new Mst_Activities
                {
                    ActivityName = "Individual Student Registration",
                    CreatedBy = 1, // LoggedInAccount.AccountId,
                    Query = "New Individual Student with Name : " + accountDetail.account.FirstName + " has been created successfully @ " + DateTime.Now.ToString("dd'/'MM'/'yyyy HH:mm:ss") + " !!!",
                    URL = "/InstitutionStudent/StudentProfile?StudentAccountId=" + accountDetail.account.AccountId.ToString() + "&isIndividual=true",
                    ContactName = "Admin", // LoggedInAccount.FirstName,
                    CreatedOn = DateTime.Now
                });


            }
            else if (accountDetail.accounttypeid == (int)Praescio.BusinessEntities.Common.AccountType.IndividualTeacher)
            {
                type = MailType.IndividualStudentRegister;

                LogActivities(new Mst_Activities
                {
                    ActivityName = "Individual Teacher Registration",
                    CreatedBy = 1, // LoggedInAccount.AccountId,
                    Query = "New Individual Teacher with Name : " + accountDetail.account.FirstName + " has been created successfully @ " + DateTime.Now.ToString("dd'/'MM'/'yyyy HH:mm:ss") + " !!!",
                    URL = "/InstitutionTeacher/TeacherProfile?teacherAccountId=" + accountDetail.account.AccountId.ToString() + "&isIndividual=true",
                    ContactName = "Admin", // LoggedInAccount.FirstName,
                    CreatedOn = DateTime.Now
                });
            }
            else if (accountDetail.accounttypeid == (int)Praescio.BusinessEntities.Common.AccountType.Teacher)
            {
                type = MailType.InstitutionTeacher;

                LogActivities(new Mst_Activities
                {
                    ActivityName = "Teacher Registration",
                    CreatedBy = 1, // LoggedInAccount.AccountId,
                    Query = "New Teacher with Name : " + accountDetail.account.FirstName + " has been created successfully @ " + DateTime.Now.ToString("dd'/'MM'/'yyyy HH:mm:ss") + " !!!",
                    URL = "/InstitutionTeacher/TeacherProfile?teacherAccountId=" + accountDetail.account.AccountId.ToString() + "&isIndividual=true",
                    ContactName = "Admin", // LoggedInAccount.FirstName,
                    CreatedOn = DateTime.Now
                });
            }
            else if (accountDetail.accounttypeid == (int)Praescio.BusinessEntities.Common.AccountType.Student)
            {
                type = MailType.InstitutionStudent;

                LogActivities(new Mst_Activities
                {
                    ActivityName = "Student Registration",
                    CreatedBy = 1, //LoggedInAccount.AccountId,
                    Query = "New Student with Name : " + accountDetail.account.FirstName + " has been created successfully @ " + DateTime.Now.ToString("dd'/'MM'/'yyyy HH:mm:ss") + " !!!",
                    URL = "/InstitutionStudent/StudentProfile?StudentAccountId=" + accountDetail.account.AccountId.ToString() + "&isIndividual=false",
                    ContactName = "Admin", // LoggedInAccount.FirstName,
                    CreatedOn = DateTime.Now
                });
            }

            db.SaveChanges();

            var organizationAccount = db.OrganizationAccount.FirstOrDefault(x => x.InstitutionAccountId == accountDetail.account.InstitutionAccountId);
            string emailsubject = string.Empty;
            Dictionary<string, string> account = new Dictionary<string, string>();
            account.Add("username", organizationAccount.DomainKey + "/" + accountDetail.account.UserName);
            account.Add("password", accountDetail.account.Password);
            account.Add("name", accountDetail.account.FirstName + ' ' + accountDetail.account.LastName);
            string emailcontent = Email.GetEmailContent(account, type, ref emailsubject);

            Email.SendEmail(emailcontent, accountDetail.account.Email, "", emailsubject);
            string smsContent = "Hello " + account["name"] + ", Thanks for registering with Praescioedu. UserName: " + account["username"] + " and Password: " + account["password"];
            SMS.SendAlertSMS(accountDetail.account.MobileNo, smsContent);

            if (accountDetail.accounttypeid == (int)Praescio.BusinessEntities.Common.AccountType.IndividualStudent || accountDetail.accounttypeid == (int)Praescio.BusinessEntities.Common.AccountType.Student)
            {
                foreach (var parent in ParentAccounts)
                {
                    string emailsubject2 = string.Empty;
                    Dictionary<string, string> account2 = new Dictionary<string, string>();
                    account2.Add("username", organizationAccount.DomainKey + "/" + parent.UserName);
                    account2.Add("password", parent.Password);
                    account2.Add("name", parent.FirstName + ' ' + parent.LastName);
                    string emailcontent2 = Email.GetEmailContent(account2, type, ref emailsubject);

                    Email.SendEmail(emailcontent2, parent.Email, "", emailsubject);
                    smsContent = "Hello " + account2["name"] + ", Thanks for registering with Praescioedu. UserName: " + account2["username"] + " and Password: " + account2["password"];
                    SMS.SendAlertSMS(parent.MobileNo, emailcontent2);
                }
            }

            return Request.CreateResponse(HttpStatusCode.OK, new {sucess=true,message="successfully created!!!", RegisteredAccountId = RegisteredAccountId });

        }
        
        [HttpGet]
        [Route("GetMedium")]
        public HttpResponseMessage GetMedium()
        {
            try
            {
                List<Medium> medium = new List<Medium>();
                using (PraescioContext db = new PraescioContext())
                {
                    medium = db.Medium.Where(x => x.IsActive).ToList();
                }
                return Request.CreateResponse(HttpStatusCode.OK, medium);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetState")]
        public HttpResponseMessage GetState()
        {

            List<Mst_State> stateList = new List<Mst_State>();
            using (PraescioContext db = new PraescioContext())
            {

                stateList = (from s in db.State
                             where s.IsActive == true
                             select s).OrderBy(m => m.StateName).ToList();
            }
            return Request.CreateResponse(HttpStatusCode.OK, stateList);
        }
        
        [HttpGet]
        [Route("GetCityList")]
        public HttpResponseMessage GetCityList(int StateId)
        {

            List<Mst_City> stateList = new List<Mst_City>();
            using (PraescioContext db = new PraescioContext())
            {

                stateList = (from s in db.City
                             where s.state_id == StateId
                             select s).OrderBy(m => m.city_name).ToList();
            }
            return Request.CreateResponse(HttpStatusCode.OK, stateList);
        }

        [HttpGet]
        [Route("GetPackage")]
        public HttpResponseMessage GetPackage(int? userPackageRole)
        {
            PraescioContext db = new PraescioContext();
            var packageList = (from p in db.Package
                               where p.PackageRoleId == userPackageRole
                               select new { id = p.PackageId, text = p.PackageName, amount = p.PackageAmount, isActive = p.IsActive, intervalType = p.IntervalType, interval = p.Interval,
                                   standardId = p.StandardId, mediumId = p.MediumId, boardId = p.BoardId
                               }).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, packageList);

        }


        [HttpGet]
        [Route("GetSubjectSingleMedium")]
        public HttpResponseMessage GetSubjectSingleMedium(int? standardid, int mediumid, int boardid)
        {
            PraescioContext db = new PraescioContext();
            var subjectlist = db.Subject.Where(x => x.StandardId == standardid && x.MediumId == mediumid && x.BoardId == boardid).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, subjectlist);
        }
    }
}
