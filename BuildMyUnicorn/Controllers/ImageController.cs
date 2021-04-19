using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BuildMyUnicorn.Controllers
{
    public class ImageController : WebController
    {
        // GET: Image
        public ActionResult Index()
        {
            return View();
        }
        public string ImageUpload(HttpPostedFileBase file)
        {
            if (file != null)
            {
                string FileName = System.IO.Path.GetFileName(file.FileName);
                string guid = Guid.NewGuid().ToString();
               //string basePath = Server.MapPath("~/Content/images/");
                string filePath = System.IO.Path.Combine(Server.MapPath("~/Content/images/"), FileName);
                //string filePath = System.IO.Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["ClientBaseUrl"] + "/Content/images/"), FileName);
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

        //public string ImageUpload(HttpPostedFileBase file)
        //{
        //    if (file != null)
        //    {
        //        string FileName = System.IO.Path.GetFileName(file.FileName);
        //        string guid = Guid.NewGuid().ToString();
        //        // string basePath = Server.MapPath("~/Content/file_upload/");
        //       //string url = ConfigurationManager.AppSettings["SupplierBaseUrl"] + "/Content/images/"+ FileName";
        //        string filePath = System.IO.Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["SupplierBaseUrl"] + "/Content/images/"), FileName);
        //        // string filePath = System.IO.Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["SupplierBaseUrl"] + "/Content/images/"), FileName);
        //        file.SaveAs(filePath);
        //        string fileGuid = guid + Path.GetExtension(filePath);
        //        var newFilePath = Path.Combine(Path.GetDirectoryName(filePath), fileGuid);
        //        System.IO.File.Move(filePath, newFilePath);
        //        return fileGuid;
        //    }
        //    else
        //    {
        //        return "!OK";
        //    }

        //}
    }
}