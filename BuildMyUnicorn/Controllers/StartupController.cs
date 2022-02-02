using BuildMyUnicorn.Business_Layer;
using Business_Model.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BuildMyUnicorn.Controllers
{
    public class StartupController : WebController
    {
        // GET: Startup
        public ActionResult Index()
        {

           // var clientID = Guid.Parse(User.Identity.Name);
            return View(new ClientManager().GetMainClientStartup());
        }

        public string FileUpload(HttpPostedFileBase file)
        {
            if (file != null)
            {
                string FileName = System.IO.Path.GetFileName(file.FileName);
                string guid = Guid.NewGuid().ToString();
                string basePath = Server.MapPath("~/Content/Images/");
                string filePath = System.IO.Path.Combine(Server.MapPath("~/Content/Images/"), FileName);
                file.SaveAs(filePath);
                string fileGuid = guid + Path.GetExtension(filePath);
                var newFilePath = Path.Combine(Path.GetDirectoryName(filePath), fileGuid);
                System.IO.File.Move(filePath, newFilePath);
                return fileGuid;
            }
            else
            {
                return "!OK";
            }

        }

        public string UpdateStartup(Client Model)
        {
            return new ClientManager().UpdateStartup(Model);
        }
    }
}