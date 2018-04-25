using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Praescio.Controllers
{
    public class BaseController : Controller
    {
        public string SavePDFFile()
        {
            try
            {
                string postedfilepath = string.Empty;
                string localpath = "Upload/RequestUpload/";
                string filename = "";

                System.Web.HttpFileCollection file = System.Web.HttpContext.Current.Request.Files;

                if (file.Count == 0)
                {
                    return "";
                }

                if (!Directory.Exists(Server.MapPath("~/" + localpath)))
                {
                    Directory.CreateDirectory(Server.MapPath("~/" + localpath));
                }

                string CreateFolder = Guid.NewGuid().ToString();
                string sPath = Server.MapPath("~/" + localpath + CreateFolder);

                Directory.CreateDirectory(Server.MapPath("~/" + localpath + CreateFolder));

                for (int i = 0; i < file.Count; i++)
                {
                    System.Web.HttpPostedFile hpf = file[i];
                    postedfilepath = sPath + "//" + Path.GetFileName(hpf.FileName);
                    filename = "//" + Path.GetFileName(hpf.FileName);

                    if (hpf.ContentLength > 0)
                    {
                        hpf.SaveAs(postedfilepath);
                        Response.StatusCode = (int)HttpStatusCode.OK;
                    }
                }
                return localpath + CreateFolder + filename;
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.OK;
                return ex.StackTrace + "-----------" + ex.Message;
            }

        }

        public FileResult DownloadFile(string path)
        {
            var filename = Path.GetFileName(path);

            byte[] fileBytes = System.IO.File.ReadAllBytes(Server.MapPath(path));
            string fileName = filename;
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
    }
}