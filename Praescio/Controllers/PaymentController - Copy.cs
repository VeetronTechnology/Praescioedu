using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
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

            dynamic result = new System.Dynamic.ExpandoObject();


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
                        string.IsNullOrEmpty(Request.Params["amount"]) ||
                        string.IsNullOrEmpty(Request.Params["firstname"]) ||
                        string.IsNullOrEmpty(Request.Params["email"]) ||
                        string.IsNullOrEmpty(Request.Params["phone"]) ||
                        string.IsNullOrEmpty(Request.Params["productinfo"]) ||
                        string.IsNullOrEmpty(Request.Params["surl"]) ||
                        string.IsNullOrEmpty(Request.Params["furl"]) ||
                        string.IsNullOrEmpty(Request.Params["service_provider"])
                        )
                    {
                        //error
                        result.success = false;
                        result.message = "Invalid values submitted.";
                    }
                    else
                    {
                        result.success = true;
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
                                hash_string = hash_string + Convert.ToDecimal(Request.Params[hash_var]).ToString("g29");
                                hash_string = hash_string + '|';
                            }
                            else
                            {
                                hash_string = hash_string + (Request.Params[hash_var] != null ? Request.Params[hash_var] : "");// isset if else
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
                    hash1 = Request.Params["hash"];
                    action1 = ConfigurationManager.AppSettings["PAYU_BASE_URL"] + "/_payment";

                }
                

                if (!string.IsNullOrEmpty(hash1))
                {
                    //hash.Value = hash1;
                    //txnid.Value = txnid1;

                    var amount = Request.Params["amount"];
                    var firstname = Request.Params["firstname"];
                    var lastname = Request.Params["lastname"];
                    var email = Request.Params["email"];
                    var phone = Request.Params["phone"];
                    var productinfo = Request.Params["productinfo"];
                    var surl = Request.Params["surl"];
                    var curl = Request.Params["curl"];
                    var furl = Request.Params["furl"];
                    var service_provider = Request.Params["service_provider"];
                    var address1 = Request.Params["address1"];
                    var address2 = Request.Params["address2"];
                    var city = Request.Params["city"];
                    var state = Request.Params["state"];
                    var country = Request.Params["country"];
                    var zipcode = Request.Params["zipcode"];
                    var udf1 = Request.Params["udf1"];
                    var udf2 = Request.Params["udf2"];
                    var udf3 = Request.Params["udf3"];
                    var udf4 = Request.Params["udf4"];
                    var udf5 = Request.Params["udf5"];
                    var pg = Request.Params["pg"];

                    System.Collections.Hashtable data = new System.Collections.Hashtable(); // adding values in gash table for data post
                    data.Add("hash", hash1);
                    data.Add("txnid", txnid1);
                    data.Add("key", ConfigurationManager.AppSettings["PAYU_Key"]);
                    string AmountForm = Convert.ToDecimal(amount.Trim()).ToString("g29");// eliminating trailing zeros
                    
                    data.Add("amount", AmountForm);
                    data.Add("firstname", firstname.Trim());
                    data.Add("email", email.Trim());
                    data.Add("phone", phone.Trim());
                    data.Add("productinfo", productinfo.Trim());
                    data.Add("surl", surl.Trim());
                    data.Add("furl", furl.Trim());
                    data.Add("lastname", lastname.Trim());
                    data.Add("curl", curl.Trim());
                    data.Add("address1", address1.Trim());
                    data.Add("address2", address2.Trim());
                    data.Add("city", city.Trim());
                    data.Add("state", state.Trim());
                    data.Add("country", country.Trim());
                    data.Add("zipcode", zipcode.Trim());
                    data.Add("udf1", udf1.Trim());
                    data.Add("udf2", udf2.Trim());
                    data.Add("udf3", udf3.Trim());
                    data.Add("udf4", udf4.Trim());
                    data.Add("udf5", udf5.Trim());
                    data.Add("pg", pg.Trim());
                    data.Add("service_provider", service_provider.Trim());


                    string strForm = PreparePOSTForm(action1, data);
                    //Page.Controls.Add(new LiteralControl(strForm));

                    result.content = strForm;

                }

                else
                {
                    //no hash

                }

            }

            catch (Exception ex)

            {
                Response.Write("<span style='color:red'>" + ex.Message + "</span>");

            }
            
            result.paymentinfo = paymentinfo;
            return Json(result);
            //return Request.re (HttpStatusCode.OK, result);
        }

        public ActionResult PaymentResponse()
        {
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


                    foreach (string merc_hash_var in merc_hash_vars_seq)
                    {
                        merc_hash_string += "|";
                        merc_hash_string = merc_hash_string + (Request.Params[merc_hash_var] != null ? Request.Params[merc_hash_var] : "");

                    }
                    //Response.Write(merc_hash_string); exit;
                    //result.merc_hash_string = merc_hash_string;

                    merc_hash = Generatehash512(merc_hash_string).ToLower();



                    if (merc_hash != Request.Params["hash"])
                    {
                        Response.Write("Hash value did not matched");

                    }
                    else
                    {
                        order_id = Request.Params["txnid"];

                        Response.Write("value matched");


                        //Hash value did not matched
                    }

                }
                else
                {

                    Response.Write("Hash value did not matched");
                    // osc_redirect(osc_href_link(FILENAME_CHECKOUT, 'payment' , 'SSL', null, null,true));

                }
            }

            catch (Exception ex)
            {
                Response.Write("<span style='color:red'>" + ex.Message + "</span>");

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