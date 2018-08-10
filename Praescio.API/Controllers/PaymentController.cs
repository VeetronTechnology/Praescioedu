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
using Newtonsoft.Json;

namespace Praescio.API.Controllers
{
    [AuthorizeInitializeAttribute]
    [RoutePrefix("api/Payment")]
    public class PaymentController : BaseApiController
    {
        [HttpPost]
        [Route("PaymentResponse")]
        public HttpResponseMessage PaymentResponse(PaymentTransaction model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (PraescioContext db = new PraescioContext())
                    {
                        var package = db.Package.Where(m => m.PackageId == model.PackageId).FirstOrDefault();
                        model.ReferenceNumber = JsonConvert.SerializeObject(package);

                        DateTime myDateTime = DateTime.UtcNow;
                        //string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
                        model.TransactionDate = myDateTime;
                        model.CreatedOn = myDateTime;

                        db.PaymentTransaction.Add(model);
                        db.SaveChanges();

                        if (model.Status == "success")
                        {
                            var account = db.Account.Where(m => m.AccountId == model.AccountId).FirstOrDefault();
                            account.IsActive = true;
                            
                            db.Account.Attach(account);
                            db.Entry(account).State = EntityState.Modified;
                            db.SaveChanges();
                        } else
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, new { success = false, message = "Payment process failed!" });
                        }
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, new { success = true, message = "Payment processed successfully!" });
                } else
                {
                    //using (PraescioContext db = new PraescioContext())
                    //{
                    //    db.PaymentTransaction.Add(model);
                    //    db.SaveChanges();

                    //    if (model.Status == "success")
                    //    {
                    //        var account = db.Account.Where(m => m.AccountId == model.Account.AccountId).FirstOrDefault();
                    //        account.IsActive = true;

                    //        db.Account.Attach(account);
                    //        db.Entry(account).State = EntityState.Modified;
                    //        db.SaveChanges();
                    //    }
                    //}
                    return Request.CreateResponse(HttpStatusCode.OK, new { success = false, message = "Payment process failed!" });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { success = false, message = "Payment process failed!" });
                //throw;
            }
        }
        
    }
}
