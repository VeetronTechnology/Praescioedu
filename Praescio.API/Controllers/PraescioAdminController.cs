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
    [AuthorizeInitializeAttribute]
    [RoutePrefix("api/PraescioAdmin")]
    public class PraescioAdminController : BaseApiController
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



        [HttpGet]
        [Route("GetSchoolDetail")]
        public HttpResponseMessage GetSchoolDetail(int institutionId)
        {
            InstitutionViewModel institionDetail = new InstitutionViewModel();

            PraescioContext db = new PraescioContext();
            institionDetail.Institution = db.OrganizationAccount.FirstOrDefault(x => x.InstitutionAccountId == institutionId);
            institionDetail.PrincipalDetail = db.PrincipalDetail.FirstOrDefault(x => x.InstitutionAccountId == institutionId);

            institionDetail.RegisteredStudent = db.Account.Where(x => x.InstitutionAccountId == institutionId && x.AccountTypeId == (int)BusinessEntities.Common.AccountType.Student).Count();
            institionDetail.RegisteredTeacher = db.Account.Where(x => x.InstitutionAccountId == institutionId && x.AccountTypeId == (int)BusinessEntities.Common.AccountType.Teacher).Count();
            institionDetail.PendingStudent = Convert.ToInt16(institionDetail.Institution.NoOfStudent) - institionDetail.RegisteredStudent;
            institionDetail.PendingTeacher = Convert.ToInt16(institionDetail.Institution.NoOfTeacher) - institionDetail.RegisteredTeacher;

            return Request.CreateResponse(HttpStatusCode.OK, institionDetail);


        }

        [HttpPost]
        [Route("RegisterSchool")]
        public HttpResponseMessage RegisterSchool(InstitutionViewModel institutionViewModel)
        {

            using (PraescioContext db = new PraescioContext())
            {
                institutionViewModel.Institution.CreatedOn = DateTime.Now;
                institutionViewModel.Institution.PackageId = 1;

                if (db.OrganizationAccount.Any(x => x.DomainKey.ToLower() == institutionViewModel.Institution.DomainKey.ToLower()))
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "domain name is already registered");
                }

                db.OrganizationAccount.Add(institutionViewModel.Institution);
                db.SaveChanges();

                var accountcode = CommonCode.UserAccountCode();
                var account = new Mst_Account() { FirstName = institutionViewModel.Institution.CustomerName, MobileNo = "", UserName = accountcode, Password = "password", ContactName = "", Email = institutionViewModel.Institution.Email, City = institutionViewModel.Institution.City, InstitutionName = institutionViewModel.Institution.InstitutionName, AccountTypeId = 2, InstitutionAccountId = institutionViewModel.Institution.InstitutionAccountId, CreatedOn = DateTime.Now, IsActive = true };
                db.Account.Add(account);
                db.SaveChanges();

                institutionViewModel.PrincipalDetail.InstitutionAccountId = institutionViewModel.Institution.InstitutionAccountId;
                institutionViewModel.PrincipalDetail.CreatedOn = DateTime.Now;
                db.PrincipalDetail.Add(institutionViewModel.PrincipalDetail);

                LogActivities(new Mst_Activities
                {
                    // ActivityName = ActivityType.InstitutionRegisteration.ToString(),
                    ActivityName = "Institution Registeration",
                    CreatedBy = LoggedInAccount.AccountId,
                    Query = "New School with Name : " + institutionViewModel.Institution.InstitutionName + " & Domain Key : " + institutionViewModel.Institution.DomainKey + " has been created successfully @ " + DateTime.Now.ToString("dd'/'MM'/'yyyy HH:mm:ss") + " !!!",
                    URL = "/InstitutionAdmin/SchoolProfile?institutionid=" + institutionViewModel.Institution.InstitutionAccountId.ToString(),
                    ContactName = LoggedInAccount.FirstName,
                    CreatedOn = DateTime.Now
                });

                db.SaveChanges();

            }
            return Request.CreateResponse(HttpStatusCode.OK, new { sucess = true, message = "successfully created!!!" });
        }

        [HttpPost]
        [Route("UpdateSchool")]
        public HttpResponseMessage UpdateSchool(InstitutionViewModel institutionViewModel)
        {
            using (PraescioContext db = new PraescioContext())
            {
                var institution = db.OrganizationAccount.FirstOrDefault(x => x.InstitutionAccountId == institutionViewModel.Institution.InstitutionAccountId);
                var principaldetail = db.PrincipalDetail.FirstOrDefault(x => x.InstitutionAccountId == institutionViewModel.Institution.InstitutionAccountId);


                institution.RegistrationNo = institutionViewModel.Institution.RegistrationNo;
                institution.InstitutionName = institutionViewModel.Institution.InstitutionName;
                institution.PackageId = institutionViewModel.Institution.PackageId;
                institution.CustomerName = institutionViewModel.Institution.CustomerName;
                institution.MobileNo = institutionViewModel.Institution.MobileNo;
                institution.LandlineNo = institutionViewModel.Institution.LandlineNo;
                institution.Email = institutionViewModel.Institution.Email;
                institution.AddressProofSrc = !string.IsNullOrEmpty(institutionViewModel.Institution.AddressProofSrc) ? institutionViewModel.Institution.AddressProofSrc : institution.AddressProofSrc;
                institution.Address = institutionViewModel.Institution.Address;
                institution.StateId = institutionViewModel.Institution.StateId;
                institution.City = institutionViewModel.Institution.City;
                institution.Pincode = institutionViewModel.Institution.Pincode;
                institution.BoardId = institutionViewModel.Institution.BoardId;
                institution.MediumId = institutionViewModel.Institution.MediumId;
                institution.Description = institutionViewModel.Institution.Description;
                institution.ModifiedBy = LoggedInAccount.AccountId;
                institution.ModifiedOn = DateTime.Now;

                db.Entry(institution).State = EntityState.Modified;
                db.SaveChanges();



                principaldetail.Name = institutionViewModel.PrincipalDetail.Name;
                principaldetail.Designation = institutionViewModel.PrincipalDetail.Designation;
                principaldetail.Email = institutionViewModel.PrincipalDetail.Email;
                principaldetail.Gender = institutionViewModel.PrincipalDetail.Gender;
                principaldetail.MobileNo = institutionViewModel.PrincipalDetail.MobileNo;
                principaldetail.PersonPhotoSrc = !string.IsNullOrEmpty(institutionViewModel.PrincipalDetail.PersonPhotoSrc) ? institutionViewModel.PrincipalDetail.PersonPhotoSrc : principaldetail.PersonPhotoSrc;
                principaldetail.AuthorityLetterSrc = !string.IsNullOrEmpty(institutionViewModel.PrincipalDetail.AuthorityLetterSrc) ? institutionViewModel.PrincipalDetail.AuthorityLetterSrc : principaldetail.AuthorityLetterSrc;


                db.Entry(principaldetail).State = EntityState.Modified;
                db.SaveChanges();


                //LogActivities(new Mst_Activities
                //{
                //    ActivityName = ActivityType.InstitutionRegisteration.ToString(),
                //    CreatedBy = LoggedInAccount.AccountId,
                //    Query = "New School with Name : " + institutionViewModel.Institution.InstitutionName + " & Domain Key : " + institutionViewModel.Institution.DomainKey + " has been created successfully @ " + DateTime.Now.ToString("dd'/'MM'/'yyyy HH:mm:ss") + " !!!",
                //    URL = "/InstitutionAdmin/SchoolProfile?institutionid=" + institutionViewModel.Institution.InstitutionAccountId.ToString(),
                //    ContactName = LoggedInAccount.FirstName,
                //    CreatedOn = DateTime.Now
                //});

                db.SaveChanges();

            }
            return Request.CreateResponse(HttpStatusCode.OK, new { sucess = true, message = "successfully updated!!!" });
        }
        [HttpGet]
        [Route("GetRegisterationSchoolDetail")]
        public HttpResponseMessage GetRegisterationSchoolDetail(int? institutionId = 0)
        {

            PraescioContext db = new PraescioContext();
            InstitutionViewModel institutionViewModel = new InstitutionViewModel();

            institutionViewModel.Institution = db.OrganizationAccount.FirstOrDefault(x => x.InstitutionAccountId == (institutionId ?? LoggedInAccount.InstitutionAccountId));
            institutionViewModel.PrincipalDetail = db.PrincipalDetail.FirstOrDefault(x => x.InstitutionAccountId == (institutionId ?? LoggedInAccount.InstitutionAccountId));

            return Request.CreateResponse(HttpStatusCode.OK, institutionViewModel);

        }

        [HttpGet]
        [Route("GetInstitutionList")]
        public HttpResponseMessage GetInstitutionList(int pageNo = 1, int itemPerPage = 10, string searchText = "")
        {

            List<Mst_InstitutionAccount> schoolList = new List<Mst_InstitutionAccount>();
            PraescioContext db = new PraescioContext();

            var content = (from s in db.OrganizationAccount
                           where ((string.IsNullOrEmpty(searchText) && s.InstitutionName == s.InstitutionName) ||
                                        (searchText != "" && s.InstitutionName.Contains(searchText)))
                           select s);
            schoolList = content.OrderBy(x => x.BoardId).Skip((pageNo - 1) * itemPerPage).Take(itemPerPage).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, new { contentData = schoolList, totalRecord = content.Count() });


        }


        [HttpPut]
        [Route("InstitutionActivation")]
        public HttpResponseMessage InstitutionActivation(int institutionAccountId, int status)
        {

            PraescioContext db = new PraescioContext();
            var institution = db.OrganizationAccount.FirstOrDefault(x => x.InstitutionAccountId == institutionAccountId);
            institution.IsActive = Convert.ToBoolean(status);
            institution.ModifiedOn = DateTime.Now;
            db.Entry(institution).State = EntityState.Modified;
            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK, institution);


        }

        [HttpPost]
        [Route("RegisterTeacher")]
        [Route("RegisterStudent")]
        public HttpResponseMessage RegisterTeacher(AccountDetailViewModel accountDetail)
        {

            PraescioContext db = new PraescioContext();
            Random rand = new Random();

            if (db.Account.Count(x => x.Email.ToLower() == accountDetail.account.Email.ToLower()) > 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Email already exists!!!");
            }
            else if ((accountDetail.accounttypeid == (int)Praescio.BusinessEntities.Common.AccountType.IndividualStudent || accountDetail.accounttypeid == (int)Praescio.BusinessEntities.Common.AccountType.Student)
                && db.Account.Count(x => x.ParentEmail.ToLower() == accountDetail.account.ParentEmail.ToLower()) > 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Parent Email already exists!!!");
            }

        SET_UserName:
            accountDetail.account.UserName = accountDetail.account.FirstName.Trim() + rand.Next(100000).ToString();
            accountDetail.account.Password = BusinessEntities.AppCode.Common.RandomString(8);

            if (db.Account.Any(x => x.UserName.ToLower() == accountDetail.account.UserName))
            {
                goto SET_UserName;
            }

            accountDetail.account.CreatedOn = DateTime.Now;
            accountDetail.account.StudentStandardId = accountDetail.studentstandard;
            db.Account.Add(accountDetail.account);
            db.SaveChanges();

            if (accountDetail.accounttypeid == (int)Praescio.BusinessEntities.Common.AccountType.IndividualTeacher || accountDetail.accounttypeid == (int)Praescio.BusinessEntities.Common.AccountType.Teacher)
            {
                foreach (var teacherStandardList in accountDetail.teacherMappingStandard)
                {
                    foreach (var subject in teacherStandardList.SubjectId)
                    {
                        db.StandardMapping.Add(new TeacherStandardMapping { TeacherId = accountDetail.account.AccountId, StandardId = teacherStandardList.StandardId, SubjectId = subject });
                    }
                }
            }
            else if (accountDetail.accounttypeid == (int)Praescio.BusinessEntities.Common.AccountType.IndividualStudent || accountDetail.accounttypeid == (int)Praescio.BusinessEntities.Common.AccountType.Student)
            {
                foreach (var subject in accountDetail.studentsubject)
                {
                    db.StandardMapping.Add(new TeacherStandardMapping { TeacherId = accountDetail.account.AccountId, StandardId = (int)accountDetail.studentstandard, SubjectId = subject });
                }

                for (int i = 0; i < 4; i++)
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
                }
            }

            db.SaveChanges();

            MailType type = MailType.NoContent;

            if (accountDetail.accounttypeid == (int)Praescio.BusinessEntities.Common.AccountType.IndividualStudent)
            {
                type = MailType.IndividualStudentRegister;
                LogActivities(new Mst_Activities
                {
                    ActivityName = "Individual Student Registeration",
                    CreatedBy = LoggedInAccount.AccountId,
                    Query = "New Individual Student with Name : " + accountDetail.account.FirstName + " has been created successfully @ " + DateTime.Now.ToString("dd'/'MM'/'yyyy HH:mm:ss") + " !!!",
                    URL = "/InstitutionStudent/StudentProfile?StudentAccountId=" + accountDetail.account.AccountId.ToString() + "&isIndividual=true",
                    ContactName = LoggedInAccount.FirstName,
                    CreatedOn = DateTime.Now
                });


            }
            else if (accountDetail.accounttypeid == (int)Praescio.BusinessEntities.Common.AccountType.IndividualTeacher)
            {
                type = MailType.IndividualStudentRegister;

                LogActivities(new Mst_Activities
                {
                    ActivityName = "Individual Teacher Registeration",
                    CreatedBy = LoggedInAccount.AccountId,
                    Query = "New Individual Teacher with Name : " + accountDetail.account.FirstName + " has been created successfully @ " + DateTime.Now.ToString("dd'/'MM'/'yyyy HH:mm:ss") + " !!!",
                    URL = "/InstitutionTeacher/TeacherProfile?teacherAccountId=" + accountDetail.account.AccountId.ToString() + "&isIndividual=true",
                    ContactName = LoggedInAccount.FirstName,
                    CreatedOn = DateTime.Now
                });
            }
            else if (accountDetail.accounttypeid == (int)Praescio.BusinessEntities.Common.AccountType.Teacher)
            {
                type = MailType.InstitutionTeacher;

                LogActivities(new Mst_Activities
                {
                    ActivityName = "Teacher Registeration",
                    CreatedBy = LoggedInAccount.AccountId,
                    Query = "New Teacher with Name : " + accountDetail.account.FirstName + " has been created successfully @ " + DateTime.Now.ToString("dd'/'MM'/'yyyy HH:mm:ss") + " !!!",
                    URL = "/InstitutionTeacher/TeacherProfile?teacherAccountId=" + accountDetail.account.AccountId.ToString() + "&isIndividual=true",
                    ContactName = LoggedInAccount.FirstName,
                    CreatedOn = DateTime.Now
                });
            }
            else if (accountDetail.accounttypeid == (int)Praescio.BusinessEntities.Common.AccountType.Student)
            {
                type = MailType.InstitutionStudent;

                LogActivities(new Mst_Activities
                {
                    ActivityName = "Student Registeration",
                    CreatedBy = LoggedInAccount.AccountId,
                    Query = "New Student with Name : " + accountDetail.account.FirstName + " has been created successfully @ " + DateTime.Now.ToString("dd'/'MM'/'yyyy HH:mm:ss") + " !!!",
                    URL = "/InstitutionStudent/StudentProfile?StudentAccountId=" + accountDetail.account.AccountId.ToString() + "&isIndividual=false",
                    ContactName = LoggedInAccount.FirstName,
                    CreatedOn = DateTime.Now
                });
            }

            db.SaveChanges();

            var organizationAccount = db.OrganizationAccount.FirstOrDefault(x => x.InstitutionAccountId == accountDetail.account.InstitutionAccountId);
            string emailsubject = string.Empty;
            Dictionary<string, string> account = new Dictionary<string, string>();
            account.Add("username", organizationAccount.DomainKey + "/" + accountDetail.account.UserName);
            account.Add("password", accountDetail.account.Password);
            string emailcontent = Email.GetEmailContent(account, type, ref emailsubject);

            Email.SendEmail(emailcontent, accountDetail.account.Email, "", emailsubject);

            return Request.CreateResponse(HttpStatusCode.OK, new {sucess=true,message="successfully created!!!" });

        }

        [HttpPost]
        [Route("UpdateTeacher")]
        [Route("UpdateStudent")]
        public HttpResponseMessage UpdateStudent(AccountDetailViewModel accountDetail)
        {

            PraescioContext db = new PraescioContext();
            Random rand = new Random();

            var account = db.Account.FirstOrDefault(x => x.AccountId == accountDetail.account.AccountId);
            account.FirstName = accountDetail.account.FirstName;
            account.LastName = accountDetail.account.LastName;
            account.Gender = accountDetail.account.Gender;
            account.DateOfBirth = accountDetail.account.DateOfBirth;
            account.Email = accountDetail.account.Email;
            account.MobileNo = accountDetail.account.MobileNo;
            account.Address = accountDetail.account.Address;
            account.VersionType = accountDetail.account.VersionType;
            account.PackageId = accountDetail.account.PackageId;
            account.AmountPaid = accountDetail.account.AmountPaid;
            account.BoardId = accountDetail.account.BoardId;
            account.StudentStandardId = accountDetail.studentstandard;
            account.MediumId = accountDetail.account.MediumId;
            account.MotherName = accountDetail.account.MotherName;
            account.FatherName = accountDetail.account.FatherName;
            account.ParentEmail = accountDetail.account.ParentEmail;
            account.ParentNo = accountDetail.account.ParentNo;
            account.ActivateOn = accountDetail.account.ActivateOn;
            account.ExpiredOn = accountDetail.account.ExpiredOn;
            account.StateId = accountDetail.account.StateId;
            account.City = accountDetail.account.City;
            account.PinCode = accountDetail.account.PinCode;

            accountDetail.account.ModifiedOn = DateTime.Now;
            accountDetail.account.ModifiedBy = LoggedInAccount.AccountId;

            db.Entry(account).State = EntityState.Modified;
            db.SaveChanges();

            db.SaveChanges();

            if (false && accountDetail.accounttypeid == (int)Praescio.BusinessEntities.Common.AccountType.IndividualTeacher || accountDetail.accounttypeid == (int)Praescio.BusinessEntities.Common.AccountType.Teacher)
            {
                db.Database.ExecuteSqlCommand("delete from TeacherStandardMapping where TeacherId = {0}", accountDetail.account.AccountId);
                foreach (var teacherStandardList in accountDetail.teacherMappingStandard)
                {
                    foreach (var subject in teacherStandardList.SubjectId)
                    {
                        db.StandardMapping.Add(new TeacherStandardMapping { TeacherId = accountDetail.account.AccountId, StandardId = teacherStandardList.StandardId, SubjectId = subject });
                    }
                }
            }
            else if (accountDetail.accounttypeid == (int)Praescio.BusinessEntities.Common.AccountType.IndividualStudent || accountDetail.accounttypeid == (int)Praescio.BusinessEntities.Common.AccountType.Student)
            {
                db.Database.ExecuteSqlCommand("delete from TeacherStandardMapping where TeacherId = {0}", accountDetail.account.AccountId);
                foreach (var subject in accountDetail.studentsubject)
                {
                    db.StandardMapping.Add(new TeacherStandardMapping { TeacherId = accountDetail.account.AccountId, StandardId = (int)accountDetail.studentstandard, SubjectId = subject });
                }
            }

            db.SaveChanges();

            MailType type = MailType.NoContent;

            if (accountDetail.accounttypeid == (int)Praescio.BusinessEntities.Common.AccountType.IndividualStudent)
            {
                type = MailType.IndividualStudentRegister;
                LogActivities(new Mst_Activities
                {
                    // ActivityName = ActivityType.IndividualStudentRegisteration.ToString(),
                    ActivityName = "Individual Student Registeration",
                    CreatedBy = LoggedInAccount.AccountId,
                    Query = "New Individual Student with Name : " + accountDetail.account.FirstName + " has been created successfully @ " + DateTime.Now.ToString("dd'/'MM'/'yyyy HH:mm:ss") + " !!!",
                    URL = "/InstitutionStudent/StudentProfile?accountid=" + accountDetail.account.AccountId.ToString(),
                    ContactName = LoggedInAccount.FirstName,
                    CreatedOn = DateTime.Now
                });


            }
            else if (accountDetail.accounttypeid == (int)Praescio.BusinessEntities.Common.AccountType.IndividualTeacher)
            {
                type = MailType.IndividualStudentRegister;

                LogActivities(new Mst_Activities
                {
                    //ActivityName = ActivityType.IndividualStudentRegisteration.ToString(),
                    ActivityName = "Individual Teacher Registeration",
                    CreatedBy = LoggedInAccount.AccountId,
                    Query = "New Individual Teacher with Name : " + accountDetail.account.FirstName + " has been created successfully @ " + DateTime.Now.ToString("dd'/'MM'/'yyyy HH:mm:ss") + " !!!",
                    URL = "/InstitutionTeacher/TeacherProfile?accountid=" + accountDetail.account.AccountId.ToString(),
                    ContactName = LoggedInAccount.FirstName,
                    CreatedOn = DateTime.Now
                });
            }
            else if (accountDetail.accounttypeid == (int)Praescio.BusinessEntities.Common.AccountType.Teacher)
            {
                type = MailType.InstitutionTeacher;

                LogActivities(new Mst_Activities
                {
                    //ActivityName = ActivityType.InstitutionTeacherRegisteration.ToString(),
                    ActivityName = "Teacher Registeration",
                    CreatedBy = LoggedInAccount.AccountId,
                    Query = "New Teacher with Name : " + accountDetail.account.FirstName + " has been created successfully @ " + DateTime.Now.ToString("dd'/'MM'/'yyyy HH:mm:ss") + " !!!",
                    URL = "/InstitutionTeacher/TeacherProfile?accountid=" + accountDetail.account.AccountId.ToString(),
                    ContactName = LoggedInAccount.FirstName,
                    CreatedOn = DateTime.Now
                });
            }
            else if (accountDetail.accounttypeid == (int)Praescio.BusinessEntities.Common.AccountType.Student)
            {
                type = MailType.InstitutionStudent;

                LogActivities(new Mst_Activities
                {
                    // ActivityName = ActivityType.InstitutionStudentRegisteration.ToString(),
                    ActivityName = "Student Registeration",
                    CreatedBy = LoggedInAccount.AccountId,
                    Query = "New Student with Name : " + accountDetail.account.FirstName + " has been created successfully @ " + DateTime.Now.ToString("dd'/'MM'/'yyyy HH:mm:ss") + " !!!",
                    URL = "/InstitutionTeacher/TeacherProfile?accountid=" + accountDetail.account.AccountId.ToString(),
                    ContactName = LoggedInAccount.FirstName,
                    CreatedOn = DateTime.Now
                });
            }

            db.SaveChanges();

            //var organizationAccount = db.OrganizationAccount.FirstOrDefault(x => x.InstitutionAccountId == accountDetail.account.InstitutionAccountId);
            //string emailsubject = string.Empty;
            //Dictionary<string, string> account = new Dictionary<string, string>();
            //account.Add("username", organizationAccount.DomainKey + "/" + accountDetail.account.UserName);
            //account.Add("password", accountDetail.account.Password);
            //string emailcontent = Email.GetEmailContent(account, type, ref emailsubject);

            //Email.SendEmail(emailcontent, accountDetail.account.Email, "", emailsubject);

            return Request.CreateResponse(HttpStatusCode.OK, new { sucess = true, message = "successfully updated!!!" });
        }

        [HttpPost]
        [Route("AddLesson")]
        public HttpResponseMessage AddLesson(Assignment assignment)
        {
            using (PraescioContext db = new PraescioContext())
            {
                db.SaveChanges();

                db.SaveChanges();
            }
            return Request.CreateResponse(HttpStatusCode.OK, assignment);

        }


        [HttpPut]
        [Route("KnowledgeBankActivation")]
        public HttpResponseMessage KnowledgeBankActivation(int knowledgeBankId, int status)
        {

            PraescioContext db = new PraescioContext();
            var knowledgeBank = db.KnowledgeBank.FirstOrDefault(x => x.KnowledgeBankId == knowledgeBankId);
            knowledgeBank.IsActive = Convert.ToBoolean(status);
            knowledgeBank.ModifiedOn = DateTime.Now;
            db.Entry(knowledgeBank).State = EntityState.Modified;
            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK, knowledgeBank);


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
                             select s).ToList();
            }
            return Request.CreateResponse(HttpStatusCode.OK, stateList);


        }



        [HttpGet]
        [Route("GetAssignmentTeacherList")]
        public HttpResponseMessage GetAssignmentTeacherList(int institutionId)
        {
            List<Mst_Account> teacherList = new List<Mst_Account>();
            PraescioContext db = new PraescioContext();

            teacherList = (from t in db.Account
                           where t.IsActive == true && t.AccountTypeId == (int)AccountType.Teacher && t.InstitutionAccountId == institutionId
                           select t).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, teacherList);

        }
    }
}
