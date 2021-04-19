using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BuildMyUnicorn_Supplier.Business_Layer;
using Business_Model.Model;

namespace BuildMyUnicorn_Supplier.Controllers
{
    public class DashboardController : WebController
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            Guid SupplierID = Guid.Parse(User.Identity.Name);
            ViewBag.CountryList = new CountryManager().GetCountryList();
            //ViewBag.BusinessPlacement = new MasterManager().GetOptionMasterList((int)OptionType.BusinessPlacement);
            ViewBag.Modules = new MasterManager().GetModuleList();
            ViewBag.WorkLocation = new MasterManager().GetOptionMasterList((int)OptionType.WorkLocation);
            ViewBag.CompanyType = new MasterManager().GetOptionMasterList((int)OptionType.CompanyType);
            ViewBag.PackageModel = new PackageManager().GetSupplierPackageList();
            ViewBag.AccountManager = new SupplierManager().GetAccountManagerList(SupplierID);
            Supplier obj = new SupplierManager().GetSingleSupplier(SupplierID);
         
            ViewBag.BusinessPlacement = new MasterManager().GetOptionMasterList((int)OptionType.BusinessPlacement);
            ViewBag.WorkLocation = new MasterManager().GetOptionMasterList((int)OptionType.WorkLocation);
            ViewBag.CompanyType = new MasterManager().GetOptionMasterList((int)OptionType.CompanyType);
            return View(obj);
        }

        public string UpdateProfile(Supplier Model)
        {
            return new SupplierManager().UpdateSupplierProfile(Model);
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
    }
}