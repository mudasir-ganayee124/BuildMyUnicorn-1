using Administration.Business_Layer;
using Business_Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Administration.Controllers
{
    public class ManageSupplierController : Controller
    {
        // GET: ManageSupplier
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Details(Guid SupplierID)
        {
         
            //ViewBag.CountryList = new CountryManager().GetCountryList();
            ViewBag.BusinessPlacement = new Master().GetOptionMasterList((int)OptionType.BusinessPlacement);
            ViewBag.WorkLocation = new Master().GetOptionMasterList((int)OptionType.WorkLocation);
            ViewBag.CompanyType = new Master().GetOptionMasterList((int)OptionType.CompanyType);
            //ViewBag.PackageModel = new Master().GetOptionMasterList();
            Supplier obj = new SupplierManager().GetSingleSupplier(SupplierID);

            //ViewBag.BusinessPlacement = new MasterManager().GetOptionMasterList((int)OptionType.BusinessPlacement);
            //ViewBag.WorkLocation = new MasterManager().GetOptionMasterList((int)OptionType.WorkLocation);
            //ViewBag.CompanyType = new MasterManager().GetOptionMasterList((int)OptionType.CompanyType);
            return View(obj);
           
        }
        public ActionResult AccountManager(Guid SupplierID)
        {
          
            SupplierManager obj = new SupplierManager();
            IEnumerable<AccountManager> Modellist = obj.GetAccountManagerList(SupplierID);
            return View(Modellist);
        }
        public ActionResult Tree()
        {
            return View();
        }
        public JsonResult GetAll()
        {
            SupplierManager obj = new SupplierManager();
            IEnumerable<Supplier> list = obj.GetAllSupplier();
            return Json(new { msg = list, total = list.Count() }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAccountManager(Guid ID)
        {
            SupplierManager obj = new SupplierManager();
            IEnumerable<AccountManager> list = obj.GetAccountManagerList(ID);
            return Json(new { msg = list, total = list.Count() }, JsonRequestBehavior.AllowGet);
        }

        public string UpdateAccountManager(List<_AccountManager> Model)
        {
            new SupplierManager().UpdateAccountManager(Model);
            return "OK";
        }

        public string Delete(Guid ID)
        {

            return new SupplierManager().Delete(ID);
        }

        public string Active(Guid ID)
        {

            return new SupplierManager().UpdateStatus(ID, true);
        }

        public string InActive(Guid ID)
        {

            return new SupplierManager().UpdateStatus(ID, false);
        }

    }
}