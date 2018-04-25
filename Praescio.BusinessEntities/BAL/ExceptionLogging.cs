using Praescio.BusinessEntities.Common;
using Praescio.Domain.DAL;
using Praescio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praescio.BusinessEntities.BAL
{
    public class ExceptionLogging
    {
        public void AddLogToDB(string contentbody, string logmessage,string ip, ExceptionType exceptionType)
        {
            try
            {
                var URL = "";//HttpContext.Current.Request.Url.ToString();
                PraescioContext db = new PraescioContext();

                Mst_ExceptionLog logger = new Mst_ExceptionLog 
                {
                    LoggerName = "WebApi",
                    ExceptionMessage = logmessage,
                    ExceptionStackTrace = contentbody,
                    ExceptionType = exceptionType.ToString(),
                    IPAddress = ip,
                    URL = URL,
                    ControllerName = "",
                    ActionName = "",
                    CreatedDate = DateTime.Now
                };

                db.ExceptionLog.Add(logger);
                db.SaveChanges();
                db.Dispose();
            }
            catch (Exception ex)
            {

            }
        }

        public void AddLogToDB(Mst_ExceptionLog exception)
        {
            try
            {
                PraescioContext db = new PraescioContext();

                db.ExceptionLog.Add(exception);
                db.SaveChanges();
                db.Dispose();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
