using Administration.Business_Layer;
using Business_Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Administration.Controllers
{
    public class ManageMasterController : Controller
    {
        // GET: ManageMaster
        public ActionResult Index(string id)
        {
          
            return View();
        }

        public ActionResult Type(string id)
        {
            string MasterType = id;
            return View();
        }

        public ActionResult IdeaProgress()
        {
            return View();
        }
        public ActionResult FeedBack()
        {

            return View();
        }

        public ActionResult CompanyLegalStructure()
        {

            return View();
        }

        public ActionResult ProductServiceProduce()
        {

            return View();
        }

        public ActionResult MVPReason()
        {

            return View();
        }

        public ActionResult ManageDevelopment()
        {
            return View();
        }

        public JsonResult GetAll(int Type)
        {
            Master obj = new Master();
            IEnumerable<MasterCommon> list = obj.GetOptionMasterList(Type);
            return Json(new { msg = list, total = list.Count() }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Get(Guid ID, int Type)
        {
            return Json(new Master().GetSingleOptionMaster(Type, ID), JsonRequestBehavior.AllowGet);
        }

        public string Add(Option Model)
        {
            
            return new Master().AddNewOptionMaster(Model);
        }

        public string Update(Option Model)
        {
          
            return new Master().UpdateOptionMaster(Model);
        }

        public string Delete(Guid ID)
        {

            return new Master().DeleteOptionMaster(ID);
        }

    }
}