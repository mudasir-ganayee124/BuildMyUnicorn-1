using Administration.Business_Layer;
using Business_Model.Helper;
using Business_Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Administration.Controllers
{
    public class PlanController : Controller
    {
        // GET: Plan
        public ActionResult Index()
        {
            ViewBag.Currency = new Master().GetCurrencyList();
            var enumData = from Frequency e in Enum.GetValues(typeof(Frequency))
                           select new
                           {
                               ID = (int)e,
                               Name = e.ToString()
                           };
            ViewBag.FrequencyList = new SelectList(enumData, "ID", "Name");

            return View();
        }
        public ActionResult RecurringPlan(string PlanID)
        {
             return View();
        }
        public JsonResult GetAll()
        {
            PlanManager obj = new PlanManager();
            IEnumerable<Plan> list = obj.GetAllPlan();
            return Json(new { msg = list, total = list.Count() }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Get(Guid ID)
        {
            return Json(new PlanManager().GetSinglePlan(ID), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPlanRecurring(Guid ID)
        {
            return Json(new PlanManager().GetPlanRecurring(ID), JsonRequestBehavior.AllowGet);
        }
        public string Add(Plan Model)
        {
            Model.EntityState = EntityState.New;
            Model.PlanID = Guid.NewGuid();
            return new PlanManager().AddPlan(Model);
        }

        public string Update(Plan Model)
        {
            Model.EntityState = EntityState.Old;
            return new PlanManager().AddPlan(Model);
        }

        public string UpdateRecurring(PlanRecurring Model)
        {
            return new PlanManager().UpdateRecurringPlan(Model);
        }
    }
}