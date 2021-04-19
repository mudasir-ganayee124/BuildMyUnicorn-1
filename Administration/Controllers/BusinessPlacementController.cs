using Administration.Business_Layer;
using Business_Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Administration.Controllers
{
    public class BusinessPlacementController : Controller
    {
        // GET: BusinessPlacement
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetAll()
        {
            Master obj = new Master();
            IEnumerable<MasterCommon> list = obj.GetOptionMasterList((int)OptionType.BusinessPlacement);
            return Json(new { msg = list, total = list.Count() }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Get(Guid ID)
        {
            return Json(new Master().GetSingleOptionMaster((int)OptionType.BusinessPlacement, ID), JsonRequestBehavior.AllowGet);
        }

        public string Add(Option Model)
        {
            Model.Type = OptionType.BusinessPlacement;
            return new Master().AddNewOptionMaster(Model);
        }

        public string Update(Option Model)
        {
            Model.Type = OptionType.BusinessPlacement;
            return new Master().UpdateOptionMaster(Model);
        }

        public string Delete(Guid ID)
        {

            return new Master().DeleteOptionMaster(ID);
        }

    }
}