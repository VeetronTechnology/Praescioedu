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
            institionDetail.PendingStudent = Math.Max(0, Convert.ToInt16(institionDetail.Institution.NoOfStudent) - institionDetail.RegisteredStudent);
            institionDetail.PendingTeacher = Math.Max(0, Convert.ToInt16(institionDetail.Institution.NoOfTeacher) - institionDetail.RegisteredTeacher);

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
                var account = new Mst_Account() { FirstName = institutionViewModel.Institution.CustomerName, MobileNo = institutionViewModel.Institution.MobileNo, UserName = accountcode, Password = "password", ContactName = "", Email = institutionViewModel.Institution.Email, City = institutionViewModel.Institution.City, InstitutionName = institutionViewModel.Institution.InstitutionName, AccountTypeId = 2, InstitutionAccountId = institutionViewModel.Institution.InstitutionAccountId, CreatedOn = DateTime.Now, IsActive = true };
                db.Account.Add(account);
                db.SaveChanges();

                institutionViewModel.PrincipalDetail.InstitutionAccountId = institutionViewModel.Institution.InstitutionAccountId;
                institutionViewModel.PrincipalDetail.CreatedOn = DateTime.Now;
                db.PrincipalDetail.Add(institutionViewModel.PrincipalDetail);

                LogActivities(new Mst_Activities
                {
                    // ActivityName = ActivityType.InstitutionRegisteration.ToString(),
                    ActivityName = "Institution Registration",
                    CreatedBy = LoggedInAccount.AccountId,
                    Query = "New School with Name : " + institutionViewModel.Institution.InstitutionName + " & Domain Key : " + institutionViewModel.Institution.DomainKey + " has been created successfully @ " + DateTime.Now.ToString("dd'/'MM'/'yyyy HH:mm:ss") + " !!!",
                    URL = "/InstitutionAdmin/SchoolProfile?institutionid=" + institutionViewModel.Institution.InstitutionAccountId.ToString(),
                    ContactName = LoggedInAccount.FirstName,
                    CreatedOn = DateTime.Now
                });

                db.SaveChanges();

                MailType type = MailType.IndividualTeacherRegister;
                string emailsubject = "School Registration";
                Dictionary<string, string> account2 = new Dictionary<string, string>();
                account2.Add("username", institutionViewModel.Institution.DomainKey + "/" + account.UserName);
                account2.Add("password", account.Password);
                account2.Add("name", account.FirstName + ' ' + account.LastName);
                string emailcontent = Email.GetEmailContent(account2, type, ref emailsubject);

                Email.SendEmail(emailcontent, account.Email, "", emailsubject);
                string smsContent = "Hello " + account2["name"] + ", Thanks for registering with Praescioedu. UserName: " + account2["username"] + " and Password: " + account2["password"];
                SMS.SendAlertSMS(account.MobileNo, smsContent);

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
                institution.NoOfTeacher = institutionViewModel.Institution.NoOfTeacher;
                institution.NoOfStudent = institutionViewModel.Institution.NoOfStudent;
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
        public HttpResponseMessage GetInstitutionList(int pageNo = 1, int itemPerPage = 10, string searchText = "", int IsActive = -1)
        {
            List<Mst_InstitutionAccount> schoolList = new List<Mst_InstitutionAccount>();
            PraescioContext db = new PraescioContext();

            var content = (from s in db.OrganizationAccount
                           where ((string.IsNullOrEmpty(searchText) && s.InstitutionName == s.InstitutionName)
                           || (searchText != "" && s.InstitutionName.Contains(searchText))
                           || (searchText != "" && s.Email.Contains(searchText))
                           || (searchText != "" && s.LandlineNo.Contains(searchText))
                           || (searchText != "" && s.MobileNo.Contains(searchText))                           
                           )
                           select s);

            if(IsActive > -1)
            {
                bool status = Convert.ToBoolean(IsActive);
                content = content.Where(m => m.IsActive == status);
            }
            schoolList = content.OrderByDescending(x => x.CreatedOn).Skip((pageNo - 1) * itemPerPage).Take(itemPerPage).ToList();

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

            int RegisteredAccountId = 0;

            if (accountDetail.account.MobileNo == accountDetail.account.ParentNo)
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
                || accountDetail.accounttypeid == (int)Praescio.BusinessEntities.Common.AccountType.Student) {
                package.RegisteredStudent = db.Account.Where(x => x.InstitutionAccountId == accountDetail.account.InstitutionAccountId && x.AccountTypeId == (int)BusinessEntities.Common.AccountType.Student).Count();
                package.PendingStudent = Math.Max(0, Convert.ToInt32(institution.NoOfStudent) - package.RegisteredStudent);

                if (accountDetail.account.IsIndividual == false && package.PendingStudent == 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new { sucess = false, message = "The maximum registrations allowed for this Institute has been exceeded!!!" });
                }
            } else
            {
                package.RegisteredTeacher = db.Account.Where(x => x.InstitutionAccountId == accountDetail.account.InstitutionAccountId && x.AccountTypeId == (int)BusinessEntities.Common.AccountType.Teacher).Count();
                package.PendingTeacher = Math.Max(0, Convert.ToInt32(institution.NoOfTeacher) - package.RegisteredTeacher);

                if (accountDetail.account.IsIndividual == false && package.PendingTeacher == 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new { sucess = false, message = "The maximum registrations allowed for this Institute has been exceeded!!!" });
                }
            }
            /* check allowed registrations -start */

            SET_UserName:
            accountDetail.account.UserName = accountDetail.account.FirstName.Trim() + rand.Next(100000).ToString();
            accountDetail.account.Password = BusinessEntities.AppCode.Common.RandomString(8);

            if (db.Account.Any(x => x.UserName.ToLower() == accountDetail.account.UserName))
            {
                goto SET_UserName;
            }

            accountDetail.account.ActivateOn = DateTime.Now;
            accountDetail.account.CreatedOn = DateTime.Now;
            accountDetail.account.CreatedBy = LoggedInAccount.AccountId;
            accountDetail.account.StudentStandardId = accountDetail.studentstandard;
            db.Account.Add(accountDetail.account);
            db.SaveChanges();

            RegisteredAccountId = db.Account.Where(m => m.Email == accountDetail.account.Email && m.UserName.ToLower() == accountDetail.account.UserName).FirstOrDefault().AccountId;

            List<Mst_Account> ParentAccounts = new List<Mst_Account>();

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
            else if ((accountDetail.accounttypeid == (int)Praescio.BusinessEntities.Common.AccountType.IndividualStudent 
                || accountDetail.accounttypeid == (int)Praescio.BusinessEntities.Common.AccountType.Student))
            {
                if(accountDetail.studentsubject != null)
                {
                    foreach (var subject in accountDetail.studentsubject)
                    {
                        db.StandardMapping.Add(new TeacherStandardMapping { TeacherId = accountDetail.account.AccountId, StandardId = (int)accountDetail.studentstandard, SubjectId = subject });
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
                    CreatedBy = LoggedInAccount.AccountId,
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
                    ActivityName = "Individual Teacher Registration",
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
                    ActivityName = "Teacher Registration",
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
                    ActivityName = "Student Registration",
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
                    smsContent = "Hello " + account2["name"]  + ", Thanks for registering with Praescioedu. UserName: " + account2["username"] + " and Password: " + account2["password"];
                    SMS.SendAlertSMS(parent.MobileNo, emailcontent2);
                }
            }

            return Request.CreateResponse(HttpStatusCode.OK, new {sucess=true,message="successfully created!!!", RegisteredAccountId = RegisteredAccountId });

        }

        [HttpPost]
        [Route("UpdateTeacher")]
        [Route("UpdateStudent")]
        public HttpResponseMessage UpdateStudent(AccountDetailViewModel accountDetail)
        {

            PraescioContext db = new PraescioContext();
            Random rand = new Random();

            var account = db.Account.FirstOrDefault(x => x.AccountId == accountDetail.account.AccountId);
            
            if (account.AccountId > 0 && db.Account.Where(x => x.AccountId != account.AccountId && (x.Email == accountDetail.account.Email)).ToList().Count > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { sucess = false, message = "Account with same email exists!" });
            }
            if (account.AccountId > 0 && db.Account.Where(x => x.AccountId != account.AccountId && (x.MobileNo == accountDetail.account.MobileNo)).ToList().Count > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { sucess = false, message = "Account with same mobile number exists!" });
            }

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

            //db.SaveChanges();

            if (accountDetail.accounttypeid == (int)Praescio.BusinessEntities.Common.AccountType.IndividualTeacher || accountDetail.accounttypeid == (int)Praescio.BusinessEntities.Common.AccountType.Teacher)
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
                    ActivityName = "Individual Student Registration",
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
                    ActivityName = "Individual Teacher Registration",
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
                    ActivityName = "Teacher Registration",
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
                    ActivityName = "Student Registration",
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
        [Route("GetAssignmentTeacherList")]
        public HttpResponseMessage GetAssignmentTeacherList(int institutionId)
        {
            List<Mst_Account> teacherList = new List<Mst_Account>();
            PraescioContext db = new PraescioContext();

            teacherList = (from t in db.Account
                           where t.IsActive == true && t.AccountTypeId == (int)AccountType.Teacher && t.InstitutionAccountId == institutionId
                           select t).ToList();
            
            PraescioContext db1 = new PraescioContext();
            teacherList.ForEach(x =>
            {
                x.TeacherStandardMapping = db1.StandardMapping.Where(t => t.TeacherId == x.AccountId).ToList();
            }) ;

            return Request.CreateResponse(HttpStatusCode.OK, teacherList);

        }

        //private HttpResponseMessage CreateParentAccount(AccountDetailViewModel accountDetail)
        //{

        //    PraescioContext db = new PraescioContext();
        //    Random rand = new Random();

        //    var account = db.Account.FirstOrDefault(x => x.AccountId == accountDetail.account.AccountId);

        //    if (account.AccountId > 0 && db.Account.Where(x => x.AccountId != account.AccountId && (x.ParentEmail == accountDetail.account.Email)).ToList().Count > 0)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.OK, new { sucess = false, message = "Parent's account with same email exists!" });
        //    }
        //    if (account.AccountId > 0 && db.Account.Where(x => x.AccountId != account.AccountId && (x.MobileNo == accountDetail.account.ParentNo)).ToList().Count > 0)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.OK, new { sucess = false, message = "Parent's account with same mobile number exists!" });
        //    }

        //    account.FirstName = accountDetail.account.FatherName;
        //    account.LastName = accountDetail.account.LastName;
        //    account.Gender = "Male";
        //    account.DateOfBirth = null;
        //    account.Email = accountDetail.account.ParentEmail;
        //    account.MobileNo = accountDetail.account.ParentNo;
        //    account.Address = accountDetail.account.Address;
        //    account.VersionType = null;
        //    account.PackageId = null;
        //    account.AmountPaid = 0;
        //    account.BoardId = accountDetail.account.BoardId;
        //    account.StudentStandardId = accountDetail.studentstandard;
        //    account.MediumId = accountDetail.account.MediumId;
        //    account.MotherName = null;
        //    account.FatherName = null;
        //    account.ParentEmail = null;
        //    account.ParentNo = null;
        //    account.ActivateOn = accountDetail.account.ActivateOn;
        //    account.ExpiredOn = accountDetail.account.ExpiredOn;
        //    account.StateId = accountDetail.account.StateId;
        //    account.City = accountDetail.account.City;
        //    account.PinCode = accountDetail.account.PinCode;

        //    accountDetail.account.CreatedOn = DateTime.Now;
        //    accountDetail.account.CreatedBy = LoggedInAccount.AccountId;

        //    db.Entry(account).State = EntityState.Modified;
        //    db.SaveChanges();

        //    return false;
        //}
    }
}
