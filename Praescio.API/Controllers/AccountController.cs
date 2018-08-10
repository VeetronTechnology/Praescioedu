using Praescio.Domain.DAL;
using Praescio.Domain.Entities;
using Praescio.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Praescio.API.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        // GET: api/Account
        [Route("UserAuthenticate")]
        [HttpGet]
        public HttpResponseMessage UserAuthenticate(string username, string password)
        {
            AccountViewModel response = new AccountViewModel();
            string[] _username = username.Split('/');

            try
            {
                if (_username.Length > 0)
                {
                    string domain = _username[0].ToLower();
                    username = _username[1];

                    PraescioContext db = new PraescioContext();
                    var organization = db.OrganizationAccount.FirstOrDefault(x => x.DomainKey.Equals(domain, StringComparison.InvariantCultureIgnoreCase));
                    if (organization == null)
                    {
                        response.hasError = true;
                        response.errorMessage = "Domain name is invalid";

                        return Request.CreateResponse(HttpStatusCode.OK, response);
                    }

                    var account = db.Account.Where(x => x.UserName == username && x.Password == password && x.InstitutionAccountId == organization.InstitutionAccountId).FirstOrDefault();

                    if (account != null && (account.AccountTypeId == (int)Praescio.BusinessEntities.Common.AccountType.IndividualStudent || account.AccountTypeId == (int)Praescio.BusinessEntities.Common.AccountType.IndividualTeacher) && account.ExpiredOn < DateTime.Now)
                    {
                        response.Account = null;
                        response.hasError = true;
                        response.errorMessage = "Your Account has been expired!!!";

                        return Request.CreateResponse(HttpStatusCode.BadRequest, response);
                    }
                    else if (account != null && (account.AccountTypeId == (int)Praescio.BusinessEntities.Common.AccountType.IndividualStudent || account.AccountTypeId == (int)Praescio.BusinessEntities.Common.AccountType.IndividualTeacher) && account.ActivateOn > DateTime.Now)
                    {
                        response.Account = null;
                        response.hasError = true;
                        response.errorMessage = "Your Account is not yet activated. Your account will be active only on date " + account.ActivateOn.Value.ToString("DD-MMM-YYYY") + ".";

                        return Request.CreateResponse(HttpStatusCode.BadRequest, response);
                    }
                    else if (account != null)
                    {
                        account.Password = password;
                        account.OrganizationAccount.DomainKey = domain;
                        response.Account = account;
                        response.hasError = false;

                        if(account.AccountTypeId == (int)Praescio.BusinessEntities.Common.AccountType.IndividualTeacher || account.AccountTypeId == (int)Praescio.BusinessEntities.Common.AccountType.Teacher)
                        {
                            PraescioContext db1 = new PraescioContext();
                            account.TeacherStandardMapping = db1.StandardMapping.Where(t => t.TeacherId == account.AccountId).ToList();

                            if(account.AccountTypeId == (int)Praescio.BusinessEntities.Common.AccountType.IndividualTeacher && account.VersionType == "Paid")
                            {
                                //var payment1 = db.PaymentTransaction.Where(m => m.AccountId == account.AccountId && m.PackageId == account.PackageId && m.Status.ToLower() == "success").FirstOrDefault();
                                var payment1 = db1.PaymentTransaction.Where(t => t.AccountId == account.AccountId && t.PackageId == account.PackageId).FirstOrDefault();
                                if (payment1 == null)
                                {
                                    response.Account = null;
                                    response.hasError = true;
                                    response.errorMessage = "Your payment has not processed.";

                                    return Request.CreateResponse(HttpStatusCode.BadRequest, response);
                                }
                            }
                        }

                        // School admin
                        if (account.AccountTypeId == (int)Praescio.BusinessEntities.Common.AccountType.Admin)
                        {
                            var institution = db.OrganizationAccount.FirstOrDefault(x => x.InstitutionAccountId == account.InstitutionAccountId);
                            account.Board = institution.Board;
                            account.BoardId = institution.BoardId;
                            account.Medium = institution.Medium;
                            account.MediumId = institution.MediumId;
                        }

                        if (account.AccountTypeId == (int)Praescio.BusinessEntities.Common.AccountType.Student || account.AccountTypeId == (int)Praescio.BusinessEntities.Common.AccountType.IndividualStudent)
                        {
                            response.ParentAccount = db.Account.Where(t => t.AccountTypeId == (int)Praescio.BusinessEntities.Common.AccountType.StudentParent && t.Email == account.ParentEmail).FirstOrDefault();

                            // check if payment done
                            if(account.VersionType == "Paid")
                            {
                                var creator = (account.CreatedBy == null ? null : db.Account.Where(m => m.AccountId == (int)account.CreatedBy).FirstOrDefault());
                                if (account.AccountTypeId == (int)Praescio.BusinessEntities.Common.AccountType.IndividualStudent
                                    && creator != null && creator.AccountTypeId != (int)Praescio.BusinessEntities.Common.AccountType.SuperAdmin)
                                {
                                    var payment = db.PaymentTransaction.Where(m => m.AccountId == account.AccountId && m.PackageId == account.PackageId && (m.Status.ToLower() == "success")).FirstOrDefault();
                                    if (payment == null)
                                    {
                                        response.Account = null;
                                        response.hasError = true;
                                        response.errorMessage = "Your payment has not processed.";

                                        return Request.CreateResponse(HttpStatusCode.BadRequest, response);
                                    }
                                }
                            }
                        }

                        if (account.AccountTypeId == (int)Praescio.BusinessEntities.Common.AccountType.StudentParent)
                        {
                            response.StudentAccount = db.Account.Where(t => (t.AccountTypeId == (int)Praescio.BusinessEntities.Common.AccountType.Student || t.AccountTypeId == (int)Praescio.BusinessEntities.Common.AccountType.IndividualStudent) && t.ParentEmail == account.Email).FirstOrDefault();
                        }
                        response.Account = account;

                        return Request.CreateResponse(HttpStatusCode.OK, response);
                    }
                    else
                    {
                        response.Account = null;
                        response.hasError = true;
                        response.errorMessage = "No record found!!!";

                        return Request.CreateResponse(HttpStatusCode.BadRequest, response);
                    }
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "no record found");
                }
            }
            catch (Exception ex)
            {

                response.Account = null;
                response.hasError = true;
                response.errorMessage = ex.Message + "---" + ex.StackTrace;

                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }

        [HttpGet]
        [Route("GetAccountDetail")]
        public HttpResponseMessage GetAccountDetail()
        {
            try
            {
                PraescioContext db = new PraescioContext();

                var AccounDetail = (from a in db.Account
                                    join i in db.OrganizationAccount on a.InstitutionAccountId equals i.InstitutionAccountId
                                    select new { username = i.DomainKey + "/" + a.UserName, password = a.Password, InstitutionName = i.InstitutionName, CreatedOn = a.CreatedOn, accountType = a.AccountType.AccountType, AccountDetail = a }).OrderByDescending(x => x.CreatedOn).ToList();

                return Request.CreateResponse(HttpStatusCode.OK, AccounDetail);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("SendResetPasswordLink")]
        [HttpGet]
        public HttpResponseMessage SendResetPasswordLink(string username, string email)
        {
            try
            {
                PraescioContext db = new PraescioContext();
                var account = db.Account.FirstOrDefault(x => x.UserName.ToLower() == username.ToLower() && x.Email.ToLower() == email.ToLower());



                if (account != null)
                {
                    Dictionary<string, string> content = new Dictionary<string, string>();
                    content.Add("username", account.UserName);
                    content.Add("email", account.Email);


                    PasswordResetAccount reset = new PasswordResetAccount
                    {
                        isEmailConfirmed = false,
                        UserAccountId = account.AccountId,
                        CreatedOn = DateTime.Now
                    };

                    db.PasswordResetAccount.Add(reset);
                    db.SaveChanges();

                    content.Add("guid", reset.Id.ToString());

                    string subject = "";
                    var emailcontent = Praescio.BusinessEntities.Email.GetEmailContent(content, BusinessEntities.Common.MailType.ResetPassword, ref subject);
                    Praescio.BusinessEntities.Email.SendEmail(emailcontent, account.Email, "", subject);

                    return Request.CreateResponse(HttpStatusCode.OK, "mail has been send successfully!!!");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Username/Email is invalid");
                }

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("ChangedPassword")]
        [HttpGet]
        public HttpResponseMessage ChangedPassword(string password, string guid)
        {
            try
            {
                PraescioContext db = new PraescioContext();
                var dateCompare = DateTime.Now.AddDays(1);
                var resetAccount = db.PasswordResetAccount.FirstOrDefault(x => x.Id.ToString().ToLower() == guid.ToLower() && x.isEmailConfirmed == false && x.CreatedOn < dateCompare);
                if (resetAccount != null)
                {
                    var passwordUpdate = db.Account.FirstOrDefault(x => x.AccountId == resetAccount.UserAccountId);
                    passwordUpdate.Password = password.Trim();

                    db.Entry(passwordUpdate).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK, "Password reset successfully!!!");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Link has been expired please click on forgot password on login again.");
                }

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        
        [Route("ChangePassword")]
        [HttpPost]
        public HttpResponseMessage ChangePassword(ChangePasswordViewModel model)
        {
            AccountViewModel response = new AccountViewModel();

            try
            {
                if (model.AccountId > 0)
                {
                    PraescioContext db = new PraescioContext();

                    var account = db.Account.Where(x => x.UserName == model.Username && x.Password == model.PasswordCurrent && x.AccountId == model.AccountId).FirstOrDefault();

                    if (account != null && (account.AccountTypeId == (int)Praescio.BusinessEntities.Common.AccountType.IndividualStudent || account.AccountTypeId == (int)Praescio.BusinessEntities.Common.AccountType.IndividualTeacher) && account.ExpiredOn < DateTime.Now)
                    {
                        response.Account = null;
                        response.hasError = true;
                        response.errorMessage = "Your Account has been expired!!!";

                        return Request.CreateResponse(HttpStatusCode.BadRequest, response);
                    }
                    else if (account != null)
                    {
                        account.Password = model.Password;
                        account.UpdatePwdDateTime = DateTime.Now;

                        db.Entry(account).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        response.Account = account;
                        response.hasError = false;

                        return Request.CreateResponse(HttpStatusCode.OK, response);
                    }
                    else
                    {
                        response.Account = null;
                        response.hasError = true;
                        response.errorMessage = "No record found!!!";

                        return Request.CreateResponse(HttpStatusCode.BadRequest, response);
                    }
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "no record found");
                }
            }
            catch (Exception ex)
            {
                response.Account = null;
                response.hasError = true;
                response.errorMessage = ex.Message + "---" + ex.StackTrace;

                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }
    }
}
