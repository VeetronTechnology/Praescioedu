using Newtonsoft.Json;
using Praescio.BusinessEntities.Common;
using Praescio.Domain.AppCode;
using Praescio.Domain.Entities;
using Praescio.Domain.Models;
using Praescio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Praescio.Controllers
{
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    HttpResponseMessage response = CommonGenerics.GetAsynch(Common.baseUrl + "Account/UserAuthenticate?username=" + model.Username.Trim()+"&password="+model.Password.Trim());
                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = response.Content.ReadAsStringAsync().Result;
                        var userdetail = JsonConvert.DeserializeObject<AccountViewModel>(responseString);

                        Common.ACCOUNT = userdetail.Account;

                        if (!string.IsNullOrEmpty(returnUrl))
                            return Redirect(returnUrl);

                        if (userdetail.Account.AccountTypeId == (int)AccountType.SuperAdmin)
                            return RedirectToAction("Dashboard", "PraescioAdmin");
                        else if (userdetail.Account.AccountTypeId == (int)AccountType.Admin)
                            return RedirectToAction("Dashboard", "InstitutionAdmin");
                        else if (userdetail.Account.AccountTypeId == (int)AccountType.Teacher)
                            return RedirectToAction("Dashboard", "InstitutionTeacher");
                        else if (userdetail.Account.AccountTypeId == (int)AccountType.Student)
                            return RedirectToAction("Dashboard", "InstitutionStudent");
                        else if (userdetail.Account.AccountTypeId == (int)AccountType.IndividualTeacher)
                            return RedirectToAction("Dashboard", "IndividualTeacher");
                        else if (userdetail.Account.AccountTypeId == (int)AccountType.IndividualStudent)
                            return RedirectToAction("Dashboard", "IndividualStudent");
                        else if (userdetail.Account.AccountTypeId == (int)AccountType.StudentParent)
                            return RedirectToAction("Dashboard", "PraescioAdmin");
                        else
                            return View(model);
                    }
                    else
                    {
                        string responseString = response.Content.ReadAsStringAsync().Result;
                        var userdetail = JsonConvert.DeserializeObject<AccountViewModel>(responseString);

                        ViewBag.Error = userdetail.errorMessage;
                        return View(model);
                    }

                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message + "-----" + ex.StackTrace;
                return View(model);
            }
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        public ActionResult PasswordReset(string guid)
        {
            ViewBag.Guid = guid;
            return View();
        }

        public ActionResult Logoff()
        {
            Common.ACCOUNT = null;
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            ChangePasswordViewModel model = new ChangePasswordViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var client = new HttpClient())
                    {
                        model.Username = Common.ACCOUNT.UserName;
                        model.AccountId = Common.ACCOUNT.AccountId;

                        HttpResponseMessage response = CommonGenerics.PostAsynch(Common.baseUrl + "Account/ChangePassword", model);
                        if (response.IsSuccessStatusCode)
                        {
                            string responseString = response.Content.ReadAsStringAsync().Result;
                            var userdetail = JsonConvert.DeserializeObject<AccountViewModel>(responseString);

                            Common.ACCOUNT = userdetail.Account;

                            string returnUrl = "";
                            if (userdetail.Account.AccountTypeId == (int)AccountType.SuperAdmin)
                                returnUrl = this.Url.Action("Dashboard", "PraescioAdmin");
                            else if (userdetail.Account.AccountTypeId == (int)AccountType.Admin)
                                returnUrl = this.Url.Action("Dashboard", "InstitutionAdmin");
                            else if (userdetail.Account.AccountTypeId == (int)AccountType.Teacher)
                                returnUrl = this.Url.Action("Dashboard", "InstitutionTeacher");
                            else if (userdetail.Account.AccountTypeId == (int)AccountType.Student)
                                returnUrl = this.Url.Action("Dashboard", "InstitutionStudent");
                            else if (userdetail.Account.AccountTypeId == (int)AccountType.IndividualTeacher)
                                returnUrl = this.Url.Action("Dashboard", "IndividualTeacher");
                            else if (userdetail.Account.AccountTypeId == (int)AccountType.IndividualStudent)
                                returnUrl = this.Url.Action("Dashboard", "IndividualStudent");
                            else if (userdetail.Account.AccountTypeId == (int)AccountType.StudentParent)
                                returnUrl = this.Url.Action("Dashboard", "PraescioAdmin");

                            ViewBag.ReturnUrl = returnUrl;
                            ViewBag.Success = "Password changed successfully.";
                            return View(model);
                        }
                        else
                        {
                            string responseString = response.Content.ReadAsStringAsync().Result;
                            var userdetail = JsonConvert.DeserializeObject<AccountViewModel>(responseString);

                            //ViewBag.Error = userdetail.errorMessage;
                            ViewBag.Error = "Password change failed. Please try again";
                            return View(model);
                        }
                    }
                }
                else
                {
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message + "-----" + ex.StackTrace;
                return View(model);
            }
        }

    }
}
