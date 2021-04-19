using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Administration.Business_Layer;
using Business_Model.Model;

namespace Administration.Controllers
{
    public class ManageGrantsController : Controller
    {

        public ActionResult Index()
        {

            return View();
        }

        public JsonResult GetAll()
        {
            Master obj = new Master();
            IEnumerable<Grants> list = obj.GetGrantsList();
            return Json(new { msg = list, total = list.Count() }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCountryList()
        {

            IEnumerable<Country> countryList = new Master().GetCountryList();
            return Json(new { country = countryList }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Get(Guid ID)
        {
            return Json(new Master().GetSingleGrant(ID), JsonRequestBehavior.AllowGet);
        }

        public string Add(Grants Model)
        {
         
            return new Master().AddNewGrant(Model);
        }

        public string Update(Grants Model)
        {
         
            return new Master().UpdateGrant(Model);
        }

        public string Delete(Guid ID)
        {

            return new Master().DeleteGrant(ID);
        }
    }
}