using Praescio.BusinessEntities.Common;
using Praescio.Domain.DAL;
using Praescio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Xml;

namespace Praescio.BusinessEntities
{
    public class Email
    {
        public static void SendEmail(string mailbody, string emailto, string recipient, string subject)
        {
            try
            {
                if (!string.IsNullOrEmpty(emailto))
                {
                    var fromEmail = ConfigurationManager.AppSettings["FromEmail"];
                    var fromEmailName = ConfigurationManager.AppSettings["FromEmailName"];
                    MailMessage newMail = new MailMessage { From = new MailAddress(fromEmail, fromEmailName) }; //CommonKey.FromEmail
                    foreach (var item in emailto.Split(',').ToArray().Distinct())
                    {
                        try
                        {
                            newMail.To.Add(item);
                        }
                        catch (Exception ex)
                        {
                            new Praescio.BusinessEntities.BAL.ExceptionLogging().AddLogToDB(ex.StackTrace, ex.Message, "", ExceptionType.Email);
                        }
                    }
                    foreach (var item in recipient.Split(',').ToArray().Distinct())
                    {
                        try
                        {
                            newMail.CC.Add(item);
                        }
                        catch (Exception ex)
                        {
                            //new Praescio.BusinessEntities.BAL.ExceptionLogging().AddLogToDB(ex.StackTrace, "receipient is empty", "", ExceptionType.Email);
                        }
                    }
                    newMail.Subject = subject;
                    newMail.Body = mailbody;
                    newMail.IsBodyHtml = true;


                    //SmtpClient smtp = new SmtpClient
                    //{
                    //    DeliveryMethod = SmtpDeliveryMethod.Network,
                    //    Host = "smtp.gmail.com",//CommonKey.SMTPHost,
                    //    //Port = 587,//CommonKey.SMTPPort,
                    //    Credentials = new NetworkCredential("aliahmedk40@gmail.com", "11081990"),//CommonKey.FromEmail, CommonKey.EmailPwd),
                    //    EnableSsl = true//CommonKey.EnableSSL
                    //};
                    
                    SmtpClient smtp = new SmtpClient();
                    //var smtpCredentials = new System.Net.Configuration.SmtpSection().Network;
                    var smtpCredentials = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
                    smtp.Host = smtpCredentials.Network.Host;
                    smtp.Port = smtpCredentials.Network.Port;
                    smtp.Credentials = new NetworkCredential(smtpCredentials.Network.UserName, smtpCredentials.Network.Password);
                    //smtp.UseDefaultCredentials = false;
                    smtp.EnableSsl = smtpCredentials.Network.EnableSsl;
                    //smtp.EnableSsl = false;
                    
                    smtp.Send(newMail);

                    smtp.Dispose();

                    try
                    {
                        new Praescio.BusinessEntities.BAL.ExceptionLogging().AddLogToDB(mailbody, "email has been send", "", ExceptionType.Email);
                    }
                    catch (Exception ex)
                    {
                        new Praescio.BusinessEntities.BAL.ExceptionLogging().AddLogToDB(ex.StackTrace, ex.Message, "", ExceptionType.Email);
                    }
                }
                else
                {
                    //Common.AddLogToDB(mailbody, "email id was empty");
                }
            }
            catch (Exception ex)
            {
                new Praescio.BusinessEntities.BAL.ExceptionLogging().AddLogToDB(ex.StackTrace, ex.InnerException.Message, "", ExceptionType.Email);
            }
        }

        public static string GetEmailContent(Dictionary<string, string> content, MailType type, ref string subject)
        {
            try
            {
                subject = string.Empty;
                string mailcontent = string.Empty;
                XmlDataDocument xmldoc = new XmlDataDocument();
                XmlNodeList xmlnode;
                string Path = HostingEnvironment.MapPath("~/");
                PraescioContext db = new PraescioContext();

                int i = 0;
                FileStream fs = new FileStream(Path + "/Template/EmailContent.xml", FileMode.Open, FileAccess.Read);
                xmldoc.Load(fs);
                fs.Close();

                #region Email Content
                try
                {
                    #region EMAIL CONTENT
                    if (MailType.ResetPassword == type)
                    {
                        string email = Convert.ToString(content["username"]);
                        var account = db.Account.FirstOrDefault(x => x.Email.ToLower() == email.ToLower());

                        xmlnode = xmldoc.GetElementsByTagName("PasswordResetLink");
                        mailcontent = xmlnode.Item(0).InnerXml;
                        mailcontent = mailcontent.Replace("txtLink", AppCode.Common.PasswordResetLink + "?guid=" + content["guid"].ToString());
                        subject = xmlnode.Item(0).Attributes["subject"].Value.Replace("txtRequest", "");
                    }
                    else if (MailType.IndividualStudentRegister == type || MailType.IndividualTeacherRegister == type || MailType.InstitutionTeacher == type || MailType.InstitutionStudent == type)
                    {
                        mailcontent = "<html xmlns=\"http://www.w3.org/1999/xhtml\"><head><title></title></head><body><img src = \"//www.aspsnippets.com/images/Blue/Logo.png\" /><br /><br /><div style = \"border-top:3px solid #22BCE5\">&nbsp;</div><span style = \"font-family:Arial;font-size:10pt\">Hello " + (content.ContainsKey("name") ? Convert.ToString(content["name"]) : "") + "<br /><br />please find your registered username and password below ,<br /><br />Username: <b>" + (content.ContainsKey("username") ? Convert.ToString(content["username"]) : "") + "</b>.<br /> Password: <b>" + (content.ContainsKey("password") ? Convert.ToString(content["password"]) : "") + "</b><br /><br />Thanks<br />Praescio</span></body></html>";
                        subject = "Praescio : Account has been created successfully!!!";
                    }

                    #endregion
                    return mailcontent;
                }
                catch (Exception ex)
                {
                    //Common.AddLogToDB(ex.StackTrace, ex.Message);
                    return "";
                }
                #endregion

            }
            catch (Exception ex)
            {
                subject = string.Empty;
                return ex.Message;
            }
        }


    }

    public class SMS
    {
        public static void SendAlertSMS(string DestinationNumber, string MessageContent)
        {
            if (MessageContent != null && MessageContent != "")
            {
                string UserName = ConfigurationManager.AppSettings["UnicelUserName"].ToString();
                string Password = ConfigurationManager.AppSettings["UnicelPassword"].ToString();
                string SenderId = ConfigurationManager.AppSettings["UnicelSenderId"].ToString();
                string UnicelURL = ConfigurationManager.AppSettings["UnicelURL"].ToString();
                // string ProxyLink = ConfigurationManager.AppSettings["proxyLink"].ToString();

                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("" + UnicelURL + "?user=" + UserName + "&pass=" + Password + "&sender=" + SenderId + "&phone=" + DestinationNumber + "&text=" + MessageContent + "&priority=ndnd&stype=normal");
                webRequest.AllowAutoRedirect = false;

                HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
                Console.Write(response.StatusCode.ToString());
                Stream receiveStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(receiveStream, Encoding.UTF8);
                string Message = reader.ReadToEnd();

                Mst_ExceptionLog smslogger = new Mst_ExceptionLog
                {
                    LoggerName = "WebApi",
                    ExceptionType = "SMS Alert",
                    ExceptionMessage = DestinationNumber,
                    ExceptionStackTrace = MessageContent,
                    CreatedDate = DateTime.Now
                };

                new Praescio.BusinessEntities.BAL.ExceptionLogging().AddLogToDB(smslogger);
            }
        }

        //public static string GetEmailContent(Dictionary<string, string> content, MailType type, ref string subject)
        //{
        //    try
        //    {
        //        subject = string.Empty;
        //        string smscontent = string.Empty;
        //        #region SMS CONTENT

        //        if (MailType.IndividualStudentRegister == type || MailType.IndividualTeacherRegister == type || MailType.InstitutionTeacher == type || MailType.InstitutionStudent == type)
        //        {
        //            smscontent = "<html xmlns=\"http://www.w3.org/1999/xhtml\"><head><title></title></head><body><img src = \"//www.aspsnippets.com/images/Blue/Logo.png\" /><br /><br /><div style = \"border-top:3px solid #22BCE5\">&nbsp;</div><span style = \"font-family:Arial;font-size:10pt\">Hello " + Convert.ToString(content["name"]) + "<br /><br />please find your registered username and password below ,<br /><br />Username: <b>" + Convert.ToString(content["username"]) + "</b>.<br /> Password: <b>" + Convert.ToString(content["password"]) + "</b><br /><br />Thanks<br />Praescio</span></body></html>";
        //            subject = "Account has been created successfully!!!";
        //        }

        //        #endregion
        //        return smscontent;
        //    }
        //    catch (Exception ex)
        //    {
        //        //Common.AddLogToDB(ex.StackTrace, ex.Message);
        //        return "";
        //    }
        //}
    }
}