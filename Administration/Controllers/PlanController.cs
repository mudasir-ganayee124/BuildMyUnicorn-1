using Administration.Business_Layer;
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
            return View();
        }

        public JsonResult GetAll()
        {
            PlanManager obj = new PlanManager();
            IEnumerable<Plan> list = obj.GetAllPlan();
            return Json(new { msg = list, total = list.Count() }, JsonRequestBehavior.AllowGet);
        }
    }
}