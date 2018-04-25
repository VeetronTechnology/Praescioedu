using Praescio.Domain.DAL;
using Praescio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Praescio.API.Controllers
{
    public class BaseApiController : ApiController
    {
        public string UserIP;
        public Mst_Account LoggedInAccount;

        public void LogActivities(Mst_Activities Activities)
        {
            PraescioContext db = new PraescioContext();
            db.Activities.Add(Activities);
            db.SaveChanges();
        }
    }
}
