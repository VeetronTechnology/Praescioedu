using Newtonsoft.Json;
using Praescio.BusinessEntities.Common;
using Praescio.Domain.Entities;
using Praescio.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Praescio.Controllers
{
    public class JsonData
    {
        public string amount { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string productinfo { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string zipcode { get; set; }
        public string surl { get; set; }
        public string curl { get; set; }
        public string furl { get; set; }
        public string service_provider { get; set; }
        public string key { get; set; }
        public string hash { get; set; }
        public string hash_string { get; set; }
        public string txnid { get; set; }
        public string udf1 { get; set; }
        public string udf2 { get; set; }
        public string udf3 { get; set; }
        public string udf4 { get; set; }
        public string udf5 { get; set; }
        public string pg { get; set; }
    }

    public class JsonResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Content { get; set; }
        public object PaymentInfo { get; set; }
    }

    public class PaymentController : Controller
    {

        public string action1 = string.Empty;
        public string hash1 = string.Empty;
        public string txnid1 = string.Empty;

        public ActionResult PaymentRequest()
        {
            dynamic paymentinfo = new System.Dynamic.ExpandoObject();
            paymentinfo.Key = ConfigurationManager.AppSettings["PAYU_Key"];
            paymentinfo.SALT = ConfigurationManager.AppSettings["PAYU_SALT"];

            ViewBag.paymentinfo = paymentinfo;
            return View();
        }

        [System.Web.Http.HttpPost]
        public ActionResult PayNow([FromBody] JsonData request)
        {
            dynamic paymentinfo = new System.Dynamic.ExpandoObject();
            paymentinfo.Key = ConfigurationManager.AppSettings["PAYU_Key"];
            paymentinfo.SALT = ConfigurationManager.AppSettings["PAYU_SALT"];

            //dynamic result = new System.Dynamic.ExpandoObject();
            JsonResponse result = new JsonResponse();


            //var costObjects = JsonConvert.DeserializeObject(request);

            try
            {

                string[] hashVarsSeq;
                string hash_string = string.Empty;


                if (string.IsNullOrEmpty(Request.Params["txnid"])) // generating txnid
                {
                    Random rnd = new Random();
                    string strHash = Generatehash512(rnd.ToString() + DateTime.Now);
                    txnid1 = strHash.ToString().Substring(0, 20);

                }
                else
                {
                    txnid1 = Request.Params["txnid"];
                }
                if (string.IsNullOrEmpty(Request.Params["hash"])) // generating hash value
                {
                    if (
                        string.IsNullOrEmpty(ConfigurationManager.AppSettings["PAYU_Key"]) ||
                        string.IsNullOrEmpty(txnid1) ||
                        string.IsNullOrEmpty(request.amount) ||
                        string.IsNullOrEmpty(request.firstname) ||
                        string.IsNullOrEmpty(request.email) ||
                        string.IsNullOrEmpty(request.phone) ||
                        string.IsNullOrEmpty(request.productinfo) ||
                        string.IsNullOrEmpty(request.surl) ||
                        string.IsNullOrEmpty(request.furl) //||
                        ///string.IsNullOrEmpty(request.service_provider)
                        )
                    {
                        //error
                        result.Success = false;
                        result.Message = "Invalid values submitted.";
                    }
                    else
                    {
                        result.Success = true;
                        hashVarsSeq = ConfigurationManager.AppSettings["PAYU_hashSequence"].Split('|'); // spliting hash sequence from config
                        hash_string = "";
                        foreach (string hash_var in hashVarsSeq)
                        {
                            if (hash_var == "key")
                            {
                                hash_string = hash_string + ConfigurationManager.AppSettings["PAYU_KEY"];
                                hash_string = hash_string + '|';
                            }
                            else if (hash_var == "txnid")
                            {
                                hash_string = hash_string + txnid1;
                                hash_string = hash_string + '|';
                            }
                            else if (hash_var == "amount")
                            {
                                hash_string = hash_string + Convert.ToDecimal(request.amount).ToString("g29");
                                hash_string = hash_string + '|';
                            }
                            else
                            {
                                System.Reflection.PropertyInfo pi = request.GetType().GetProperty(hash_var);
                                string val = (pi == null ? null : (String)(pi.GetValue(request, null)));
                                hash_string = hash_string + (val != null ? val : "");// isset if else
                                hash_string = hash_string + '|';
                            }
                        }

                        hash_string += ConfigurationManager.AppSettings["PAYU_SALT"];// appending SALT

                        hash1 = Generatehash512(hash_string).ToLower();         //generating hash
                        action1 = ConfigurationManager.AppSettings["PAYU_BASE_URL"] + "/_payment";// setting URL
                    }
                }

                else if (!string.IsNullOrEmpty(Request.Params["hash"]))
                {
                    hash1 =  Request.Params["hash"];
                    action1 = ConfigurationManager.AppSettings["PAYU_BASE_URL"] + "/_payment";

                }
                

                if (!string.IsNullOrEmpty(hash1))
                {
                    //hash.Value = hash1;
                    //txnid.Value = txnid1;

                    //var amount = Request.Params["amount"];
                    //var firstname = Request.Params["firstname"];
                    //var lastname = Request.Params["lastname"];
                    //var email = Request.Params["email"];
                    //var phone = Request.Params["phone"];
                    //var productinfo = Request.Params["productinfo"];
                    //var surl = Request.Params["surl"];
                    //var curl = Request.Params["curl"];
                    //var furl = Request.Params["furl"];
                    //var service_provider = Request.Params["service_provider"];
                    //var address1 = Request.Params["address1"];
                    //var address2 = Request.Params["address2"];
                    //var city = Request.Params["city"];
                    //var state = Request.Params["state"];
                    //var country = Request.Params["country"];
                    //var zipcode = Request.Params["zipcode"];
                    //var udf1 = Request.Params["udf1"];
                    //var udf2 = Request.Params["udf2"];
                    //var udf3 = Request.Params["udf3"];
                    //var udf4 = Request.Params["udf4"];
                    //var udf5 = Request.Params["udf5"];
                    //var pg = Request.Params["pg"];

                    System.Collections.Hashtable data = new System.Collections.Hashtable(); // adding values in gash table for data post
                    data.Add("hash", hash1);
                    data.Add("txnid", txnid1);
                    data.Add("key", ConfigurationManager.AppSettings["PAYU_Key"]);
                    string AmountForm = Convert.ToDecimal(request.amount.Trim()).ToString("g29");// eliminating trailing zeros
                    
                    data.Add("amount", AmountForm);
                    data.Add("firstname", request.firstname.Trim());
                    data.Add("email", request.email.Trim());
                    data.Add("phone", request.phone.Trim());
                    data.Add("productinfo", request.productinfo.Trim());
                    data.Add("surl", request.surl.Trim());
                    data.Add("furl", request.furl.Trim());
                    data.Add("lastname", request.lastname.Trim());
                    data.Add("curl", request.curl.Trim());
                    data.Add("address1", request.address1.Trim());
                    data.Add("address2", (request.address2 == null ? null : request.address2.Trim()));
                    data.Add("city", (request.city == null ? null : request.city.Trim()));
                    data.Add("state", (request.state == null ? null : request.state.Trim()));
                    data.Add("country", (request.country == null ? null : request.country.Trim()));
                    data.Add("zipcode", (request.zipcode == null ? null : request.zipcode.Trim()));
                    data.Add("udf1", (request.udf1 == null ? null : request.udf1.Trim()));
                    data.Add("udf2", (request.udf2 == null ? null : request.udf2.Trim()));
                    data.Add("udf3", (request.udf3 == null ? null : request.udf3.Trim()));
                    data.Add("udf4", (request.udf4 == null ? null : request.udf4.Trim()));
                    data.Add("udf5", (request.udf5 == null ? null : request.udf5.Trim()));
                    data.Add("pg", (request.pg == null ? null : request.pg.Trim()));
                    data.Add("service_provider", (request.service_provider == null ? null : request.service_provider.Trim()));
                    
                    string strForm = PreparePOSTForm(action1, data);
                    result.Content = strForm;
                }
                else
                {
                    //no hash
                }
            }
            catch (Exception ex)
            {
                //Response.Write("<span style='color:red'>" + ex.Message + "</span>");
            }
            
            result.PaymentInfo = paymentinfo;
            return Json(result);
            //return Request.re (HttpStatusCode.OK, result);
        }
        
        public ActionResult PaymentFailed()
        {
            return View();
        }

        public ActionResult PaymentCancelled()
        {
            return View();
        }

        public ActionResult PaymentResponse()
        {
            int accountTypeId = Convert.ToInt16(Request.Params["udf1"].ToString());
            int accountId = (Request.Params["udf2"].ToString() == "" ? 0 : Convert.ToInt32(Request.Params["udf2"].ToString()));
            string redirectURL = "";
            if (accountTypeId == (int)AccountType.IndividualTeacher)
            {
                redirectURL = Url.Action("RegisterIndividualTeacher", "PraescioAdmin").ToString() + "?accountid=" + accountId;
            }
            else if (accountTypeId == (int)AccountType.IndividualStudent)
            {
                redirectURL = Url.Action("RegisterIndividualStudent", "PraescioAdmin").ToString() + "?accountid=" + accountId;
            }

            try
            {
                string[] merc_hash_vars_seq;
                string merc_hash_string = string.Empty;
                string merc_hash = string.Empty;
                string order_id = string.Empty;
                string hash_seq = "key|txnid|amount|productinfo|firstname|email|udf1|udf2|udf3|udf4|udf5|udf6|udf7|udf8|udf9|udf10";

                dynamic result = new System.Dynamic.ExpandoObject();

                if (Request.Params["status"] == "success")
                {
                    merc_hash_vars_seq = hash_seq.Split('|');
                    Array.Reverse(merc_hash_vars_seq);
                    merc_hash_string = ConfigurationManager.AppSettings["PAYU_SALT"] + "|" + Request.Params["status"];

                    Dictionary<string, string> metaList = new Dictionary<string, string>();
                    foreach (string merc_hash_var in merc_hash_vars_seq)
                    {
                        merc_hash_string += "|";
                        merc_hash_string = merc_hash_string + (Request.Params[merc_hash_var] != null ? Request.Params[merc_hash_var] : "");
                        metaList.Add(merc_hash_var, Request.Params[merc_hash_var]);
                    }
                    //Response.Write(merc_hash_string); exit;
                    //result.merc_hash_string = merc_hash_string;

                    merc_hash = Generatehash512(merc_hash_string).ToLower();
                    
                    if (merc_hash != Request.Params["hash"])
                    {
                        //Response.Write("Hash value did not matched");
                        order_id = null;
                    }
                    else
                    {
                        order_id = Request.Params["txnid"];
                        //Response.Write("value matched");
                        //Hash value did not matched
                    }
                    Dictionary<string, string> metaList2 = new Dictionary<string, string>();
                    try
                    {
                        metaList2.Add("PG_TYPE", Request.Params["PG_TYPE"]);
                        metaList2.Add("encryptedPaymentId", Request.Params["encryptedPaymentId"]);
                        metaList2.Add("bank_ref_num", Request.Params["bank_ref_num"]);
                        metaList2.Add("bankcode", Request.Params["bankcode"]);
                        metaList2.Add("name_on_card", Request.Params["name_on_card"]);
                        metaList2.Add("cardnum", Request.Params["cardnum"]);
                        metaList2.Add("amount_split", Request.Params["amount_split"]);
                        metaList2.Add("payuMoneyId", Request.Params["payuMoneyId"]);
                        metaList2.Add("discount", Request.Params["discount"]);
                        metaList2.Add("net_amount_debit", Request.Params["net_amount_debit"]);
                    }
                    catch (Exception ex)
                    {

                        //throw;
                    }

                    PaymentTransaction model = new PaymentTransaction();
                    model.AccountId = accountId;
                    model.Amount = Convert.ToDecimal(Request.Params["amount"]);
                    model.CreatedBy = Praescio.Models.Common.ACCOUNT.AccountId;
                    model.CurrencyCode = "INR"; // Request.Params[""]; // TODO
                    model.ErrorCode = Request.Params["error"];
                    model.ErrorText = Request.Params["error_Message"];
                    model.InstitutionAccountId = null;// Praescio.Models.Common.ACCOUNT.InstitutionAccountId; // TODO - if paid for institute
                    //var productinfo = Request.Params["productinfo"];
                    //Mst_Package package = JsonConvert.DeserializeObject<Mst_Package>(productinfo);
                    model.PackageId = Convert.ToInt32(Request.Params["productinfo"]);
                    model.PaymentMethod = "payu";
                    model.TransactionId = order_id;
                    model.ReferenceNumber = null;
                    model.Status = Request.Params["status"];

                    var metaData = new { metalist = metaList, metalist2 = metaList2 };
                    model.MetaData = JsonConvert.SerializeObject(metaData);
                    
                    using (HttpClient _client = new HttpClient())
                    {
                        _client.DefaultRequestHeaders.Clear();
                        _client.DefaultRequestHeaders.Accept.Clear();
                        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        
                        _client.DefaultRequestHeaders.Authorization =
                            new AuthenticationHeaderValue(
                                "Authorization",
                                string.Format("{0}:{1}", Praescio.Models.Common.ACCOUNT.UserName.ToString(),
                                Praescio.Models.Common.ACCOUNT.Password.ToString()));

                        _client.DefaultRequestHeaders.Add("UserIP", Convert.ToString(HttpContext.Request.ServerVariables["REMOTE_ADDR"].ToString()).ToLower());
                        _client.DefaultRequestHeaders.Add("DomainKey", Convert.ToString(Praescio.Models.Common.ACCOUNT.OrganizationAccount.DomainKey).ToLower());

                        HttpResponseMessage responseMessage = _client.PostAsync(Common.baseUrl + "Payment/PaymentResponse", model, new JsonMediaTypeFormatter()).Result;

                        if (responseMessage.IsSuccessStatusCode)
                        {
                            //ViewBag.Result = new { Success = true, Message = "Payment process successfull!",
                            //    RedirectURL = redirectURL
                            //};
                            ViewBag.Success = true;
                            ViewBag.Message = "Payment successfull!";
                            ViewBag.RedirectURL = redirectURL;
                            return View();
                        }
                        else
                        {
                            ViewBag.Result = new { Success = false, Message = "Payment process failed!",
                                RedirectURL = redirectURL
                            };

                            ViewBag.Success = false;
                            ViewBag.Message = "Payment failed!";
                            ViewBag.RedirectURL = redirectURL;
                            return View();
                        }
                    }
                }
                else
                {
                    ViewBag.Success = false;
                    ViewBag.Message = "Payment failed!";
                    ViewBag.RedirectURL = redirectURL;
                    return View();
                }
            }
            catch (Exception ex)
            {
                //Response.Write("<span style='color:red'>" + ex.Message + "</span>");
                //ViewBag.Result = new { Success = false, Message = "Payment process failed!" };
                ViewBag.Success = false;
                ViewBag.Message = "Payment failed!";
                ViewBag.RedirectURL = redirectURL;
            }
            return View();
        }

        private string Generatehash512(string text)
        {

            byte[] message = Encoding.UTF8.GetBytes(text);

            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] hashValue;
            SHA512Managed hashString = new SHA512Managed();
            string hex = "";
            hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                hex += String.Format("{0:x2}", x);
            }
            return hex;

        }

        private string PreparePOSTForm(string url, System.Collections.Hashtable data)      // post form
        {
            //Set a name for the form
            string formID = "PostForm";
            //Build the form using the specified data to be posted.
            StringBuilder strForm = new StringBuilder();
            strForm.Append("<form id=\"" + formID + "\" name=\"" +
                           formID + "\" action=\"" + url +
                           "\" method=\"POST\">");

            foreach (System.Collections.DictionaryEntry key in data)
            {

                strForm.Append("<input type=\"hidden\" name=\"" + key.Key +
                               "\" value=\"" + key.Value + "\">");
            }


            strForm.Append("</form>");
            //Build the JavaScript which will do the Posting operation.
            StringBuilder strScript = new StringBuilder();
            strScript.Append("<script language='javascript'>");
            strScript.Append("var v" + formID + " = document." +
                             formID + ";");
            strScript.Append("v" + formID + ".submit();");
            strScript.Append("</script>");
            //Return the form and the script concatenated.
            //(The order is important, Form then JavaScript)
            return strForm.ToString() + strScript.ToString();
        }
    }
}